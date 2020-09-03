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

        private readonly List<string> _menuItems = new List<string>();
        private int _selectedIndex = -1;
        private bool _mouseOver;

        private int _width;
        private int _height;

        private Color _normalColor = Color.White;
        private Color _hiliteColor = Color.Red;

        private Texture2D _selected;
        private Texture2D _texture;

        private Vector2 _position;

        #endregion Fields

        #region Properties

        public Vector2 Postion
        {
            get { return _position; }
            set { _position = value; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = (int)MathHelper.Clamp(
                        value,
                        0,
                        _menuItems.Count - 1);
            }
        }

        public Color NormalColor
        {
            get { return _normalColor; }
            set { _normalColor = value; }
        }

        public Color HiliteColor
        {
            get { return _hiliteColor; }
            set { _hiliteColor = value; }
        }

        public bool MouseOver
        {
            get { return _mouseOver; }
        }

        #endregion Properties

        #region Constructors

        public MenuComponent(SpriteFont spriteFont, Texture2D selected, Texture2D texture)
        {
            this._mouseOver = false;
            this._selected = selected;
            this.spriteFont = spriteFont;
            this._texture = texture;
        }

        public MenuComponent(SpriteFont spriteFont, Texture2D selected, Texture2D texture, string[] menuItems)
            : this(spriteFont, selected, texture)
        {
            _selectedIndex = 0;

            foreach (string s in menuItems)
            {
                this._menuItems.Add(s);
            }

            MeassureMenu();
        }

        #endregion Constructors

        #region Methods

        public void SetMenuItems(string[] items)
        {
            _menuItems.Clear();
            _menuItems.AddRange(items);
            MeassureMenu();

            _selectedIndex = 0;
        }

        private void MeassureMenu()
        {
            _width = _texture.Width;
            _height = 0;

            foreach (string s in _menuItems)
            {
                Vector2 size = spriteFont.MeasureString(s);

                if (size.X > _width)
                    _width = (int)size.X;

                _height += _texture.Height + 50;
            }

            _height -= 50;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 menuPosition = _position;
            Point p = Xin.MouseAsPoint;

            Rectangle buttonRect;

            for (int i = 0; i < _menuItems.Count; i++)
            {
                buttonRect = new Rectangle((int)menuPosition.X, (int)menuPosition.Y, _texture.Width, _texture.Height);

                if (buttonRect.Contains(p))
                {
                    _selectedIndex = i;
                    _mouseOver = true;
                }

                menuPosition.Y += _texture.Height + 50;
            }

            if (!_mouseOver && Xin.CheckKeyPress(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0)
                    _selectedIndex = _menuItems.Count - 1;
            }
            else if (!_mouseOver && Xin.CheckKeyPress(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex > _menuItems.Count - 1)
                    _selectedIndex = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 menuPosition = _position;
            Vector2 selectedPosition = new Vector2();
            Color myColor;

            for (int i = 0; i < _menuItems.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    myColor = HiliteColor;
                    selectedPosition.X = menuPosition.X - 35;
                    selectedPosition.Y = menuPosition.Y;
                }
                else
                    myColor = NormalColor;

                if (i == SelectedIndex)
                    spriteBatch.Draw(_selected, menuPosition, Color.White);
                else
                    spriteBatch.Draw(_texture, menuPosition, Color.White);

                Vector2 textSize = spriteFont.MeasureString(_menuItems[i]);

                Vector2 textPosition = menuPosition + new Vector2((int)(_texture.Width - textSize.X) / 2, (int)(_texture.Height - textSize.Y) / 2);
                spriteBatch.DrawString(spriteFont,
                    _menuItems[i],
                    textPosition,
                    myColor);

                menuPosition.Y += _texture.Height + 50;
            }
        }

        #endregion Methods

        #region Virtual Methods
        #endregion Virtual Methods

    }
}
