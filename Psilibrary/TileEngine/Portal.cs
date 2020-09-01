using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Psilibrary.TileEngine
{
    public class Portal
    {
        #region Field Region

        private int _tilesWide = 1;
        private int _tilesHigh = 1;

        private Point _sourceTile;
        private Point _destinationTile;
        private string _destinationLevel;

        #endregion

        #region Property Region

        [ContentSerializer]
        public Point SourceTile
        {
            get { return _sourceTile; }
            private set { _sourceTile = value; }
        }

        [ContentSerializer]
        public Point DestinationTile
        {
            get { return _destinationTile; }
            private set { _destinationTile = value; }
        }

        [ContentSerializer]
        public string DestinationLevel
        {
            get { return _destinationLevel; }
            private set { _destinationLevel = value; }
        }

        [ContentSerializer(Optional=true)]
        public int TilesWide
        {
            get { return _tilesWide; }
            private set { _tilesWide = value; }
        }

        [ContentSerializer(Optional = true)]
        public int TilesHigh
        {
            get { return _tilesHigh; }
            private set { _tilesHigh = value; }
        }

        [ContentSerializer(Optional = true)]
        public bool Enabled { get; set; }

        #endregion

        #region Constructor Region

        private Portal()
        {
            Enabled = true;
        }

        public Portal(Point sourceTile, Point destinationTile, string destinationLevel, bool enabled = true, Point? p = null)
        {
            SourceTile = sourceTile;
            DestinationTile = destinationTile;
            DestinationLevel = destinationLevel;

            if (p == null)
            {
                _tilesWide = 1;
                _tilesHigh = 1;
            }
            else
            {
                _tilesWide = p.Value.X;
                _tilesHigh = p.Value.Y;
            }
        }

        #endregion
    }
}
