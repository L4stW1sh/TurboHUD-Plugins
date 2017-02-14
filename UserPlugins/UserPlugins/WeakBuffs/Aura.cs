using System.Collections.Generic;
using Turbo.Plugins.LastPlugins.WeakBuffs.Graphics;
using Turbo.Plugins.LastPlugins.WeakBuffs.Triggers;

namespace Turbo.Plugins.LastPlugins.WeakBuffs
{
    public class Aura
    {
        public Loader Loader { get; set; }
        public List<ITrigger> Triggers { get; set; }
        public IGraphic Graphic { get; set; }

        public Aura()
        {
            Triggers = new List<ITrigger>();
        }
    }
}