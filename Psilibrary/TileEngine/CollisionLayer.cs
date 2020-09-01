using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Psilibrary.TileEngine
{
    public enum CollisionValue { Impassible, Water }

    public class CollisionLayer
    {
        private Dictionary<Point, CollisionValue> _collisionAreas;

        public Dictionary<Point, CollisionValue> CollisionAreas
        {
            get { return _collisionAreas; }
            set { _collisionAreas = value; }
        }

        public CollisionLayer()
        {
            _collisionAreas = new Dictionary<Point, CollisionValue>();
        }
    }
}
