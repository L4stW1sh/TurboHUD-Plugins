using System;
using System.Collections.Generic;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.LastPlugins.SimonSays
{
    public enum SimonPun
    {
        All = 0,
        Debug = 2,
        Info = 4,
        Error = 16,
    }

    public static class SimonSays
    {
        public static ushort MaxMessages { get; set; }
        public static List<Tuple<string, SimonPun>> Messages { get; private set; }

        static SimonSays()
        {
            MaxMessages = 25;
            Messages = new List<Tuple<string, SimonPun>>(MaxMessages);
        }

        private static void AddMessage(string message, SimonPun pun = SimonPun.All, string format = "{1:HH:mm:ss.fff} : {2,5} : {0}")
        {
            if (Messages.Count >= MaxMessages)
            {
                Messages = Messages.Skip(Math.Max(0, Messages.Count() - MaxMessages + 1)).ToList();
            }

            Messages.Add(new Tuple<string, SimonPun>(string.Format(format, message, DateTime.Now, pun), pun));
        }

        public static void Debug(string message)
        {
            AddMessage(message, SimonPun.Debug);
        }

        public static void Info(string message)
        {
            AddMessage(message, SimonPun.Info);
        }

        public static void Error(string message)
        {
            AddMessage(message, SimonPun.Error);
        }
    }

    public class SimonSaysPlugin : BasePlugin
    {
        public TopLabelDecorator MessageFrame { get; set; }
        public Dictionary<SimonPun, IFont> Fonts { get; set; }

        public SimonSaysPlugin()
        {
            Enabled = true;
            Fonts = new Dictionary<SimonPun, IFont>();
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            MessageFrame = new TopLabelDecorator(hud)
            {
                BackgroundBrush = Hud.Render.CreateBrush(178, 0, 0, 0, 0),
                BorderBrush = Hud.Render.CreateBrush(255, 0, 0, 0, 2),
                TextFont = Hud.Render.CreateFont("arial", 7, 224, 240, 240, 64, true, false, false)
            };

            Fonts.Add(SimonPun.Error, Hud.Render.CreateFont("arial", 7, 224, 255, 0, 0, true, false, false));
            Fonts.Add(SimonPun.All, Hud.Render.CreateFont("arial", 7, 224, 240, 240, 64, true, false, false));
            Fonts.Add(SimonPun.Info, Hud.Render.CreateFont("arial", 7, 224, 240, 240, 64, true, false, false));
            Fonts.Add(SimonPun.Debug, Hud.Render.CreateFont("arial", 7, 224, 240, 240, 64, true, false, false));
        }

        public override void PaintTopInGame(ClipState clipState)
        {
            if (clipState != ClipState.BeforeClip) return;
            if (SimonSays.Messages.Count == 0) return;

            // dumb test
            //SimonSays.Error(Guid.NewGuid().ToString());

            var screenSize = Hud.Window.Size;
            var x = screenSize.Width * 0.0642f;
            var y = screenSize.Height * 0.1042f;

            var estimatedWidth = SimonSays.Messages.Max(m => m.Item1.Length) * 7f; // TODO : fix for long exceptions
            var estimatedHeight = (SimonSays.Messages.Count + 1) * 14f + 10 + 2;

            //MessageFrame.PaintFixed(x, y, estimatedWidth, estimatedHeight, string.Empty);
            MessageFrame.TextFunc = () => string.Empty;
            MessageFrame.Paint(x, y, estimatedWidth, estimatedHeight, HorizontalAlign.Left);

            x += 10;
            y += 4;

            Fonts[SimonPun.All].DrawText("Simon says :", x, y);
            y += 14;

            foreach (var message in SimonSays.Messages)
            {
                Fonts[message.Item2].DrawText(message.Item1, x, y);
                y += 14;
            }
        }
    }
}