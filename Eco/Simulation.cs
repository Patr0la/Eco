using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Eco
{
    public class Simulation : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Camera Camera;
        public static Map Map;
        public static Random Random;

        public static int Tick;

        public static List<Object> Objects = new List<Object>();

        public Simulation()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        BasicEffect effect;
        protected override void Initialize()
        {
            base.Initialize();

            Random = new Random((int)'L');

            Camera = new Camera(GraphicsDevice);

            Map = new Map(100,100);

            effect = new BasicEffect(GraphicsDevice);


            graphics.PreferMultiSampling = true;
            GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Models.Initlize(Content);
        }

        protected override void UnloadContent()
        {
        }

        bool _goneFullscreen = false;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if(!_goneFullscreen && keyboardState.IsKeyDown(Keys.F12))
            {
                _goneFullscreen = true;

                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }
            Camera.Update(gameTime, mouseState, keyboardState);

            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i] != null)
                    Objects[i].Update(gameTime);
            }
            Console.WriteLine(Tick);
            Tick++;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawGround();

            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i] != null)
                    Objects[i].Draw();
            }

            base.Draw(gameTime);
        }

        void DrawGround()
        {

            effect.View = Camera.ViewMatrix;
            effect.Projection = Camera.ProjectionMatrix;
            effect.VertexColorEnabled = true;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                    // We’ll be rendering two trinalges
                    PrimitiveType.TriangleList,
                    // The array of verts that we want to render
                    Map.Vertecies,
                    // The offset, which is 0 since we want to start 
                    // at the beginning of the floorVerts array
                    0,
                    // The number of triangles to draw
                    Map.w * Map.h * 2);
            }
        }

        public static void DrawModel(Model model, Vector3 position)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = Matrix.CreateTranslation(position);
                    effect.View = Camera.ViewMatrix;
                    effect.Projection = Camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }
    }
}
