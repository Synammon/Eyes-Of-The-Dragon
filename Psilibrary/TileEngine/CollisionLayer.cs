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
        private Dictionary<Point, CollisionValue> collisionAreas;

        public Dictionary<Point, CollisionValue> CollisionAreas
        {
            get { return collisionAreas; }
            set { collisionAreas = value; }
        }

        public CollisionLayer()
        {
            collisionAreas = new Dictionary<Point, CollisionValue>();
        }
    }
}
