namespace Turbo.Plugins.LastPlugins.WeakBuffs.Graphics
{
    public interface IGraphic
    {
        IController Hud { get;  }
        void Draw();
    }
}