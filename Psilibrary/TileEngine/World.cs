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

        private Dictionary<string, TileMap> maps;

        private string currentMapName;
        
        #endregion

        #region Property region

        [ContentSerializer]
        public Dictionary<string, TileMap> Maps
        {
            get { return maps; }
            private set { maps = value; }
        }

        [ContentSerializer]
        public string CurrentMapName
        {
            get { return currentMapName; }
            private set { currentMapName = value; }
        }

        public TileMap CurrentMap
        {
            get { return maps[currentMapName]; }
        }

        #endregion

        #region Constructor Region

        public World()
        {
            maps = new Dictionary<string, TileMap>();
        }

        #endregion

        #region Method Region

        public void AddMap(string mapName, TileMap map)
        {
            if (!maps.ContainsKey(mapName))
                maps.Add(mapName, map);

            CurrentMapName = mapName;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera, bool debug = false)
        {
            CurrentMap.Draw(gameTime, spriteBatch, camera);
        }

        public Portal ChangeMap(string mapName, string portalName)
        {
            if (maps.ContainsKey(mapName) && maps[mapName].PortalLayer.Portals.ContainsKey(portalName))
            {
                currentMapName = mapName;

                return CurrentMap.PortalLayer.Portals[portalName];
            }

            throw new Exception("Map name or portal name not found.");
        }

        #endregion
    }
}
