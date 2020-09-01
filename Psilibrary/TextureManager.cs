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
        private Dictionary<string, Texture2D> _textures;

        public TextureManager(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ITextureManager), this);
        }

        public override void Initialize()
        {
            _textures = new Dictionary<string, Texture2D>();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void AddTexture(string textureName, Texture2D texture)
        {
            if (!_textures.ContainsKey(textureName))
            {
                _textures.Add(textureName, texture);
            }
        }

        public void RemoveTexture(string textureName)
        {
            if (_textures.ContainsKey(textureName))
            {
                _textures.Remove(textureName);
            }
        }

        public Texture2D GetTexture(string textureName)
        {
            if (_textures.ContainsKey(textureName))
                return _textures[textureName];

            return null;
        }

        public bool ContainsTexture(string textureName)
        {
            return _textures.ContainsKey(textureName);
        }
    }
}
