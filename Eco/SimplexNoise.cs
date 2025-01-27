﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Eco {
    public static class Noise2d
    {
        private static int[] _permutation;

        private static Vector2[] _gradients;

        static Noise2d()
        {
            CalculatePermutation(out _permutation);
            CalculateGradients(out _gradients);
        }

        private static void CalculatePermutation(out int[] p)
        {
            p = Enumerable.Range(0, 256).ToArray();

            /// shuffle the array
            for (var i = 0; i < p.Length; i++)
            {
                var source = Simulation.Random.Next(p.Length);

                var t = p[i];
                p[i] = p[source];
                p[source] = t;
            }
        }

        /// <summary>
        /// generate a new permutation.
        /// </summary>
        public static void Reseed()
        {
            CalculatePermutation(out _permutation);
        }

        private static void CalculateGradients(out Vector2[] grad)
        {
            grad = new Vector2[256];

            for (var i = 0; i < grad.Length; i++)
            {
                Vector2 gradient;

                do
                {
                    gradient = new Vector2((float)(Simulation.Random.NextDouble() * 2 - 1), (float)(Simulation.Random.NextDouble() * 2 - 1));
                }
                while (gradient.LengthSquared() >= 1);

                gradient.Normalize();

                grad[i] = gradient;
            }

        }

        private static float Drop(float t)
        {
            t = Math.Abs(t);
            return 1f - t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static float Q(float u, float v)
        {
            return Drop(u) * Drop(v);
        }

        public static float Noise(float x, float y)
        {
            var cell = new Vector2((float)Math.Floor(x), (float)Math.Floor(y));

            var total = 0f;

            var corners = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

            foreach (var n in corners)
            {
                var ij = cell + n;
                var uv = new Vector2(x - ij.X, y - ij.Y);

                var index = _permutation[(int)ij.X % _permutation.Length];
                index = _permutation[(index + (int)ij.Y) % _permutation.Length];

                var grad = _gradients[index % _gradients.Length];

                total += Q(uv.X, uv.Y) * Vector2.Dot(grad, uv);
            }

            return Math.Max(Math.Min(total, 1f), -1f);
        }

        public static float[] GenerateNoiseMap(int w, int h, float frequency, float amplitude)
        {
            var data = new float[w * h];

            var min = float.MaxValue;
            var max = float.MinValue;

            Reseed();

            for (var octave = 0; octave < 5; octave++)
            {
                /// parallel loop - easy and fast.
                Parallel.For(0
                    , w * h
                    , (offset) =>
                    {
                        var i1 = offset % w;
                        var j = offset / w;
                        var noise = Noise(i1 * frequency * 1f / w, j * frequency * 1f / h);
                        noise = data[j * w + i1] += noise * amplitude;

                        min = Math.Min(min, noise);
                        max = Math.Max(max, noise);

                    }
                );

                frequency *= 2;
                amplitude /= 2;
            }

            var noiseData = data.Select(
                (f) =>
                {
                    var norm = (f - min) / (max - min);
                    return norm;
                }
            ).ToArray();

            return noiseData;
        }
    }
}