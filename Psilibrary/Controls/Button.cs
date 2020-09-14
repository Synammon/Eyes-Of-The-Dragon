using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Psilibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psilibrary.Controls
{
    public class Button : Control
    {
        #region Event Region

        public event EventHandler Click;

        #endregion

        #region Field Region

        Texture2D background;
       
        #endregion

        #region Property Region
        #endregion

        #region Constructor Region  

        public Button(Texture2D background)
        {
            background = background;
        }

        #endregion

        #region Method Region

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            Rectangle destination = new Rectangle(
                (int)Position.X, 
                (int)Position.Y, 
                background.Width, 
                background.Height).Scale(scale);
            spriteBatch.Draw(background, destination, Color.White);
            spriteFont = FontManager.GetFont("interfacefont");
            Vector2 size = spriteFont.MeasureString(Text);
            Vector2 offset = new Vector2((background.Width * scale.X - size.X) / 2, (background.Height * scale.Y - size.Y) / 2);
            spriteBatch.DrawString(spriteFont, Text, (Position.Scale(scale) + offset), Color);
        }

        public override void HandleInput()
        {
            Point position = Xin.MouseAsPoint;
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            Rectangle destination = new Rectangle(
                (int)position.X, 
                (int)position.Y, 
                background.Width, 
                background.Height).Scale(scale);

            if (destination.Contains(position) && Xin.CheckMouseReleased(MouseButton.Left))
            {
                OnClick();
            }
        }

        private void OnClick()
        {
            Click?.Invoke(this, null);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
        }

        #endregion
    }
}
