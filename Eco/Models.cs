using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Eco
{
    public static class Models
    {
        public static Model Rabbit, Fox, Wolf, Tree, Rock;

        public static Model Bush_0, Bush_1, Bush_2;

        public static void Initlize(ContentManager content)
        {
            Rabbit = content.Load<Model>("Rabbit");
            Wolf = content.Load<Model>("Wolf");

            Tree = content.Load<Model>("Tree");
            Rock = content.Load<Model>("Rock");

            Bush_0 = content.Load<Model>("Bush_0");
            Bush_1 = content.Load<Model>("Bush_1");
            Bush_2 = content.Load<Model>("Bush_2");
        }
    }
}
