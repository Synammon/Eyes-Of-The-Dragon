using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyesOfTheDragon.PlayerComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary;
using Psilibrary.CharacterComponents;
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
        private static Player player;

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
            player = new Player(
                Game, 
                "Alyssa", 
                true, 
                Character.FromDefinitioin(new PriestHeroDefinition(Game), ""));
            Components.Add(player);

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

            Vector2 motion = Vector2.Zero;
            if (Xin.IsKeyDown(Keys.S))
            {
                motion.Y = 1;
                player.Sprite.IsAnimating = true;
                player.Sprite.CurrentAnimation = AnimationKey.WalkUp;
            }

            if (Xin.IsKeyDown(Keys.W))
            {
                motion.Y = -1;
                player.Sprite.IsAnimating = true;
                player.Sprite.CurrentAnimation = AnimationKey.WalkDown;
            }

            if (Xin.IsKeyDown(Keys.A))
            {
                motion.X = -1;
                player.Sprite.IsAnimating = true;
                player.Sprite.CurrentAnimation = AnimationKey.WalkLeft;
            }

            if (Xin.IsKeyDown(Keys.D))
            {
                motion.X = 1;
                player.Sprite.IsAnimating = true;
                player.Sprite.CurrentAnimation = AnimationKey.WalkRight;
            }

            if (Xin.IsMouseDown(MouseButton.Right))
            {
                Vector2 position = camera.Position + Xin.MouseAsVector2;

                if (position.X < player.Sprite.Position.X && 
                    position.Y > player.Sprite.Position.Y &&
                    position.Y < player.Sprite.Position.Y + Engine.TileHeight)
                {
                    motion.X = -1;
                    player.Sprite.IsAnimating = true;
                    player.Sprite.CurrentAnimation = AnimationKey.WalkLeft;
                }
                else if (position.X > player.Sprite.Position.X &&
                    position.Y > player.Sprite.Position.Y &&
                    position.Y < player.Sprite.Position.Y + Engine.TileHeight)
                {
                    motion.X = 1;
                    player.Sprite.IsAnimating = true;
                    player.Sprite.CurrentAnimation = AnimationKey.WalkRight;
                }

                if (position.Y < player.Sprite.Position.Y &&
                    position.X > player.Sprite.Position.X &&
                    position.X < player.Sprite.Position.X + Engine.TileWidth)
                {
                    motion.Y = -1;
                    player.Sprite.IsAnimating = true;
                    player.Sprite.CurrentAnimation = AnimationKey.WalkDown;
                }
                else if (position.Y > player.Sprite.Position.Y &&
                    position.X > player.Sprite.Position.X &&
                    position.X < player.Sprite.Position.X + Engine.TileWidth)
                {
                    motion.Y = 1;
                    player.Sprite.IsAnimating = true;
                    player.Sprite.CurrentAnimation = AnimationKey.WalkUp;
                }
            }
            if (motion != Vector2.Zero)
            {
                motion.Normalize();
                player.Sprite.Position += motion * player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                player.Sprite.LockToMap(new Point(world.CurrentMap.WidthInPixels, world.CurrentMap.HeightInPixels));
                camera.LockToSprite(player.Sprite, world.CurrentMap);
            }
            else
            {
                player.Sprite.IsAnimating = false;
            }

            
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

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }

        public void SetupNewGame()
        {
            world = new World();

            Tileset tileset = new Tileset(
                content.Load<Texture2D>(@"Tiles\tilemap"),
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
            engine = new Engine(64, 64);
        }

        public void LoadGame()
        {
        }
        #endregion
    }
}
