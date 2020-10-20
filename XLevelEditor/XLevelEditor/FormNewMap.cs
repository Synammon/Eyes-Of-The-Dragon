using Psilibrary.TileEngine;
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

namespace XLevelEditor
{
    public partial class FormNewMap : Form
    {
        #region Field Region

        bool okPressed;
        string gamePath;
        LevelData levelData;

        #endregion

        #region Property Region

        public bool OKPressed
        {
            get { return okPressed; }
        }

        public LevelData LevelData
        {
            get { return levelData; }
        }

        public string GamePath
        {
            get { return gamePath; }
        }

        #endregion

        #region Constructor Region

        public FormNewMap()
        {
            InitializeComponent();

            btnBrowse.Click += new EventHandler(btnBrowse_Click);

            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            SetDefaultValues();
        }

        void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.Description = "Select Game folder";
            folderDialog.SelectedPath = Application.StartupPath;

            DialogResult folderResult = folderDialog.ShowDialog();

            if (folderResult == DialogResult.OK)
            {
                gamePath = Path.Combine(folderDialog.SelectedPath, "Game");
                tbGamePath.Text = gamePath;
            }
        }

        private void SetDefaultValues()
        {
            tbLevelName.Text = "Shadow Down";
            tbMapName.Text = "Shadow Down";
            mtbWidth.Text = "100";
            mtbHeight.Text = "100";
        }

        #endregion

        #region Button Event Handler Region

        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbLevelName.Text))
            {
                MessageBox.Show("You must enter a name for the level.", "Missing Level Name");
                return;
            }

            if (string.IsNullOrEmpty(tbMapName.Text))
            {
                MessageBox.Show("You must enter a name for the map of the level.", "Missing Map Name");
                return;
            }

            int mapWidth = 0;
            int mapHeight = 0;

            if (!int.TryParse(mtbWidth.Text, out mapWidth) || mapWidth < 1)
            {
                MessageBox.Show("The width of the map must be greater than or equal to one.", "Map Size Error");
                return;
            }

            if (!int.TryParse(mtbHeight.Text, out mapHeight) || mapHeight < 1)
            {
                MessageBox.Show("The height of the map must be greater than or equal to one.", "Map Size Error");
                return;
            }

            levelData = new LevelData
            {
                LevelName = tbLevelName.Text,
                MapName = tbMapName.Text,
                MapWidth = mapWidth,
                MapHeight = mapHeight
            };

            okPressed = true;
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            okPressed = false;
            this.Close();
        }

        #endregion
    }
}
