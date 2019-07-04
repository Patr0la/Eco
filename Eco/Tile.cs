using System;
using Microsoft.Xna.Framework;

namespace Eco
{
    public enum TileType
    {
        Grass,
        Water,
        DeepWater,
        Shore,
        Swamp,
        Tree,
        Rock,
        Food
    }

    public class Tile
    {
        public int x, y;
        public TileType TileType;

        public bool Passable;
        public float MoveSpeed;

        public Object ObjectOnTop;

        public Tile(int x, int y, TileType tileType, Object objectOnTop)
        {
            this.x = x;
            this.y = y;
            TileType = tileType;
            ObjectOnTop = objectOnTop;

            if (TileType == TileType.Tree || TileType == TileType.Rock || TileType == TileType.Food || TileType == TileType.Water || TileType == TileType.DeepWater) Passable = false;
            else
            {
                Passable = true;
                if (TileType == TileType.Grass || TileType == TileType.Shore)
                    MoveSpeed = 1;
                else
                    MoveSpeed = 0.5f;
            }
        }

        public Color GetColor()
        {
            if (TileType == TileType.Grass || TileType == TileType.Tree || TileType == TileType.Rock || TileType == TileType.Food) return Color.Green;
            if (TileType == TileType.Water) return Color.Blue;
            if (TileType == TileType.DeepWater) return Color.DarkBlue;
            if (TileType == TileType.Swamp) return Color.SandyBrown;
            if (TileType == TileType.Shore) return Color.LightGoldenrodYellow;
            return Color.Black;
        }
    }
}
