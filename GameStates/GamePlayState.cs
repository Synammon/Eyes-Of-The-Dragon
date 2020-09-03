using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Psilibrary.TileEngine;

namespace EyesOfTheDragon.GameStates
{
    public interface IGamePlayState
    {
        void SetupNewGame();
        void LoadGame();
    }

    public class GamePlayState : BaseGameState, IGamePlayState
    {
        #region Field Region

        private Camera camera;
        private Engine engine;
        private World world;

        #endregion

        #region Method Region
        #endregion

        #region Constructor Region

        public GamePlayState(Game game) : base(game)
        {
            Game.Services.AddService(typeof(IGamePlayState), this);
        }

        #endregion

        #region Method Region

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime, world.CurrentMap);
            camera.LockCamera(world.CurrentMap);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                camera.Transformation);

            world.CurrentMap.Draw(gameTime, GameRef.SpriteBatch, camera);

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetupNewGame()
        {
            world = new World();

            Tileset tileset = new Tileset(
                _content.Load<Texture2D>(@"Tiles\tilemap"),
                13,
                9,
                16,
                16);

            MapLayer baseLayer = new MapLayer(new Tile[100, 100]);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    baseLayer.SetTile(i, j, new Tile(8, 0));
                }
            }

            TileMap map = new TileMap(
                "test", 
                tileset, 
                baseLayer, 
                new CollisionLayer(), 
                new PortalLayer());

            map.PortalLayer.Portals.Add(
                "test", 
                new Portal(
                    new Point(0, 0), 
                    new Point(0, 0), 
                    "test"));

            world.AddMap("test", map);
            world.ChangeMap("test", "test");

            camera = new Camera(new Rectangle(0, 0, 1280, 720));
            engine = new Engine(32, 32);
        }

        public void LoadGame()
        {
        }
        #endregion
    }
}
