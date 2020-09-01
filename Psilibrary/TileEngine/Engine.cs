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

        private static int _tileWidth;
        private static int _tileHeight;

        #endregion

        #region Property Region

        public static int TileWidth
        {
            get { return _tileWidth; }
        }

        public static int TileHeight
        {
            get { return _tileHeight; }
        }

        #endregion

        #region Constructors

        public Engine(int tileWidth, int tileHeight)
        {
            Engine._tileWidth = tileWidth;
            Engine._tileHeight = tileHeight;
        }

        #endregion

        #region Methods

        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / _tileWidth, (int)position.Y / _tileHeight);
        }

        public static Point PointToWorld(Point point)
        {
            return new Point(point.X * _tileWidth, point.Y * _tileHeight);
        }
        #endregion
    }
}
