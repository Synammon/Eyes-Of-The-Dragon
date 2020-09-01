using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary;
using Psilibrary.StateManager;
using Psilibrary.TileEngine;

namespace EyesOfTheDragon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly StateManager stateManager;
        private readonly TextureManager textureManager;
        private SpriteBatch spriteBatch;

        private Camera camera;
        private Engine engine;
        private World world;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            stateManager = new StateManager(this);
            Components.Add(stateManager);

            textureManager = new TextureManager(this);
            Components.Add(textureManager);

            Components.Add(new Xin(this));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            world = new World();
            Tileset tileset = new Tileset(
                Content.Load<Texture2D>(@"Tiles\tilemap"),
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
            TileMap map = new TileMap("test", tileset, baseLayer, new CollisionLayer(), new PortalLayer());
            map.PortalLayer.Portals.Add("test", new Portal(new Point(0, 0), new Point(0, 0), "test"));
            world.AddMap("test", map);
            world.ChangeMap("test", "test");
            camera = new Camera(new Rectangle(0, 0, 1280, 720));
            engine = new Engine(32, 32);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.Update(gameTime, world.CurrentMap);
            camera.LockCamera(world.CurrentMap);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                camera.Transformation);

            world.CurrentMap.Draw(gameTime, spriteBatch, camera);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
