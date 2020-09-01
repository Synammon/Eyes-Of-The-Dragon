using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary.StateManager
{
    public interface IGameState
    {
        GameState Tag { get; }
    }

    public abstract partial class GameState : DrawableGameComponent, IGameState
    {
        #region Field Region

        private ITextureManager _textures;
        protected GameState _tag;
        protected readonly IStateManager _manager;
        protected ContentManager _content;
        protected readonly List<GameComponent> _childComponents;

        #endregion

        #region Property Region

        public List<GameComponent> Components
        {
            get { return _childComponents; }
        }

        public GameState Tag
        {
            get { return _tag; }
        }

        public ITextureManager TextureManager
        {
            get { return _textures; }
        }

        #endregion

        #region Constructor Region

        public GameState(Game game)
            : base(game)
        {
            _tag = this;

            _childComponents = new List<GameComponent>();
            _content = Game.Content;

            _manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
            _textures = (ITextureManager)Game.Services.GetService(typeof(ITextureManager));
        }

        #endregion

        #region Method Region

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in _childComponents)
                if (component.Enabled)
                    component.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (GameComponent component in _childComponents)
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                    ((DrawableGameComponent)component).Draw(gameTime);
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (_manager.CurrentState == _tag)
                Show();
            else
                Hide();
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

        #endregion
    }
}
