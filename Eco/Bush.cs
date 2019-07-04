using System;
using Microsoft.Xna.Framework;

namespace Eco
{
    public class Bush : Object
    {
        int Food = 0;
        const int MaxFood = 5;
        const int Tier2Food = 3;
        int RegenerationTickTime = 1000;
        private int _nextRegenerationTick;

        public Bush(int x, int y) : base(Models.Bush_1, x, y)
        {
        }


        public override void Update(GameTime gameTime)
        {
            if (Food < MaxFood)
            {
                if (_nextRegenerationTick == 0)
                    _nextRegenerationTick = Simulation.Tick + RegenerationTickTime;

                if (_nextRegenerationTick == Simulation.Tick)
                {
                    Food++;
                    _nextRegenerationTick = 0;
                }
            }

            if (Food == 0) Model = Models.Bush_0;
            else if (Food > Tier2Food) Model = Models.Bush_2;
            else Model = Models.Bush_1;
        }
    }
}
