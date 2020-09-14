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

        private int tilesWide = 1;
        private int tilesHigh = 1;

        private Point sourceTile;
        private Point destinationTile;
        private string destinationLevel;

        #endregion

        #region Property Region

        [ContentSerializer]
        public Point SourceTile
        {
            get { return sourceTile; }
            private set { sourceTile = value; }
        }

        [ContentSerializer]
        public Point DestinationTile
        {
            get { return destinationTile; }
            private set { destinationTile = value; }
        }

        [ContentSerializer]
        public string DestinationLevel
        {
            get { return destinationLevel; }
            private set { destinationLevel = value; }
        }

        [ContentSerializer(Optional=true)]
        public int TilesWide
        {
            get { return tilesWide; }
            private set { tilesWide = value; }
        }

        [ContentSerializer(Optional = true)]
        public int TilesHigh
        {
            get { return tilesHigh; }
            private set { tilesHigh = value; }
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
                tilesWide = 1;
                tilesHigh = 1;
            }
            else
            {
                tilesWide = p.Value.X;
                tilesHigh = p.Value.Y;
            }
        }

        #endregion
    }
}
