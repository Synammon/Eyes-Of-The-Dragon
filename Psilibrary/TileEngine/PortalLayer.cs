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

        private Dictionary<string, Portal> _portals;

        #endregion

        #region Property Region

        [ContentSerializer]
        public Dictionary<string, Portal> Portals
        {
            get { return _portals; }
            set { _portals = value; }
        }

        #endregion

        #region Constructor Region

        public PortalLayer()
        {
            _portals = new Dictionary<string, Portal>();
        }

        #endregion
    }
}
