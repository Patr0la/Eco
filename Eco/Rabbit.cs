using System;
using Microsoft.Xna.Framework;

namespace Eco.Desktop
{
    public class Rabbit : Animal
    {
        private static float[] BaseValues = { 0, 0, 0, 0, 0, 0, 0 };
        private static float[] IncreseValues = { 0.5f, 0.25f, 0.25f, 1, 0, 0, 0 };
        private static double MiniMutationActivationValue = 0.95;

        private float _foodLostPerTick;
        public Rabbit(int x, int y) : base(Models.Rabbit, x, y)
        {
            GenerateGenom(BaseValues, IncreseValues, MiniMutationActivationValue);

            _foodLostPerTick = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            Food -= 0.1f;
        }
    }
}
