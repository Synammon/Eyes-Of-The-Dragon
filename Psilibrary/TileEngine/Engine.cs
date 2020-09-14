using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Psilibrary.TileEngine
{
    public class Engine
    {
        #region Field Region

        private static int tileWidth;
        private static int tileHeight;

        #endregion

        #region Property Region

        public static int TileWidth
        {
            get { return tileWidth; }
        }

        public static int TileHeight
        {
            get { return tileHeight; }
        }

        #endregion

        #region Constructors

        public Engine(int tileWidth, int tileHeight)
        {
            Engine.tileWidth = tileWidth;
            Engine.tileHeight = tileHeight;
        }

        #endregion

        #region Methods

        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }

        public static Point PointToWorld(Point point)
        {
            return new Point(point.X * tileWidth, point.Y * tileHeight);
        }
        #endregion
    }
}
