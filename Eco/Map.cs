using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eco
{
    public class Map
    {
        public const int TILE_WIDTH = 2;
        public const int TILE_HEIGHT = 2;
        public Tile[,] Tiles;
        public int w, h;

        public VertexPositionColor[] Vertecies;
        public Map(int w1, int h1)
        {
            w = w1;
            h = h1;

            Vertecies = new VertexPositionColor[w * h * 3 * 2];

            Tiles = new Tile[h,w];

            int i = 0;


            var terrainNoiseData = Noise2d.GenerateNoiseMap(w, h, 5f, 1f);
            var terrainOnTopNoiseData = Noise2d.GenerateNoiseMap(w, h, 80f, 1f);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    double terrainType = terrainNoiseData[y * w + x];
                    double terrainOnTop = terrainOnTopNoiseData[y * w + x];

                    Object objectOnTop = null;

                    if (terrainType > 0.3)
                    {
                        if (terrainType < 0.4)
                        {
                            if (terrainOnTop > 0.9) objectOnTop = new Object(Models.Rock, x, y);
                            Tiles[y, x] = new Tile(x, y, TileType.Shore, objectOnTop);
                        }
                        else if (terrainType < 0.85)
                        {
                            if( terrainOnTop > 0.85) objectOnTop = new Bush(x, y); 
                            else if (terrainOnTop > 0.8) objectOnTop = new Object(Models.Rock, x, y);
                            else if (terrainOnTop > 0.7) objectOnTop = new Object(Models.Tree, x, y); 

                            Tiles[y, x] = new Tile(x, y, TileType.Grass, objectOnTop);
                        }
                        else
                        {
                            Tiles[y, x] = new Tile(x, y, TileType.Swamp, objectOnTop);
                        }
                    }
                    else if (terrainType > 0.15)
                        Tiles[y, x] = new Tile(x, y, TileType.Water, objectOnTop);
                    else
                        Tiles[y, x] = new Tile(x, y, TileType.DeepWater, objectOnTop);

                    if (objectOnTop != null) Simulation.Objects.Add(objectOnTop);
                }
            }


            for (int y = 0; y < h; y++)
            {
                for(int x = 0; x < w;  x++)
                {
                    Color color = Tiles[y, x].GetColor();
                    //  if (y % 2 == 0)
                    // {

                    int cx = x * TILE_WIDTH;
                    int cy = y * TILE_HEIGHT;

                    int nx = cx + TILE_WIDTH;
                    int ny = cy + TILE_HEIGHT;
                    Vertecies[i] = new VertexPositionColor(new Vector3(cx, cy, 0), color);
                    Vertecies[i + 1] = new VertexPositionColor(new Vector3(cx, ny, 0), color);
                    Vertecies[i + 2] = new VertexPositionColor(new Vector3(nx, cy, 0), color);

                    Vertecies[i + 3] = new VertexPositionColor(Vertecies[i + 1].Position, color);
                    Vertecies[i + 4] = new VertexPositionColor(new Vector3(nx, ny, 0), color);
                    Vertecies[i + 5] = new VertexPositionColor(Vertecies[i + 2].Position, color);

                    for (int ii = 0; ii < 6; ii++)
                        {
                            Vertecies[i + ii].Color = color;
                        }
                    /*  }
                      else
                      {

                      }
  */
                    i += 6;
                }
            }
        }
    }
}
