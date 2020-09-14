using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Psilibrary.TileEngine
{
    public class PortalLayer
    {
        #region Field Region

        private Dictionary<string, Portal> portals;

        #endregion

        #region Property Region

        [ContentSerializer]
        public Dictionary<string, Portal> Portals
        {
            get { return portals; }
            set { portals = value; }
        }

        #endregion

        #region Constructor Region

        public PortalLayer()
        {
            portals = new Dictionary<string, Portal>();
        }

        #endregion
    }
}
