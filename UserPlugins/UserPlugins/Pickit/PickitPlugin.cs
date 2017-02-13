using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.LastPlugins.Pickit
{
    public class PickitPlugin : BasePlugin
    {

        public List<Pickit> PickitList { get; set; }
        public PickitPlugin()
        {
            Enabled = true;
        }
        public override void Load(IController hud)
        {
            base.Load(hud);

            var pickit = new Pickit
            {
                Name = "Monk",
                MatchPredicate = item => item.IsLegendary && item.RareName.Contains("wundiko"),
                WorldDecorator = new WorldDecoratorCollection(),
                ShowInList = true,
                ListColor = Color.White
            };

            PickitList = new List<Pickit>();
        }

        public override void PaintWorld(WorldLayer layer)
        {
            var displayItems = new List<Tuple<string, Color>>();
            var items = Hud.Game.Items.Where(item => item.Location == ItemLocation.Floor);
            foreach (var item in items)
            {
                var itemName = GetItemName(item);
                var pickit = PickitList.FirstOrDefault(p => p.MatchPredicate.Invoke(item));
                if (pickit != null)
                {
                    pickit.WorldDecorator.Paint(layer, item, item.FloorCoordinate, itemName);
                    if (pickit.ShowInList) displayItems.Add(Tuple.Create(itemName, pickit.ListColor));
                }
            }

            foreach (var displayItem in displayItems)
            {
                //TODO: display item in list
            }
        }

        private string GetItemName(IItem item)
        {
            var name = (item.RareName != null ? item.RareName + ", " : null) + item.SnoItem.NameLocalized;

            var ancientRank = item.AncientRank;
            if (ancientRank > 0)
            {
                name = "Ancient " + name;
            }

            if (item.KeepDecision == ItemKeepDecision.LooksGood)
            {
                name += " [!]";
            }

            return name;
        }

        public class Pickit
        {
            public string Name { get; set; }
            public Predicate<IItem> MatchPredicate { get; set; }
            public WorldDecoratorCollection WorldDecorator { get; set; }
            public bool ShowInList { get; set; }
            public Color ListColor { get; set; }
        }
    }
}