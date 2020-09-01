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

        private string _name;
        private List<Tileset> _tilesets;
        private List<ILayer> _mapLayers;
        private CollisionLayer _collisionLayer;
        private Dictionary<string, Portal> _spawnPositions;
        private PortalLayer _portalLayer;

        private int _mapWidth;
        private int _mapHeight;

        #endregion

        #region Property Region

        public string Name
        {
            get { return _name; }
        }

        public PortalLayer PortalLayer 
        {
            get { return _portalLayer; }
        }

        public CollisionLayer CollisionLayer
        {
            get { return _collisionLayer; }
            set { _collisionLayer = value; }
        }

        public Dictionary<string, Portal> SpawnPositions
        {
            get { return _spawnPositions; }
            private set { _spawnPositions = value; }
        }

        public int WidthInPixels
        {
            get { return _mapWidth * Engine.TileWidth; }
        }

        public int HeightInPixels
        {
            get { return _mapHeight * Engine.TileHeight; }
        }

        #endregion

        #region Constructor Region

        public TileMap(string name, List<Tileset> tilesets, MapLayer baseLayer, MapLayer buildingLayer, MapLayer splatterLayer, CollisionLayer collisionLayer, PortalLayer portalLayer)
        {
            this._name = name;
            this._tilesets = tilesets;
            this._mapLayers = new List<ILayer>();

            this._portalLayer = portalLayer;

            _mapLayers.Add(baseLayer);

            AddLayer(buildingLayer);
            AddLayer(splatterLayer);

            _mapWidth = baseLayer.Width;
            _mapHeight = baseLayer.Height;

            CollisionLayer = collisionLayer;
            _spawnPositions = new Dictionary<string, Portal>();
        }

        public TileMap(string name, Tileset tileset, MapLayer baseLayer, CollisionLayer collisionLayer, PortalLayer portalLayer)
        {
            this._name = name;
            _tilesets = new List<Tileset>();
            _tilesets.Add(tileset);

            this._portalLayer = portalLayer;

            _mapLayers = new List<ILayer>();
            _mapLayers.Add(baseLayer);

            _mapWidth = baseLayer.Width;
            _mapHeight = baseLayer.Height;

            CollisionLayer = collisionLayer;
            SpawnPositions = new Dictionary<string, Portal>();

        }

        #endregion

        #region Method Region

        public void AddLayer(ILayer layer)
        {
            if (layer is MapLayer)
            {
                if (!(((MapLayer)layer).Width == _mapWidth && ((MapLayer)layer).Height == _mapHeight))
                    throw new Exception("Map layer size exception");
            }

            _mapLayers.Add(layer);
        }

        public void AddTileset(Tileset tileset)
        {
            _tilesets.Add(tileset);
        }

        public void Update(GameTime gameTime)
        {
            foreach (ILayer layer in _mapLayers)
            {
                layer.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (ILayer layer in _mapLayers)
            {
                if (layer is MapLayer)
                    ((MapLayer)layer).Draw(spriteBatch, camera, _tilesets);
                //else if (layer is ItemLayer)
                //    ((ItemLayer)layer).Draw(spriteBatch, camera, _tilesets);
            }
        }

        #endregion

        public void Resize(List<ILayer> layers)
        {
            _mapWidth = ((MapLayer)layers[0]).Width;
            _mapHeight = ((MapLayer)layers[0]).Height;

            _mapLayers.Clear();

            foreach (ILayer layer in layers)
                _mapLayers.Add(layer);
        }
    }
}
