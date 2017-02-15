using System.ComponentModel.Design;
using System.Windows.Forms;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.LastPlugins.WeakBuffs.Graphics
{
    public class TextGraphic : GraphicBase
    {
        public float X { get; set; }
        public float Y { get; set; }
        public IFont TextFont { get; set; }
        public StringGeneratorFunc TextFunc { get; set; }

        private readonly TopLabelDecorator _decorator;

        public TextGraphic(IController hud) : base(hud)
        {
            _decorator = new TopLabelDecorator(Hud);
        }

        public override void Draw()
        {
            if (TextFont == null) return;


            _decorator.TextFont = TextFont;
            _decorator.TextFunc = TextFunc;

            var text = TextFunc != null ? TextFunc.Invoke() : null;
            var layout = TextFont.GetTextLayout(text);
            var x = (float)Hud.Window.Size.Width / 2 + X;
            var y = (float)Hud.Window.Size.Height / 2 + Y;
            SimonSays.SimonSays.Debug(string.Format("TextFunc: {0}", text));
            SimonSays.SimonSays.Debug(string.Format("X: {0}, Y: {1}, W:{2}, H:{3}", x, x, layout.Metrics.Width,layout.Metrics.Height));
            
            _decorator.Paint(x, y, layout.Metrics.Width, layout.Metrics.Height, HorizontalAlign.Left);

        }
    }
}
