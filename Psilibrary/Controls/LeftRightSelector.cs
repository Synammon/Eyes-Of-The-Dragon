using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psilibrary.Controls
{
    public class LeftRightSelector : Control
    {
        #region Event Region

        public event EventHandler SelectionChanged;

        #endregion

        #region Field Region

        private List<string> items = new List<string>();

        private Texture2D leftTexture;
        private Texture2D rightTexture;
        private Texture2D stopTexture;

        private Color selectedColor = Color.Red;
        private int maxItemWidth;
        private int selectedItem;
        private Rectangle leftSide = new Rectangle();
        private Rectangle rightSide = new Rectangle();
        private int yOffset;

        #endregion

        #region Property Region

        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public int SelectedIndex
        {
            get { return selectedItem; }
            set { selectedItem = (int)MathHelper.Clamp(value, 0f, items.Count); }
        }

        public string SelectedItem
        {
            get { return Items[selectedItem]; }
        }

        public List<string> Items
        {
            get { return items; }
        }

        public int MaxItemWidth
        {
            get { return maxItemWidth; }
            set { maxItemWidth = value; }
        }

        #endregion

        #region Constructor Region

        public LeftRightSelector(Texture2D leftArrow, Texture2D rightArrow, Texture2D stop)
        {
            leftTexture = leftArrow;
            rightTexture = rightArrow;
            stopTexture = stop;
            TabStop = true;
            Color = Color.White;
        }

        #endregion

        #region Method Region

        public void SetItems(string[] items, int maxWidth)
        {
            this.items.Clear();

            foreach (string s in items)
                this.items.Add(s);

            maxItemWidth = maxWidth;
        }

        protected void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(this, null);
        }

        #endregion

        #region Abstract Method Region

        public override void Update(GameTime gameTime)
        {
            HandleMouseInput();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 drawTo = position;
            Vector2 scale = new Vector2(
                Settings.Resolution.X / 1280,
                Settings.Resolution.Y / 720);

            spriteFont = FontManager.GetFont("interfacefont");

            yOffset = (int)((leftTexture.Height - spriteFont.MeasureString("W").Y) / 2 * scale.Y);
            leftSide = new Rectangle(
                (int)position.X, 
                (int)position.Y, 
                leftTexture.Width, 
                leftTexture.Height).Scale(scale);

            //if (selectedItem != 0)
            spriteBatch.Draw(leftTexture, leftSide, Color.White);
            //else
            //    spriteBatch.Draw(stopTexture, drawTo, Color.White);

            drawTo.X += leftTexture.Width + 5f;

            float itemWidth = spriteFont.MeasureString(items[selectedItem]).X;
            float offset = (maxItemWidth - itemWidth) / 2 * scale.X;

            drawTo.X += offset;
            drawTo.Y += yOffset;

            if (hasFocus)
                spriteBatch.DrawString(spriteFont, items[selectedItem], drawTo.Scale(scale), selectedColor);
            else
                spriteBatch.DrawString(spriteFont, items[selectedItem], drawTo.Scale(scale), Color);

            drawTo.X += -1 * offset + maxItemWidth + 5f;

            rightSide = new Rectangle((int)drawTo.X, (int)drawTo.Y - yOffset, rightTexture.Width, rightTexture.Height).Scale(scale);
            //if (selectedItem != items.Count - 1)
            spriteBatch.Draw(rightTexture, rightSide, Color.White);
            //else
            //    spriteBatch.Draw(stopTexture, drawTo, Color.White);
        }

        public override void HandleInput()
        {
            if (items.Count == 0)
                return;

            if (Xin.WasKeyReleased(Keys.Left))
            {
                selectedItem--;
                if (selectedItem < 0)
                    selectedItem = this.Items.Count - 1;
                OnSelectionChanged();
            }

            if (Xin.WasKeyReleased(Keys.Right))
            {
                selectedItem++;
                if (selectedItem >= items.Count)
                    selectedItem = 0;
                OnSelectionChanged();
            }
        }

        private void HandleMouseInput()
        {
            if (Xin.CheckMouseReleased(MouseButton.Left))
            {
                Point mouse = Xin.MouseAsPoint;

                if (leftSide.Contains(mouse))
                {
                    selectedItem--;
                    if (selectedItem < 0)
                        selectedItem = this.Items.Count - 1;
                    OnSelectionChanged();
                }

                if (rightSide.Contains(mouse))
                {
                    selectedItem++;
                    if (selectedItem >= items.Count)
                        selectedItem = 0;
                    OnSelectionChanged();
                }
            }
        }

        #endregion
    }
}
