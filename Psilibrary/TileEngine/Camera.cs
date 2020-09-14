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

        private Vector2 position;
        private float speed;
        private float zoom;
        private Rectangle viewportRectangle;
        private CameraMode mode;

        #endregion

        #region Property Region

        public Point PositionAsPoint
        {
            get { return new Point((int)position.X, (int)position.Y); }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = (float)MathHelper.Clamp(speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return zoom; }
        }

        public CameraMode CameraMode
        {
            get { return mode; }
        }

        public Matrix Transformation
        {
            get { return Matrix.CreateScale(zoom) * 
                Matrix.CreateTranslation(new Vector3((int)-Position.X, (int)-Position.Y, 0f)); }
        }

        public Rectangle ViewportRectangle
        {
            get { return new Rectangle(
                viewportRectangle.X,
                viewportRectangle.Y, 
                viewportRectangle.Width, 
                viewportRectangle.Height); }
        }

        #endregion

        #region Constructor Region

        public Camera(Rectangle viewportRect)
        {
            speed = 4f;
            zoom = 1f;
            viewportRectangle = viewportRect;
            mode = CameraMode.Follow;
            Position = Vector2.Zero;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            speed = 4f;
            zoom = 1f;
            viewportRectangle = viewportRect;
            Position = position;
            mode = CameraMode.Follow;
        }

        #endregion

        #region Method Region

        public void Update(GameTime gameTime, TileMap map)
        {
            Vector2 motion = Vector2.Zero;

            if (Xin.IsKeyDown(Keys.Left))
                motion.X = -speed;
            else if (Xin.IsKeyDown(Keys.Right))
                motion.X = speed;

            if (Xin.IsKeyDown(Keys.Up))
                motion.Y = -speed;
            else if (Xin.IsKeyDown(Keys.Down))
                motion.Y = speed;

            if (motion != Vector2.Zero && map != null)
            {
                motion.Normalize();
                position += motion * speed;
                LockCamera(map);
            }

        }

        public void ZoomIn()
        {
            zoom += .25f;

            if (zoom > 2.5f)
                zoom = 2.5f;

            Vector2 newPosition = Position * zoom;
        }

        public void ZoomOut()
        {
            zoom -= .25f;

            if (zoom < .5f)
                zoom = .5f;

            Vector2 newPosition = Position * zoom;
        }

        public void SnapToPosition(Vector2 newPosition, TileMap map)
        {
            position.X = newPosition.X - viewportRectangle.Width / 2;
            position.Y = newPosition.Y - viewportRectangle.Height / 2;
            LockCamera(map);
        }

        public void LockCamera(TileMap map)
        {
            if (map != null)
            {
                position.X = MathHelper.Clamp(position.X,
                    0,
                    map.WidthInPixels - viewportRectangle.Width);
                position.Y = MathHelper.Clamp(position.Y,
                    0,
                    map.HeightInPixels - viewportRectangle.Height);
            }
        }

        public void LockToSprite(AnimatedSprite sprite, TileMap map)
        {
            position.X = (sprite.Position.X + sprite.Width / 2) * zoom
                            - (viewportRectangle.Width / 2);
            position.Y = (sprite.Position.Y + sprite.Height / 2) * zoom
                            - (viewportRectangle.Height / 2);
            LockCamera(map);
        }

        #endregion
    }
}
