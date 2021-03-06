﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Psilibrary;
using Psilibrary.Components;
using Psilibrary.StateManager;

namespace EyesOfTheDragon.GameStates
{
    public interface IMainMenuState
    {
    }

    public class MainMenuState : BaseGameState, IMainMenuState
    {
        #region Field Region

        private MenuComponent menu;
        private readonly ITextureManager textureManager;

        #endregion

        #region Property Region
        #endregion

        #region Constructor Region

        public MainMenuState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IMainMenuState), this);
            textureManager = 
                (ITextureManager)game.Services.GetService(typeof(ITextureManager));
        }

        #endregion

        #region Method Region

        protected override void LoadContent()
        {
            menu = new MenuComponent(
                content.Load<SpriteFont>(@"Fonts\InterfaceFont"),
                textureManager.GetTexture("green-button"),
                textureManager.GetTexture("blue-button"),
                new[]{ "New Game", "Continue", "Options", "Credits", "Exit" });
            menu.Postion = new Vector2(1100, 50);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);

            if (menu.MouseOver && Xin.CheckMouseReleased(MouseButton.Left))
            {
                switch (menu.SelectedIndex)
                {
                    case 0:
                        IGamePlayState gamePlayState =
                            (IGamePlayState)Game.Services.GetService(
                                typeof(IGamePlayState));
                        gamePlayState.SetupNewGame();
                        manager.PushState((GameState)gamePlayState);
                        break;
                    case 1:
                        break;
                    case 2:
                        manager.PushState(GameRef.OptionState);
                        break;
                    case 3:
                        break;
                    case 4:
                        GameRef.Exit();
                        break;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(
                textureManager.GetTexture("title-screen"), 
                new Rectangle(0, 0, 1280, 720), 
                Color.White);

            menu.Draw(gameTime, GameRef.SpriteBatch);

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }
        #endregion
    }
}
