using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Psilibrary.TileEngine
{
    public class TileMap
    {
        #region Field Region

        private string name;
        private List<Tileset> tilesets;
        private List<ILayer> mapLayers;
        private CollisionLayer collisionLayer;
        private Dictionary<string, Portal> spawnPositions;
        private PortalLayer portalLayer;

        private int mapWidth;
        private int mapHeight;

        #endregion

        #region Property Region

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public List<Tileset> Tilesets
        {
            get { return tilesets; }
            private set { tilesets = value; }
        }

        public List<ILayer> Layers
        {
            get { return mapLayers; }
            set { mapLayers = value; }
        }

        public PortalLayer PortalLayer 
        {
            get { return portalLayer; }
            private set { portalLayer = value; }
        }

        public CollisionLayer CollisionLayer
        {
            get { return collisionLayer; }
            set { collisionLayer = value; }
        }

        public Dictionary<string, Portal> SpawnPositions
        {
            get { return spawnPositions; }
            private set { spawnPositions = value; }
        }

        public int WidthInPixels
        {
            get { return mapWidth * Engine.TileWidth; }
        }

        public int HeightInPixels
        {
            get { return mapHeight * Engine.TileHeight; }
        }

        #endregion

        #region Constructor Region

        private TileMap()
        {

        }

        public TileMap(string name, List<Tileset> tilesets, MapLayer baseLayer, MapLayer buildingLayer, MapLayer splatterLayer, CollisionLayer collisionLayer, PortalLayer portalLayer)
        {
            this.name = name;
            this.tilesets = tilesets;
            this.mapLayers = new List<ILayer>();

            this.portalLayer = portalLayer;

            mapWidth = baseLayer.Width;
            mapHeight = baseLayer.Height;

            mapLayers.Add(baseLayer);

            AddLayer(buildingLayer);
            AddLayer(splatterLayer);

            CollisionLayer = collisionLayer;
            spawnPositions = new Dictionary<string, Portal>();
        }

        public void SetTile(int selectedIndex1, int x, int y, decimal value, int selectedIndex2)
        {
            if (mapLayers[selectedIndex1] is MapLayer layer)
            {
                layer.SetTile(x, y, new Tile((int)value, selectedIndex2));
            }
        }

        public TileMap(string name, Tileset tileset, MapLayer baseLayer, CollisionLayer collisionLayer, PortalLayer portalLayer)
        {
            this.name = name;
            tilesets = new List<Tileset>();
            tilesets.Add(tileset);

            this.portalLayer = portalLayer;

            mapLayers = new List<ILayer>();
            mapLayers.Add(baseLayer);

            mapWidth = baseLayer.Width;
            mapHeight = baseLayer.Height;

            CollisionLayer = collisionLayer;
            SpawnPositions = new Dictionary<string, Portal>();

        }

        #endregion

        #region Method Region

        public void AddLayer(ILayer layer)
        {
            if (layer is MapLayer)
            {
                if (!(((MapLayer)layer).Width == mapWidth && ((MapLayer)layer).Height == mapHeight))
                    throw new Exception("Map layer size exception");
            }

            mapLayers.Add(layer);
        }

        public void AddTileset(Tileset tileset)
        {
            tilesets.Add(tileset);
        }

        public void Update(GameTime gameTime)
        {
            foreach (ILayer layer in mapLayers)
            {
                layer.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (ILayer layer in mapLayers)
            {
                if (layer is MapLayer)
                    ((MapLayer)layer).Draw(spriteBatch, camera, tilesets);
                //else if (layer is ItemLayer)
                //    ((ItemLayer)layer).Draw(spriteBatch, camera, tilesets);
            }
        }

        #endregion

        public void Resize(List<ILayer> layers)
        {
            mapWidth = ((MapLayer)layers[0]).Width;
            mapHeight = ((MapLayer)layers[0]).Height;

            mapLayers.Clear();

            foreach (ILayer layer in layers)
                mapLayers.Add(layer);
        }
    }
}
