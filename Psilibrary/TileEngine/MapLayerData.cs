using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary.TileEngine
{
    public class MapLayerData
    {
        #region Field Region

        #endregion

        #region Property Region

        public Dictionary<Point, Tile> Tiles { get; private set; }

        #endregion

        #region Constructor Region

        public MapLayerData()
        {
            Tiles = new Dictionary<Point, Tile>();
        }

        public MapLayerData(MapLayer l)
        {
            for (int y = 0; y < l.Height; y++)
            {
                for (int x = 0; x < l.Width; x++)
                {
                    if (l.GetTile(x, y).TileIndex == -1 || l.GetTile(x,y).Tileset == -1)
                    {
                        continue;
                    }

                    Tiles.Add(new Point(x, y), l.GetTile(x, y));
                }
            }
        }

        #endregion

        #region Method Region
        #endregion
    }
}
