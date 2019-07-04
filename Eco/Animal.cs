using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eco
{
    public class Animal : Object
    {
        public int Hp, MaxHp;
        public float Food, Water, ReproductionUrge, Vision, Stamina;
        public float MaxFood, MaxWater, MaxReproductionUrge, MaxStamina;

        public Genom Genom;

        public Animal(Genom genom, Model model, int x, int y) : base(model, x, y)
        {
            Genom = genom;

            this.x = x;
            this.y = y;

            this.Vision = Genom.Traits[(int)Traits.Vision].GetValue();
            this.Stamina = Genom.Traits[(int)Traits.Fitness].GetValue();
        }

        public Animal(Model model, int x, int y) : base(model, x, y)
        {
            this.x = x;
            this.y = y;
        }


        public Animal(int hp, int maxHp, float food, float water, float reproductionUrge, float vision, float stamina, float maxFood, float maxWater, float maxReproductionUrge, float maxStamina, Genom genom, Model model, int x, int y) : base(model, x, y)
        {
            Hp = hp;
            MaxHp = maxHp;
            Food = food;
            Water = water;
            ReproductionUrge = reproductionUrge;
            Vision = vision;
            Stamina = stamina;
            MaxFood = maxFood;
            MaxWater = maxWater;
            MaxReproductionUrge = maxReproductionUrge;
            MaxStamina = maxStamina;
        }

        public virtual Genom GenerateGenom(float[] baseValues, float[] increseValues, double miniActivationValue)
        {
            if (baseValues == null || increseValues == null || Math.Abs(miniActivationValue) < 0.1) throw new Exception("Trait values not defined");

            Trait[] traits = new Trait[7];

            for (int i = 0; i < 7; i++)
            {
                int[] activeGenes = new int[8];
                for (int j = 0; j < 8; j++)
                    if (Simulation.Random.NextDouble() > miniActivationValue)
                        activeGenes[j] = 1;
                    else
                        activeGenes[j] = 0;
                traits[i] = new Trait(baseValues[i], increseValues[i], activeGenes);
            }

            return new Genom(traits);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw()
        {

        }

        public virtual void Move()
        {

        }

        public virtual void FindFood()
        {

        }

        public virtual void Eat()
        {

        }

        public virtual void FindWater()
        {

        }

        public virtual void Drink()
        {

        }

        public virtual void FindMate()
        {

        }

        public virtual void Mate()
        {

        }
    }
}
