using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary.TileEngine
{
    public class TileMapData
    {
        #region Field Region
        #endregion

        #region Property Region

        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<TilesetData> Tilesets { get; private set; }
        public List<MapLayerData> MapLayerData { get; private set; }

        #endregion

        #region Constructor Region

        private TileMapData()
        {
            Tilesets = new List<TilesetData>();
            MapLayerData = new List<MapLayerData>();
        }

        public TileMapData(TileMap map)
            : this()
        {
            Width = ((MapLayer)map.Layers[0]).Width;
            Height = ((MapLayer)map.Layers[0]).Height;

            foreach (Tileset tileset in map.Tilesets)
            {
                Tilesets.Add(new TilesetData(tileset));
            }

            foreach (ILayer layer in map.Layers)
            {
                if (layer is MapLayer l)
                {
                    MapLayerData data = new MapLayerData(l);
                    MapLayerData.Add(data);
                }
            }
        }

        #endregion

        #region Method Region

        #endregion
    }
}
