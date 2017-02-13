using System.Globalization;
using System.Windows.Forms;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.LastPlugins.ScreenLocationInfo
{
    public class ScreenLocationInfoPlugin : BasePlugin
    {
        public Keys HotKey { get; set; }
        public IBrush BackgroundBrush { get; set; }
        public IFont TextFont { get; set; }

        public float Offset { get; set; }
        public float Padding { get; set; }

        public ScreenLocationInfoPlugin()
        {
            Enabled = true;
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            HotKey = Keys.V;
            TextFont = Hud.Render.CreateFont("tahoma", 7, 224, 240, 240, 64, true, false, false);
            BackgroundBrush = Hud.Render.CreateBrush(178, 0, 0, 0, 0);
            Offset = 20;
            Padding = 5;
        }

        public override void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.AfterClip) return;
            if (!Hud.Input.IsKeyDown(HotKey)) return;

            var mousePositionX = (float)Hud.Window.CursorX;
            var mousePositionY = (float)Hud.Window.CursorY;

            var text = string.Format(CultureInfo.InvariantCulture, "{0}x{1}\nX: {2,4}\nY: {3,4}",
                Hud.Window.Size.Width, Hud.Window.Size.Height,
                string.Format("{0} px | % : {1}f", mousePositionX, mousePositionX / Hud.Window.Size.Width),
                string.Format("{0} px | % : {1}f", mousePositionY, mousePositionY / Hud.Window.Size.Height)
                );

            var layout = TextFont.GetTextLayout(text);
            var h = layout.Metrics.Height;
            var w = layout.Metrics.Width;

            mousePositionX += (Hud.Window.Size.Width / 2 > mousePositionX) ? Offset : -w - Offset;
            mousePositionY += (Hud.Window.Size.Height / 2 > mousePositionY) ? Offset : -h - Offset;

            BackgroundBrush.DrawRectangle(mousePositionX - Padding, mousePositionY - Padding, w + Padding * 2, h + Padding * 2);
            TextFont.DrawText(layout, mousePositionX, mousePositionY);
        }
    }
}