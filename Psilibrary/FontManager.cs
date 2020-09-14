using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Psilibrary
{
    public class FontManager : DrawableGameComponent
    {
        private readonly static Dictionary<string, SpriteFont> fonts =
            new Dictionary<string, SpriteFont>();
        private static Game gameRef;

        public FontManager(Game game) : base(game)
        {
            gameRef = game;
        }

        protected override void LoadContent()
        {
            fonts.Add("interfacefont", Game.Content.Load<SpriteFont>(@"Fonts\interfacefont"));
            fonts.Add("interfacefontmedium", Game.Content.Load<SpriteFont>(@"Fonts\interfacefont_medium"));
            fonts.Add("interfacefonthigh", Game.Content.Load<SpriteFont>(@"Fonts\interfacefont_large"));
            fonts.Add("interfacefontultra", Game.Content.Load<SpriteFont>(@"Fonts\interfacefont_ultra"));
        }

        public static SpriteFont GetFont(string name)
        {
            if (gameRef.GraphicsDevice.Viewport.Width >= 4000)
            {
                name += "ultra";
            }
            else if (gameRef.GraphicsDevice.Viewport.Width >= 3000)
            {
                name += "high";
            }
            else if (gameRef.GraphicsDevice.Viewport.Width >= 1920)
            {
                name += "medium";
            }

            return fonts[name];
        }

        public static bool ContainsFont(string name)
        {
            if (gameRef.GraphicsDevice.Viewport.Width >= 4000)
            {
                name += "ultra";
            }
            else if (gameRef.GraphicsDevice.Viewport.Width >= 3000)
            {
                name += "high";
            }
            else if (gameRef.GraphicsDevice.Viewport.Width >= 1920)
            {
                name += "medium";
            }

            return fonts.ContainsKey(name);
        }
    }
}
