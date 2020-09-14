using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary;

namespace EyesOfTheDragon.GameStates
{
    public interface ITitleState
    {
    }

    public class TitleState : BaseGameState, ITitleState
    {
        #region Field Region

        private TextureManager textureManager;
        private SpriteFont font;
        private Rectangle destination;
        private bool mouseOver;

        #endregion

        #region Property Region
        #endregion

        #region Construtor Region

        public TitleState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ITitleState), this);
            textureManager = (TextureManager)game.Services.GetService(typeof(ITextureManager));
        }

        #endregion

        #region Method Region

        protected override void LoadContent()
        {
            font = content.Load<SpriteFont>(@"Fonts\InterfaceFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if ((Xin.CheckMouseReleased(MouseButton.Left) && mouseOver) ||
                Xin.CheckKeyRelease(Keys.Enter))
            {
                manager.ChangeState(GameRef.MainMenuState);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            GameRef.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            Vector2 size = font.MeasureString("PRESS ENTER TO BEGIN");

            GameRef.SpriteBatch.Draw(
                textureManager.GetTexture("title-screen"), 
                new Rectangle(0, 0, Settings.Resolution.X, Settings.Resolution.Y), 
                Color.White);

            Vector2 position = new Vector2((1280 - size.X) / 2, 620).Scale(scale);
            destination = new Rectangle(
                (int)position.X - 10,
                (int)position.Y - 20,
                (int)(size.X + 20),
                (int)(size.Y + 40));

            if (destination.Contains(Xin.MouseAsPoint))
            {
                mouseOver = true;
            }

            GameRef.SpriteBatch.Draw(
                textureManager.GetTexture("blue-button"),
                destination,
                null,
                Color.White);

            GameRef.SpriteBatch.DrawString(
                font,
                "PRESS ENTER TO BEGIN",
                position,
                Color.White);

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }
        #endregion
    }
}
