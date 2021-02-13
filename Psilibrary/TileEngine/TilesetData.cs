using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary.TileEngine
{
    public class TilesetData
    {
        public TilesetData()
        {
        }

        public TilesetData(Tileset tileset)
        {
            TilesetName = tileset.Texture.Name;
            TilesetImageName = tileset.Texture.Name;
            TileWidthInPixels = tileset.TileWidth;
            TileHeightInPixels = tileset.TileHeight;
            TilesWide = tileset.TilesWide;
            TilesHigh = tileset.TilesHigh;
        }

        public string TilesetName { get; set; }
        public string TilesetImageName { get; set; }
        public int TileWidthInPixels { get; set; }
        public int TileHeightInPixels { get; set; }
        public int TilesWide { get; set; }
        public int TilesHigh { get; set; }
    }
}
