using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Psilibrary.TileEngine
{
    public class Tileset
    {
        #region Fields and Properties

        private Texture2D _image;
        private int _tileWidthInPixels;
        private int _tileHeightInPixels;
        private int _tilesWide;
        private int _tilesHigh;
        private Rectangle[] _sourceRectangles;

        #endregion

        #region Property Region

        public Texture2D Texture
        {
            get { return _image; }
            private set { _image = value; }
        }

        public int TileWidth
        {
            get { return _tileWidthInPixels; }
            private set { _tileWidthInPixels = value; }
        }

        public int TileHeight
        {
            get { return _tileHeightInPixels; }
            private set { _tileHeightInPixels = value; }
        }

        public int TilesWide
        {
            get { return _tilesWide; }
            private set { _tilesWide = value; }
        }

        public int TilesHigh
        {
            get { return _tilesHigh; }
            private set { _tilesHigh = value; }
        }

        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])_sourceRectangles.Clone(); }
        }

        #endregion

        #region Constructor Region

        public Tileset(Texture2D image, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            Texture = image;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;

            int tiles = tilesWide * tilesHigh;

            _sourceRectangles = new Rectangle[tiles];

            int tile = 0;

            for (int y = 0; y < tilesHigh; y++)
                for (int x = 0; x < tilesWide; x++)
                {
                    _sourceRectangles[tile] = new Rectangle(
                        x * tileWidth,
                        y * tileHeight,
                        tileWidth,
                        tileHeight);
                    tile++;
                }
        }

        #endregion

        #region Method Region
        #endregion
    }
}
