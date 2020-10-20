using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XLevelEditor
{
    public class MapDisplay : MonoGame.Forms.Controls.MonoGameControl
    {

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            if (FormMain.camera == null)
            {
                return;
            }

            Editor.spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                FormMain.camera.Transformation);

            if (FormMain.Map != null)
                FormMain.Map.Draw(new GameTime(), Editor.spriteBatch, FormMain.camera);

            Editor.spriteBatch.End();
        }
    }
}
