using System.Collections.Generic;
using System.Linq;
using Turbo.Plugins.Default;
using Turbo.Plugins.LastPlugins.WeakBuffs.Graphics;

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
                Enabled = true,
                InCombat = true,
                InTown = false
            };
            var trigger = new Trigger(Hud)
            {
                Sno = 96090,
                StacksCount = 5,
                StackOperator = Types.Operators.Minor
            };
            var graphic = new TextGraphic(Hud)
            {
                TextFont = Hud.Render.CreateFont("tahoma", 7.0f, 255, 0, 255, 40, false, false, true),
                TextFunc = () => "Warning SW < 5 stack"
            };

            var aura = new Aura
            {
                Graphic = graphic,
                Loader = loader,
                Triggers = new List<Trigger>(new[] {trigger})
            };

            Auras.Add(aura);
        }

        public override void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;
            if (Hud.Game.IsPaused) return;
            if (Auras == null || Auras.Count == 0) return;

            if (Hud.Game.IsInGame && Hud.Game.IsInTown)
            {
                var auras = Auras.Where(a => a.Loader.Enabled && a.Loader.InTown);
                auras.ForEach(aura =>
                {
                    var triggered = aura.Triggers.Any(t => t.IsTriggered());
                    if (triggered)
                    {
                        aura.Graphic.Draw();
                    }
                });
            }

            if (Hud.Game.IsInGame && !Hud.Game.IsInTown)
            {
                var auras = Auras.Where(a => a.Loader.Enabled && !a.Loader.InTown);
                auras.ForEach(aura =>
                {
                    var triggered = aura.Triggers.Any(t => t.IsTriggered());
                    if (triggered)
                    {
                        aura.Graphic.Draw();
                    }
                });
            }

            if (Hud.Game.IsInGame && Hud.Game.Me.InCombat)
            {
                var auras = Auras.Where(a => a.Loader.Enabled && a.Loader.InCombat);
                auras.ForEach(aura =>
                {
                    var triggered = aura.Triggers.Any(t => t.IsTriggered());
                    if (triggered)
                    {
                        aura.Graphic.Draw();
                    }
                });
            }

            if (Hud.Game.IsInGame && !Hud.Game.Me.InCombat)
            {
                var auras = Auras.Where(a => a.Loader.Enabled && !a.Loader.InCombat);
                auras.ForEach(aura =>
                {
                    var triggered = aura.Triggers.Any(t => t.IsTriggered());
                    if (triggered)
                    {
                        aura.Graphic.Draw();
                    }
                });
            }
        }
    }
}