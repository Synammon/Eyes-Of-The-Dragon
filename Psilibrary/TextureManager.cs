using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary
{
    public interface ITextureManager
    {
        void AddTexture(string textureName, Texture2D texture);
        void RemoveTexture(string textureName);
        Texture2D GetTexture(string textureName);
        bool ContainsTexture(string textureName);
    }

    public class TextureManager : GameComponent, ITextureManager
    {
        private Dictionary<string, Texture2D> textures;

        public TextureManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ITextureManager), this);
        }

        public override void Initialize()
        {
            textures = new Dictionary<string, Texture2D>();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void AddTexture(string textureName, Texture2D texture)
        {
            if (!textures.ContainsKey(textureName))
            {
                textures.Add(textureName, texture);
            }
        }

        public void RemoveTexture(string textureName)
        {
            if (textures.ContainsKey(textureName))
            {
                textures.Remove(textureName);
            }
        }

        public Texture2D GetTexture(string textureName)
        {
            if (textures.ContainsKey(textureName))
                return textures[textureName];

            return null;
        }

        public bool ContainsTexture(string textureName)
        {
            return textures.ContainsKey(textureName);
        }
    }
}
