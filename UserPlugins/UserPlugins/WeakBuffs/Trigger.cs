using System;
using Turbo.SNO;

namespace Turbo.Plugins.LastPlugins.WeakBuffs
{
    public class Trigger
    {
        public IController Hud { get; }
        public string Code { get; set; }
        public uint Sno { get; set; }
        public int IconIndex { get; set; }
        public int StacksCount { get; set; }
        public Types.Operators StackOperator { get; set; }
        public Trigger(IController hud)
        {
            Hud = hud;
        }
        public bool IsTriggered()
        {
            if (Hud == null) return false;
            if (Sno == 0 && string.IsNullOrEmpty(Code)) return false;

            var me = Hud.Game.Me;
            
     
           
            // check buff


            //check stack

            return true;
        }


        private void DefaultValues()
        {
            Code = string.Empty;
            Sno = 0;
            IconIndex = -1;
            StacksCount = 0;
            StackOperator = Types.Operators.None;
        }
    }
}