using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Psilibrary.TileEngine
{
    public class MapLayer : ILayer
    {
        #region Field Region

        private Tile[,] layer;
        
        #endregion

        #region Property Region

        public Tile[,] Layer
        {
            get { return layer; }
            private set { layer = value; }
        }

        public int Width
        {
            get { return layer.GetLength(1); }
        }

        public int Height
        {
            get { return layer.GetLength(0); }
        }

        #endregion

        #region Constructor Region

        private MapLayer()
        {

        }

        public MapLayer(Tile[,] map)
        {
            this.layer = (Tile[,])map.Clone();
        }

        public MapLayer(int width, int height)
        {
            layer = new Tile[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    layer[y, x] = new Tile(0, 0);
                }
            }
        }

        #endregion

        #region Method Region

        public Tile GetTile(int x, int y)
        {
            return layer[y, x];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            layer[y, x] = tile;
        }

        public void SetTile(int x, int y, int tileIndex, int tileset, int rotation = 0)
        {
            layer[y, x] = new Tile(tileIndex, tileset, rotation);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, List<Tileset> tilesets, bool debug = false)
        {
            Point cameraPoint = Engine.VectorToCell(camera.Position * (1 / camera.Zoom));
            Point viewPoint = Engine.VectorToCell(
                new Vector2(
                    (camera.Position.X + camera.ViewportRectangle.Width) * (1 / camera.Zoom),
                    (camera.Position.Y + camera.ViewportRectangle.Height) * (1 / camera.Zoom)));

            Point min = new Point();
            Point max = new Point();

            min.X = Math.Max(0, cameraPoint.X - 1);
            min.Y = Math.Max(0, cameraPoint.Y - 1);
            max.X = Math.Min(viewPoint.X + 1, Width);
            max.Y = Math.Min(viewPoint.Y + 1, Height);
            Vector2 origin = new Vector2();

            Rectangle destination = new Rectangle(0, 0, Engine.TileWidth, Engine.TileHeight);
            Tile tile;

            for (int y = min.Y; y < max.Y; y++)
            {
                destination.Y = y * Engine.TileHeight;

                for (int x = min.X; x < max.X; x++)
                {
                    tile = GetTile(x, y);

                    if (tile.TileIndex == -1 || tile.Tileset == -1 || tile.TileIndex >= tilesets[tile.Tileset].SourceRectangles.Length)
                        continue;

                    origin.X = (float)tilesets[tile.Tileset].SourceRectangles[tile.TileIndex].Width / 2f;
                    origin.Y = (float)tilesets[tile.Tileset].SourceRectangles[tile.TileIndex].Height / 2f;

                    destination.X = x * Engine.TileWidth;

                    //spriteBatch.Draw(
                    //    tilesets[tile.Tileset].Texture,
                    //    destination,
                    //    tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                    //    Color.White);

                    if (tile.Rotation == 0)
                    {
                        spriteBatch.Draw(
                            tilesets[tile.Tileset].Texture,
                            destination,
                            tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                            Color.White);
                    }
                    else
                    {
                        Rectangle dest2 = new Rectangle(destination.X, destination.Y, destination.Width, destination.Height);
                        dest2.X += Engine.TileWidth / 2;
                        dest2.Y += Engine.TileHeight / 2;

                        spriteBatch.Draw(
                            tilesets[tile.Tileset].Texture,
                            dest2,
                            tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                            Color.White,
                            MathHelper.ToRadians(tile.Rotation),
                            origin,
                            SpriteEffects.None,
                            1f);                  
                    }
                }
            }
        }

        #endregion
    }
}
