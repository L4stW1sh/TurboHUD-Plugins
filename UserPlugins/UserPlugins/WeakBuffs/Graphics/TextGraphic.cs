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

        private TopLabelDecorator _decorator;

        public TextGraphic(IController hud) : base(hud)
        {
            
        }

        public override void Draw()
        {
            if (TextFont == null) return;

           
            _decorator = new TopLabelDecorator(Hud)
            {
                TextFont = TextFont,
                TextFunc = TextFunc
            };

            var text = TextFunc != null ? TextFunc.Invoke() : null;
            var layout = TextFont.GetTextLayout(text);

            _decorator.Paint(X, Y, layout.Metrics.Width, layout.Metrics.Height, HorizontalAlign.Left);

        }
    }
}
