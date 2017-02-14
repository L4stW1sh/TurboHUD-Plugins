namespace Turbo.Plugins.LastPlugins.WeakBuffs
{
    public class Loader
    {
        public bool Enabled { get; set; }
        public bool? InCombat { get; set; }
        public bool? InTown { get; set; }
        public SpecialArea SpecificArea { get; set; }
        public HeroClass HeroClass { get; set; }
        public string HeroName { get; set; }
        public GameDifficulty Difficulty { get; set; }

        public Loader()
        {
            Enabled = true;
            InCombat = null;
            InTown = null;
            SpecificArea = SpecialArea.None;
            HeroClass = HeroClass.None;
            HeroName = string.Empty;
            Difficulty = GameDifficulty.unknown;
        }
    }
}
