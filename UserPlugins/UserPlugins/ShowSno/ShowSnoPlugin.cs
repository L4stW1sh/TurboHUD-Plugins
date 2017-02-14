using System.Linq;
using Turbo.Collector;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.LastPlugins.ShowSno
{
    public class ShowSnoPlugin : BasePlugin
    {
        public ShowSnoPlugin()
        {
            Enabled = true;
            
        }

        public override void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.AfterClip) return;
        }
    }
}