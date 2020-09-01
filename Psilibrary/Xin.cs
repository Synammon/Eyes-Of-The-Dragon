using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Psilibrary
{
    #region Enumeration Region
    public enum MouseButton { None, Left, Right, Middle, X1, X2 };
    #endregion

    public sealed class Xin : GameComponent
    {
        #region General Region

        private static void SetStates()
        {
            _lastKeyboardState = _keyboardState;
            _lastMouseState = _mouseState;

            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

        }

        #endregion

        public Xin(Game game)
            : base(game)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }

        public sealed override void Initialize()
        {
            base.Initialize();
        }

        public sealed override void Update(GameTime gameTime)
        {
            SetStates();
            base.Update(gameTime);
        }

        public static bool WasKeyReleased(Keys key)
        {
            if (_keyboardState.IsKeyUp(key) &&
                _lastKeyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        public static bool WasKeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyUp(key);
        }

        #region Mouse Region

        #region Mouse Event Region

        ///<summary>
        /// Event fired when a mouse button is down
        ///</summary>

        private void CheckMouseDown()
        {
        }

        private void CheckMouseUp()
        {
        }

        private void CheckMouseMove()
        {
        }

        private void CheckMouseClick()
        {
        }

        #endregion


        static MouseState _mouseState;
        static MouseState _lastMouseState;

        public static MouseState MouseState
        {
            get { return _mouseState; }
        }

        public static MouseState LastMouseState
        {
            get { return _lastMouseState; }
        }

        public static Point MouseAsPoint
        {
            get { return new Point(_mouseState.X, _mouseState.Y); }
        }

        public static Vector2 MouseAsVector2
        {
            get { return new Vector2(_mouseState.X, _mouseState.Y); }
        }

        public static Point LastMouseAsPoint
        {
            get { return new Point(_lastMouseState.X, _lastMouseState.Y); }
        }

        public static Vector2 LastMouseAsVector2
        {
            get { return new Vector2(_lastMouseState.X, _lastMouseState.Y); }
        }

        public static bool CheckMousePress(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = CheckLeftButtonPressed();
                    break;
                case MouseButton.Right:
                    result = CheckRightButtonPressed();
                    break;
                case MouseButton.Middle:
                    result = CheckMiddleButtonPressed();
                    break;
                case MouseButton.X1:
                    result = CheckXButton1Pressed();
                    break;
                case MouseButton.X2:
                    result = CheckXButton2Pressed();
                    break;
            }
            return result;
        }

        public static bool CheckMouseReleased(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = CheckLeftButtonReleased();
                    break;
                case MouseButton.Right:
                    result = CheckRightButtonReleased();
                    break;
                case MouseButton.Middle:
                    result = CheckMiddleButtonReleased();
                    break;
                case MouseButton.X1:
                    result = CheckXButton1Released();
                    break;
                case MouseButton.X2:
                    result = CheckXButton2Released();
                    break;
            }
            return result;
        }

        public static bool IsMouseDown(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.None:
                    break;
                case MouseButton.Left:
                    result = _mouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButton.Right:
                    result = _mouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButton.Middle:
                    result = _mouseState.MiddleButton == ButtonState.Pressed;
                    break;
                case MouseButton.X1:
                    result = _mouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButton.X2:
                    result = _mouseState.XButton2 == ButtonState.Pressed;
                    break;
            }
            return result;
        }

        public static bool IsMouseUp(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = _mouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButton.Right:
                    result = _mouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButton.Middle:
                    result = _mouseState.MiddleButton == ButtonState.Released;
                    break;
                case MouseButton.X1:
                    result = _mouseState.XButton1 == ButtonState.Released;
                    break;
                case MouseButton.X2:
                    result = _mouseState.XButton2 == ButtonState.Released;
                    break;
            }
            return result;
        }

        public static bool IsLastMouseDown(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = _lastMouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButton.Right:
                    result = _lastMouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButton.Middle:
                    result = _lastMouseState.MiddleButton == ButtonState.Pressed;
                    break;
                case MouseButton.X1:
                    result = _lastMouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButton.X2:
                    result = _lastMouseState.XButton2 == ButtonState.Pressed;
                    break;
            }
            return result;
        }

        public static bool LastIsMouseUp(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = _lastMouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButton.Right:
                    result = _lastMouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButton.Middle:
                    result = _lastMouseState.MiddleButton == ButtonState.Released;
                    break;
                case MouseButton.X1:
                    result = _lastMouseState.XButton1 == ButtonState.Released;
                    break;
                case MouseButton.X2:
                    result = _lastMouseState.XButton2 == ButtonState.Released;
                    break;
            }
            return result;
        }

        private static bool CheckLeftButtonReleased()
        {
            return (_mouseState.LeftButton == ButtonState.Released) &&
                (_lastMouseState.LeftButton == ButtonState.Pressed);
        }

        private static bool CheckRightButtonReleased()
        {
            return (_mouseState.RightButton == ButtonState.Released) &&
                (_lastMouseState.RightButton == ButtonState.Pressed);
        }

        private static bool CheckMiddleButtonReleased()
        {
            return (_mouseState.MiddleButton == ButtonState.Released) &&
                (_lastMouseState.MiddleButton == ButtonState.Pressed);
        }

        private static bool CheckXButton1Released()
        {
            return (_mouseState.XButton1 == ButtonState.Released) &&
                (_lastMouseState.XButton1 == ButtonState.Pressed);
        }

        private static bool CheckXButton2Released()
        {
            return (_mouseState.XButton2 == ButtonState.Released) &&
                (_lastMouseState.XButton2 == ButtonState.Pressed);
        }

        private static bool CheckLeftButtonPressed()
        {
            return (_mouseState.LeftButton == ButtonState.Pressed) &&
                (_lastMouseState.LeftButton == ButtonState.Released);
        }

        private static bool CheckRightButtonPressed()
        {
            return (_mouseState.RightButton == ButtonState.Pressed) &&
                (_lastMouseState.RightButton == ButtonState.Released);
        }

        private static bool CheckMiddleButtonPressed()
        {
            return (_mouseState.MiddleButton == ButtonState.Pressed) &&
                (_lastMouseState.MiddleButton == ButtonState.Released);
        }

        private static bool CheckXButton1Pressed()
        {
            return (_mouseState.XButton1 == ButtonState.Pressed) &&
                (_lastMouseState.XButton1 == ButtonState.Released);
        }

        private static bool CheckXButton2Pressed()
        {
            return (_mouseState.XButton2 == ButtonState.Pressed) &&
                (_lastMouseState.XButton2 == ButtonState.Released);
        }
        #endregion


        #region Keyboard Region

        #region Keyboard Event Region

        private bool CheckPressedKeys()
        {
            bool keyPressed = false;

            foreach (Keys key in _keyboardState.GetPressedKeys())
            {
                if (_lastKeyboardState.IsKeyUp(key))
                {
                    keyPressed = true;
                    break;
                }
            }
            return keyPressed;
        }

        private bool CheckReleasedKeys()
        {
            bool keyPressed = false;

            foreach (Keys key in _lastKeyboardState.GetPressedKeys())
            {
                if (_keyboardState.IsKeyUp(key))
                {
                    keyPressed = true;
                    break;
                }
            }
            return keyPressed;
        }

        #endregion

        static KeyboardState _keyboardState;
        static KeyboardState _lastKeyboardState;

        public static void FlushInput()
        {
            SetStates();
        }

        public static KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public static bool CheckKeyPress(Keys key)
        {
            return _keyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyUp(key);
        }

        public static bool CheckKeyRelease(Keys key)
        {
            return _keyboardState.IsKeyUp(key) && _lastKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return _keyboardState.IsKeyUp(key);
        }

        public static bool LastIsKeyDown(Keys key)
        {
            return _lastKeyboardState.IsKeyDown(key);
        }

        public static bool LastIsKeyUp(Keys key)
        {
            return _lastKeyboardState.IsKeyUp(key);
        }

        public static Keys[] GetPressedKeys()
        {
            return _keyboardState.GetPressedKeys();
        }

        public static Keys[] GetLastPressedKeys()
        {
            return _lastKeyboardState.GetPressedKeys();
        }

        #endregion
    }
}