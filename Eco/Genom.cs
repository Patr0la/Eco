using System;
namespace Eco
{
    public enum Traits
    {
        Speed, // Time izmedu kretnji
        ReproductiveNeed,
        PregnencyTime,
        NumberOfOffsprings, // Vise -> potomci su manji
        Fitness, // Kolicina kretnji prije nego li se treba odmoriti
        Inteligence, // Sposobnost sječanja gdje su hrana i voda
        Vision // Udaljenost viđenja
    }

    public class Genom
    {
        public Trait[] Traits;

        public Genom(Trait[] trait)
        {
            Traits = trait;
        }

        public void Mutate(double mutationActivationValue)
        {
            for (int i = 0; i < 7; i++)
            {
                Traits[i].Mutate(mutationActivationValue);
            }
        }

        public static Genom Cross(Genom a, Genom b)
        {
            Trait[] newTraits = new Trait[7];

            for (int i = 0; i < 7; i++)
            {
                Trait traitA = a.Traits[i];
                Trait traitB = b.Traits[i];
                int[] activations = new int[traitA.ActiveGenes.Length];

                for (int j = 0;  j < traitA.ActiveGenes.Length; j++)
                {
                    if (Simulation.Random.NextDouble() > 0.5)
                        activations[j] = traitA.ActiveGenes[j];
                    else
                        activations[j] = traitB.ActiveGenes[j];
                }

                newTraits[i] = new Trait(traitA.BaseValue, traitA.BaseValue, activations);
            }

            return new Genom(newTraits);
        }
    }

    public class Trait
    {
        public readonly float BaseValue;
        public readonly float IncriseValue;

        public int[] ActiveGenes;

        public Trait(float baseValue, float incriseValue, int[] activeGenes)
        {
            BaseValue = baseValue;
            IncriseValue = incriseValue;
            ActiveGenes = activeGenes;
        }

        public void Mutate(double mutationActivationValue)
        {
            for (int i = 0; i < ActiveGenes.Length; i++)
            {
                if (Simulation.Random.NextDouble() > mutationActivationValue)
                {
                    if (ActiveGenes[i] == 1) ActiveGenes[i] = 0;
                    else ActiveGenes[i] = 1;
                }
            }
        }

        public float GetValue()
        {
            float totalActive = 0;
            for (int i = 0; i < ActiveGenes.Length; i++)
            {
                totalActive += ActiveGenes[i];
            }
            return BaseValue + totalActive * IncriseValue;
        }
    }
}
