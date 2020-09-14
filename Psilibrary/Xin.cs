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
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

        }

        #endregion

        public Xin(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
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
            if (keyboardState.IsKeyUp(key) &&
                lastKeyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        public static bool WasKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
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


        static MouseState mouseState;
        static MouseState lastMouseState;

        public static MouseState MouseState
        {
            get { return mouseState; }
        }

        public static MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        public static Point MouseAsPoint
        {
            get { return new Point(mouseState.X, mouseState.Y); }
        }

        public static Vector2 MouseAsVector2
        {
            get { return new Vector2(mouseState.X, mouseState.Y); }
        }

        public static Point LastMouseAsPoint
        {
            get { return new Point(lastMouseState.X, lastMouseState.Y); }
        }

        public static Vector2 LastMouseAsVector2
        {
            get { return new Vector2(lastMouseState.X, lastMouseState.Y); }
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
                    result = mouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButton.Right:
                    result = mouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButton.Middle:
                    result = mouseState.MiddleButton == ButtonState.Pressed;
                    break;
                case MouseButton.X1:
                    result = mouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButton.X2:
                    result = mouseState.XButton2 == ButtonState.Pressed;
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
                    result = mouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButton.Right:
                    result = mouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButton.Middle:
                    result = mouseState.MiddleButton == ButtonState.Released;
                    break;
                case MouseButton.X1:
                    result = mouseState.XButton1 == ButtonState.Released;
                    break;
                case MouseButton.X2:
                    result = mouseState.XButton2 == ButtonState.Released;
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
                    result = lastMouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButton.Right:
                    result = lastMouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButton.Middle:
                    result = lastMouseState.MiddleButton == ButtonState.Pressed;
                    break;
                case MouseButton.X1:
                    result = lastMouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButton.X2:
                    result = lastMouseState.XButton2 == ButtonState.Pressed;
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
                    result = lastMouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButton.Right:
                    result = lastMouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButton.Middle:
                    result = lastMouseState.MiddleButton == ButtonState.Released;
                    break;
                case MouseButton.X1:
                    result = lastMouseState.XButton1 == ButtonState.Released;
                    break;
                case MouseButton.X2:
                    result = lastMouseState.XButton2 == ButtonState.Released;
                    break;
            }
            return result;
        }

        private static bool CheckLeftButtonReleased()
        {
            return (mouseState.LeftButton == ButtonState.Released) &&
                (lastMouseState.LeftButton == ButtonState.Pressed);
        }

        private static bool CheckRightButtonReleased()
        {
            return (mouseState.RightButton == ButtonState.Released) &&
                (lastMouseState.RightButton == ButtonState.Pressed);
        }

        private static bool CheckMiddleButtonReleased()
        {
            return (mouseState.MiddleButton == ButtonState.Released) &&
                (lastMouseState.MiddleButton == ButtonState.Pressed);
        }

        private static bool CheckXButton1Released()
        {
            return (mouseState.XButton1 == ButtonState.Released) &&
                (lastMouseState.XButton1 == ButtonState.Pressed);
        }

        private static bool CheckXButton2Released()
        {
            return (mouseState.XButton2 == ButtonState.Released) &&
                (lastMouseState.XButton2 == ButtonState.Pressed);
        }

        private static bool CheckLeftButtonPressed()
        {
            return (mouseState.LeftButton == ButtonState.Pressed) &&
                (lastMouseState.LeftButton == ButtonState.Released);
        }

        private static bool CheckRightButtonPressed()
        {
            return (mouseState.RightButton == ButtonState.Pressed) &&
                (lastMouseState.RightButton == ButtonState.Released);
        }

        private static bool CheckMiddleButtonPressed()
        {
            return (mouseState.MiddleButton == ButtonState.Pressed) &&
                (lastMouseState.MiddleButton == ButtonState.Released);
        }

        private static bool CheckXButton1Pressed()
        {
            return (mouseState.XButton1 == ButtonState.Pressed) &&
                (lastMouseState.XButton1 == ButtonState.Released);
        }

        private static bool CheckXButton2Pressed()
        {
            return (mouseState.XButton2 == ButtonState.Pressed) &&
                (lastMouseState.XButton2 == ButtonState.Released);
        }
        #endregion


        #region Keyboard Region

        #region Keyboard Event Region

        private bool CheckPressedKeys()
        {
            bool keyPressed = false;

            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (lastKeyboardState.IsKeyUp(key))
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

            foreach (Keys key in lastKeyboardState.GetPressedKeys())
            {
                if (keyboardState.IsKeyUp(key))
                {
                    keyPressed = true;
                    break;
                }
            }
            return keyPressed;
        }

        #endregion

        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        public static void FlushInput()
        {
            SetStates();
        }

        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        public static bool CheckKeyPress(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        public static bool CheckKeyRelease(Keys key)
        {
            return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public static bool LastIsKeyDown(Keys key)
        {
            return lastKeyboardState.IsKeyDown(key);
        }

        public static bool LastIsKeyUp(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key);
        }

        public static Keys[] GetPressedKeys()
        {
            return keyboardState.GetPressedKeys();
        }

        public static Keys[] GetLastPressedKeys()
        {
            return lastKeyboardState.GetPressedKeys();
        }

        #endregion
    }
}