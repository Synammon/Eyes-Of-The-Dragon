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

        private int _tileIndex;
        private int _tileset;
        private int _rotation;
        private bool _visible;

        #endregion

        #region Property Region

        [ContentSerializer]
        public int TileIndex
        {
            get { return _tileIndex; }
            private set { _tileIndex = value; }
        }

        [ContentSerializer]
        public int Tileset
        {
            get { return _tileset; }
            private set { _tileset = value; }
        }

        [ContentSerializer(Optional=true)]
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [ContentSerializer(Optional=true)]
        public int Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        #endregion

        #region Constructor Region

        private Tile()
        {
            _visible = true;
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
