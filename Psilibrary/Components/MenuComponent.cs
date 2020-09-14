using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary.Components
{
    public class MenuComponent
    {
        #region Fields

        private SpriteFont spriteFont;

        private readonly List<string> menuItems = new List<string>();
        private int selectedIndex = -1;
        private bool mouseOver;

        private int width;
        private int height;

        private Color normalColor = Color.White;
        private Color hiliteColor = Color.Red;

        private Texture2D selected;
        private Texture2D texture;

        private Vector2 position;

        #endregion Fields

        #region Properties

        public Vector2 Postion
        {
            get { return position; }
            set { position = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = (int)MathHelper.Clamp(
                        value,
                        0,
                        menuItems.Count - 1);
            }
        }

        public Color NormalColor
        {
            get { return normalColor; }
            set { normalColor = value; }
        }

        public Color HiliteColor
        {
            get { return hiliteColor; }
            set { hiliteColor = value; }
        }

        public bool MouseOver
        {
            get { return mouseOver; }
        }

        #endregion Properties

        #region Constructors

        public MenuComponent(SpriteFont spriteFont, Texture2D selected, Texture2D texture)
        {
            this.mouseOver = false;
            this.selected = selected;
            this.spriteFont = spriteFont;
            this.texture = texture;
        }

        public MenuComponent(SpriteFont spriteFont, Texture2D selected, Texture2D texture, string[] menuItems)
            : this(spriteFont, selected, texture)
        {
            selectedIndex = 0;

            foreach (string s in menuItems)
            {
                this.menuItems.Add(s);
            }

            MeassureMenu();
        }

        #endregion Constructors

        #region Methods

        public void SetMenuItems(string[] items)
        {
            menuItems.Clear();
            menuItems.AddRange(items);
            MeassureMenu();

            selectedIndex = 0;
        }

        private void MeassureMenu()
        {
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            spriteFont = FontManager.GetFont("interfacefont");

            width = texture.Width * (int)scale.X;
            height = 0;

            foreach (string s in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(s);

                if (size.X > width)
                    width = (int)size.X;

                height += texture.Height * (int)scale.Y + 50;
            }

            height -= 50;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 menuPosition = position;
            Point p = Xin.MouseAsPoint;
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            Rectangle buttonRect;
            mouseOver = false;

            for (int i = 0; i < menuItems.Count; i++)
            {
                buttonRect = new Rectangle(
                    (int)menuPosition.X, 
                    (int)menuPosition.Y, 
                    texture.Width, 
                    texture.Height).Scale(scale);

                if (buttonRect.Contains(p))
                {
                    selectedIndex = i;
                    mouseOver = true;
                }

                menuPosition.Y += texture.Height + 50;
            }

            if (!mouseOver && Xin.CheckKeyPress(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Count - 1;
            }
            else if (!mouseOver && Xin.CheckKeyPress(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex > menuItems.Count - 1)
                    selectedIndex = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 menuPosition = position;
            Vector2 selectedPosition = new Vector2();
            Color myColor;
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            spriteFont = FontManager.GetFont("interfacefont");

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    myColor = HiliteColor;
                    selectedPosition.X = menuPosition.X - 35;
                    selectedPosition.Y = menuPosition.Y;
                }
                else
                    myColor = NormalColor;

                Rectangle destination = new Rectangle(
                    (int)menuPosition.X,
                    (int)menuPosition.Y,
                    selected.Width,
                    selected.Height).Scale(scale);

                if (i == SelectedIndex)
                    spriteBatch.Draw(selected, destination, Color.White);
                else
                    spriteBatch.Draw(texture, destination, Color.White);

                Vector2 textSize = spriteFont.MeasureString(menuItems[i]);

                Vector2 textPosition = menuPosition + new Vector2(
                    (texture.Width * scale.X - textSize.X) / 2, 
                    (texture.Height * scale.Y - textSize.Y) / 2);
                spriteBatch.DrawString(spriteFont,
                    menuItems[i],
                    textPosition.Scale(scale),
                    myColor);

                menuPosition.Y += texture.Height + 50;
            }
        }

        #endregion Methods

        #region Virtual Methods
        #endregion Virtual Methods

    }
}
