using System.Collections.Generic;
using System.Linq;
using Turbo.Plugins.Default;
using Turbo.Plugins.LastPlugins.WeakBuffs.Graphics;
using Turbo.Plugins.LastPlugins.WeakBuffs.Triggers;

namespace Turbo.Plugins.LastPlugins.WeakBuffs
{
    public class WeakBuffsPlugin : BasePlugin
    {
        public List<Aura> Auras { get; private set; }

        public override void Load(IController hud)
        {
            base.Load(hud);
            Auras = new List<Aura>();
            var loader = new Loader
            {
                Enabled = true
            };

            var trigger = new BuffTrigger
            {
                Sno = 96090,
                StackCount = 0,
                StackOperator = Operators.Greater
            };

            var textGraphic = new TextGraphic(hud)
            {
                TextFunc = () => "Sweeping Wind is UP",
                TextFont = Hud.Render.CreateFont("tahoma", 12.0f, 255, 255, 255, 255, false, false, true),
                X = 15.0f,
                Y = 15.0f
            };

            var aura = new Aura
            {
                Graphic = textGraphic,
                Loader = loader
            };
            aura.Triggers.Add(trigger);
            Auras.Add(new Aura
            {
                Graphic = textGraphic,
                Loader = loader
            });
        }

        public override void PaintTopInGame(ClipState clipState)
        {
            if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;
            if (Hud.Game.IsPaused) return;
            if (Auras == null || Auras.Count == 0) return;


            //auras always display
            var auras = Auras.Where(aura => aura.Triggers.Any(t => t.IsTriggered(Hud))).ToList();

            SimonSays.SimonSays.Debug(auras.Count + " auras");
            foreach (var aura in auras)
            {
                SimonSays.SimonSays.Debug(aura.Graphic.GetType().FullName + " is display");
                aura.Graphic.Draw();
            }

        }
    }
}