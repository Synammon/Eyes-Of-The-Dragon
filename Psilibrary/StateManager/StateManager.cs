using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Psilibrary.StateManager
{
    public interface IStateManager
    {
        GameState CurrentState { get; }

        event EventHandler StateChanged;

        void PushState(GameState state);
        void ChangeState(GameState state);
        void PopState();
        bool ContainsState(GameState state);
    }

    public class StateManager : GameComponent, IStateManager
    {
        #region Field Region

        private readonly Stack<GameState> _gameStates = new Stack<GameState>();

        private const int _startDrawOrder = 5000;
        private const int _drawOrderInc = 50;
        private int _drawOrder;

        #endregion

        #region Event Handler Region

        public event EventHandler StateChanged;

        #endregion

        #region Property Region

        public GameState CurrentState
        {
            get { return _gameStates.Peek(); }
        }

        #endregion

        #region Constructor Region

        public StateManager(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this);
        }

        #endregion

        #region Method Region

        public void PushState(GameState state)
        {
            AddState(state);
            OnStateChanged();
        }

        private void AddState(GameState state)
        {
            _drawOrder += _drawOrderInc;
            state.DrawOrder = _drawOrder;

            _gameStates.Push(state);
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (_gameStates.Count != 0)
            {
                RemoveState();
                OnStateChanged();
            }
        }

        private void RemoveState()
        {
            GameState state = _gameStates.Peek();
            _drawOrder -= _drawOrderInc;
            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void ChangeState(GameState state)
        {
            while (_gameStates.Count > 0)
                RemoveState();

            _drawOrder = _startDrawOrder;
            state.DrawOrder = _drawOrder;
            _drawOrder += _drawOrderInc;

            AddState(state);
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return _gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, null);
        }

        #endregion
    }
}
