namespace Turbo.Plugins.LastPlugins.WeakBuffs.Triggers
{
    public interface ITrigger
    {
        uint Sno { get; set; }
        int IconIndex { get; set; }
        bool IsTriggered(IController hud);
    }
}