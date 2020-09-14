using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Psilibrary.TileEngine
{
    public class Tile
    {
        #region Field Region

        private int tileIndex;
        private int tileset;
        private int rotation;
        private bool visible;

        #endregion

        #region Property Region

        [ContentSerializer]
        public int TileIndex
        {
            get { return tileIndex; }
            private set { tileIndex = value; }
        }

        [ContentSerializer]
        public int Tileset
        {
            get { return tileset; }
            private set { tileset = value; }
        }

        [ContentSerializer(Optional=true)]
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        [ContentSerializer(Optional=true)]
        public int Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        #endregion

        #region Constructor Region

        private Tile()
        {
            visible = true;
        }

        public Tile(int tileIndex, int tileset, int rotation = 0)
            : base()
        {
            TileIndex = tileIndex;
            Tileset = tileset;
            Rotation = rotation;
        }

        #endregion
    }
}    
