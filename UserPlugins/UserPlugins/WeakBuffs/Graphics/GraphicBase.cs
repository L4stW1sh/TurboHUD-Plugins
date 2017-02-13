namespace Turbo.Plugins.LastPlugins.WeakBuffs.Graphics
{
    public abstract class GraphicBase : IGraphic
    {
        public IController Hud { get; }
        protected GraphicBase(IController hud)
        {
            Hud = hud;
        }

        public abstract void Draw();
    }
}