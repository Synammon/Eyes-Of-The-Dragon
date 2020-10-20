using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Psilibrary.TileEngine;

namespace XLevelEditor
{
    public partial class FormMain : Form
    {
        public static World World = new World();
        public static TileMap Map { get; set; }
        public static List<Tileset> Tilesets { get; set; }
        public static List<TilesetData> TilesetData { get; set; }
        public static List<MapLayer> Layers = new List<MapLayer>();
        public static LevelData LevelData = new LevelData();
        public static Engine engine = new Engine(64, 64);
        public static Camera camera;

        public FormMain()
        {
            InitializeComponent();

            Tilesets = new List<Tileset>();
            TilesetData = new List<TilesetData>();

            newMapToolStripMenuItem.Click += NewMapToolStripMenuItem_Click;
            newTilesetToolStripMenuItem.Click += NewTilesetToolStripMenuItem_Click;
            newLayerToolStripMenuItem.Click += NewLayerToolStripMenuItem_Click;
            newWorldToolStripMenuItem.Click += NewWorldToolStripMenuItem_Click;

            vScrollBar1.Scroll += VScrollBar1_Scroll;
            hScrollBar1.Scroll += HScrollBar1_Scroll;

            mapDisplay.MouseMove += MapDisplay_MouseMove;
            mapDisplay.MouseDown += MapDisplay_MouseDown;

            lstTilesets.SelectedIndexChanged += LstTilesets_SelectedIndexChanged;

            nudInc1.ValueChanged += NudInc1_ValueChanged;
        }

        private void NudInc1_ValueChanged(object sender, EventArgs e)
        {
            FillPreviews();
        }

        private void LstTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = Tilesets[lstTilesets.SelectedIndex].Texture.Name;
            pbTileset.Image = Image.FromFile(TilesetData[lstTilesets.SelectedIndex].TilesetImageName);
            FillPreviews();
        }

        private void FillPreviews()
        {
            int selected = lstTilesets.SelectedIndex;
            int tile = (int)nudInc1.Value;

            Image preview = (Image)new Bitmap(pbPreview.Width, pbPreview.Height);

            Rectangle dest = new Rectangle(0, 0, preview.Width, preview.Height);
            Rectangle source = new Rectangle(
                Tilesets[selected].SourceRectangles[tile].X,
                Tilesets[selected].SourceRectangles[tile].Y,
                Tilesets[selected].SourceRectangles[tile].Width,
                Tilesets[selected].SourceRectangles[tile].Height);

            Graphics g = Graphics.FromImage(preview);
            g.DrawImage(pbTileset.Image, dest, source, GraphicsUnit.Pixel);
            pbPreview.Image = preview;
        }

        private void MapDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            Microsoft.Xna.Framework.Point dest = 
                new Microsoft.Xna.Framework.Point(
                    (p.X + (int)camera.Position.X) / Engine.TileWidth, 
                    (p.Y + (int)camera.Position.Y) / Engine.TileHeight);

            if (e.Button == MouseButtons.Left && 
                lstTilesets.SelectedIndex >= 0 && 
                lstLayers.SelectedIndex >= 0)
            {
                Map.SetTile(
                    lstLayers.SelectedIndex, 
                    dest.X, 
                    dest.Y, 
                    nudInc1.Value, 
                    lstTilesets.SelectedIndex);
            }
            else if (e.Button == MouseButtons.Right && lstLayers.SelectedIndex >= 0)
            {
                Map.SetTile(
                    lstLayers.SelectedIndex, 
                    dest.X, 
                    dest.Y, 
                    -1, 
                    -1);
            }
        }

        private void MapDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (camera == null || Map == null)
                return;

            camera.Position = new Microsoft.Xna.Framework.Vector2(
                Engine.TileWidth * hScrollBar1.Value,
                camera.Position.Y);

            camera.LockCamera(Map);
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (camera == null || Map == null)
                return;

            camera.Position = new Microsoft.Xna.Framework.Vector2(
                camera.Position.X,
                Engine.TileHeight * vScrollBar1.Value);

            camera.LockCamera(Map);
        }

        private void NewWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            World = new World();
            LevelData = new LevelData();
            Map = null;
            Tilesets.Clear();
            lstLayers.Items.Clear();
            lstMaps.Items.Clear();
            lstTilesets.Items.Clear();
        }

        private void NewLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tilesets.Count > 0 && LevelData.MapWidth > 0 && LevelData.MapHeight > 0)
            {
                MapLayer layer = new MapLayer(LevelData.MapWidth, LevelData.MapHeight);
                Map.AddLayer(layer);
                lstLayers.Items.Add("New Layer");
                Layers.Add(layer);
            }
        }

        private void NewTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewTileset frm = new FormNewTileset();
            frm.ShowDialog();

            if (frm.OKPressed)
            {
                using (Stream stream = new FileStream(frm.TilesetData.TilesetImageName, FileMode.Open, FileAccess.Read))
                {
                    Tileset tileset = new Tileset(
                        Texture2D.FromStream(mapDisplay.Editor.graphics, stream),
                        frm.TilesetData.TilesWide,
                        frm.TilesetData.TilesHigh,
                        frm.TilesetData.TileWidthInPixels,
                        frm.TilesetData.TileHeightInPixels);

                    Tilesets.Add(tileset);
                    TilesetData.Add(frm.TilesetData);
                    lstTilesets.Items.Add(frm.TilesetData.TilesetName);
                    lstTilesets.SelectedIndex = Tilesets.Count - 1;
                }
            }
        }

        private void NewMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tilesets.Count > 0)
            {
                FormNewMap frm = new FormNewMap();
                frm.ShowDialog();

                if (frm.OKPressed)
                {
                    LevelData = frm.LevelData;

                    vScrollBar1.Maximum = LevelData.MapHeight;
                    hScrollBar1.Maximum = LevelData.MapWidth;

                    Map = new TileMap(
                        LevelData.MapName,
                        Tilesets,
                        new MapLayer(LevelData.MapWidth, LevelData.MapHeight),
                        new MapLayer(LevelData.MapWidth, LevelData.MapHeight),
                        new MapLayer(LevelData.MapWidth, LevelData.MapHeight),
                        new CollisionLayer(),
                        new PortalLayer());

                    lstLayers.Items.Clear();
                    lstLayers.Items.Add("Base");
                    lstLayers.Items.Add("Building");
                    lstLayers.Items.Add("Splatter");
                    lstMaps.Items.Add(LevelData.MapName);
                    World.AddMap(LevelData.MapName, Map);

                    camera = new Camera(new Microsoft.Xna.Framework.Rectangle(
                        0,
                        0,
                        mapDisplay.Width,
                        mapDisplay.Height));
                }
            }
        }
    }
}
