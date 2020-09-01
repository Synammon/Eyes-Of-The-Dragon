using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Psilibrary.TileEngine
{
    public class World
    {
        #region Field Region

        private Dictionary<string, TileMap> _maps;

        private string _currentMapName;
        
        #endregion

        #region Property region

        [ContentSerializer]
        public Dictionary<string, TileMap> Maps
        {
            get { return _maps; }
            private set { _maps = value; }
        }

        [ContentSerializer]
        public string CurrentMapName
        {
            get { return _currentMapName; }
            private set { _currentMapName = value; }
        }

        public TileMap CurrentMap
        {
            get { return _maps[_currentMapName]; }
        }

        #endregion

        #region Constructor Region

        public World()
        {
            _maps = new Dictionary<string, TileMap>();
        }

        #endregion

        #region Method Region

        public void AddMap(string mapName, TileMap map)
        {
            if (!_maps.ContainsKey(mapName))
                _maps.Add(mapName, map);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera, bool debug = false)
        {
            CurrentMap.Draw(gameTime, spriteBatch, camera);
        }

        public Portal ChangeMap(string mapName, string portalName)
        {
            if (_maps.ContainsKey(mapName) && _maps[mapName].PortalLayer.Portals.ContainsKey(portalName))
            {
                _currentMapName = mapName;

                return CurrentMap.PortalLayer.Portals[portalName];
            }

            throw new Exception("Map name or portal name not found.");
        }

        #endregion
    }
}
