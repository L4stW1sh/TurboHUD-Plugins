using System.Collections.Generic;
using Turbo.Plugins.LastPlugins.WeakBuffs.Graphics;

namespace Turbo.Plugins.LastPlugins.WeakBuffs
{
    public class Aura
    {
        public Loader Loader { get; set; }
        public List<Trigger> Triggers { get; set; }
        public GraphicBase Graphic { get; set; }
    }
}