using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eco
{
    public class Object
    {
        public BoundingBox BoundingBox;
        public Model Model;
        public Vector3 Position;

        public int x, y;

        public Object(Model model, int x, int y)
        {
            Model = model;
            this.x = x;
            this.y = y;

            Position = new Vector3(1 + x * 2, 1 + y * 2, 0);

            BoundingBox = new BoundingBox(Position - Vector3.One, Position + Vector3.One);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw()
        {
            if (Model != null && Simulation.Camera.Frustum.Contains(BoundingBox) != ContainmentType.Disjoint)
                Simulation.DrawModel(Model, Position);
        }
    }
}
