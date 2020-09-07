using EyesOfTheDragon.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary;
using Psilibrary.StateManager;
using Psilibrary.TileEngine;
using System.Collections.Generic;

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
        private readonly OptionState _optionState;
        private readonly MainMenuState _mainMenuState;
        private readonly TitleState _titleState;
        private readonly GamePlayState _gamePlayState;

        public static readonly Dictionary<string, Point> Resolutions =
            new Dictionary<string, Point>();

        private SpriteBatch spriteBatch;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public MainMenuState MainMenuState
        {
            get { return _mainMenuState; }
        }

        public TitleState TitleState
        {
            get { return _titleState; }
        }

        public OptionState OptionState
        {
            get { return _optionState; }
        }

        public GamePlayState GamePlayState
        {
            get { return _gamePlayState; }
        }

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return graphics; }
        }

        public Game1()
        {
            Settings.Load();

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Settings.Resolution.X,
                PreferredBackBufferHeight = Settings.Resolution.Y
            };

            graphics.ApplyChanges();

            foreach (var v in graphics.GraphicsDevice.Adapter.SupportedDisplayModes)
            {
                Point p = new Point(v.Width, v.Height);
                string s = v.Width + " by " + v.Height;

                if (v.Width >= 1280 && v.Height >= 720)
                {
                    Resolutions.Add(s, p);
                }
            }

            Content.RootDirectory = "Content";

            stateManager = new StateManager(this);
            Components.Add(stateManager);

            textureManager = new TextureManager(this);
            Components.Add(textureManager);

            Components.Add(new FontManager(this));
            Components.Add(new Xin(this));

            _mainMenuState = new MainMenuState(this);
            _titleState = new TitleState(this);
            _optionState = new OptionState(this);
            _gamePlayState = new GamePlayState(this);

            stateManager.ChangeState(_titleState);

            IsMouseVisible = true;
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

            textureManager.AddTexture(
                "title-screen", 
                Content.Load<Texture2D>(@"Backgrounds\title-screen"));
            textureManager.AddTexture(
                "blue-button",
                Content.Load<Texture2D>(@"GUI\g9202"));
            textureManager.AddTexture(
                "green-button",
                Content.Load<Texture2D>(@"GUI\g9236"));
            textureManager.AddTexture(
                "grey-button",
                Content.Load<Texture2D>(@"GUI\g9254"));

            // TODO: use this.Content to load your game content here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
