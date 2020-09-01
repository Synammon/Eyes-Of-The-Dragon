using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Psilibrary.TileEngine
{
    public enum CameraMode { Free, Follow }

    public class Camera
    {
        #region Field Region

        private Vector2 _position;
        private float _speed;
        private float _zoom;
        private Rectangle _viewportRectangle;
        private CameraMode _mode;

        #endregion

        #region Property Region

        public Point PositionAsPoint
        {
            get { return new Point((int)_position.X, (int)_position.Y); }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set
            {
                _speed = (float)MathHelper.Clamp(_speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return _zoom; }
        }

        public CameraMode CameraMode
        {
            get { return _mode; }
        }

        public Matrix Transformation
        {
            get { return Matrix.CreateScale(_zoom) * 
                Matrix.CreateTranslation(new Vector3((int)-Position.X, (int)-Position.Y, 0f)); }
        }

        public Rectangle ViewportRectangle
        {
            get { return new Rectangle(
                _viewportRectangle.X,
                _viewportRectangle.Y, 
                _viewportRectangle.Width, 
                _viewportRectangle.Height); }
        }

        #endregion

        #region Constructor Region

        public Camera(Rectangle viewportRect)
        {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRect;
            _mode = CameraMode.Follow;
            Position = Vector2.Zero;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRect;
            Position = position;
            _mode = CameraMode.Follow;
        }

        #endregion

        #region Method Region

        public void Update(GameTime gameTime, TileMap map)
        {
            Vector2 motion = Vector2.Zero;

            if (Xin.IsKeyDown(Keys.Left))
                motion.X = -_speed;
            else if (Xin.IsKeyDown(Keys.Right))
                motion.X = _speed;

            if (Xin.IsKeyDown(Keys.Up))
                motion.Y = -_speed;
            else if (Xin.IsKeyDown(Keys.Down))
                motion.Y = _speed;

            if (motion != Vector2.Zero && map != null)
            {
                motion.Normalize();
                _position += motion * _speed;
                LockCamera(map);
            }

        }

        public void ZoomIn()
        {
            _zoom += .25f;

            if (_zoom > 2.5f)
                _zoom = 2.5f;

            Vector2 newPosition = Position * _zoom;
        }

        public void ZoomOut()
        {
            _zoom -= .25f;

            if (_zoom < .5f)
                _zoom = .5f;

            Vector2 newPosition = Position * _zoom;
        }

        public void SnapToPosition(Vector2 newPosition, TileMap map)
        {
            _position.X = newPosition.X - _viewportRectangle.Width / 2;
            _position.Y = newPosition.Y - _viewportRectangle.Height / 2;
            LockCamera(map);
        }

        public void LockCamera(TileMap map)
        {
            if (map != null)
            {
                _position.X = MathHelper.Clamp(_position.X,
                    0,
                    map.WidthInPixels - _viewportRectangle.Width);
                _position.Y = MathHelper.Clamp(_position.Y,
                    0,
                    map.HeightInPixels - _viewportRectangle.Height);
            }
        }

        #endregion
    }
}
