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
        public Operators StackOperator { get; set; }
        public Trigger(IController hud)
        {
            Hud = hud;
        }
        public bool IsTriggered()
        {
            if (Hud == null) return false;
            if (Sno == 0 && string.IsNullOrEmpty(Code)) return false;

            var me = Hud.Game.Me;
            if (Sno != 0)
            {
                if (me.Powers.BuffIsActive(Sno))
                {
                    var buff = me.Powers.GetBuff(Sno);
                    bool result = false;
                    switch (StackOperator)
                    {
                        case Operators.None:
                            break;
                        case Operators.Equals:
                            result = buff.IconCounts[IconIndex] == StacksCount;
                            break;
                        case Operators.Greater:
                            result = buff.IconCounts[IconIndex] > StacksCount;
                            break;
                        case Operators.GreaterOrEquals:
                            result = buff.IconCounts[IconIndex] >= StacksCount;
                            break;
                        case Operators.Minor:
                            result = buff.IconCounts[IconIndex] < StacksCount;
                            break;
                        case Operators.MinorOrEquals:
                            result = buff.IconCounts[IconIndex] <= StacksCount;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    return result;
                }
            }
            return false;
        }


        private void DefaultValues()
        {
            Code = string.Empty;
            Sno = 0;
            IconIndex = -1;
            StacksCount = 0;
            StackOperator = Operators.None;
        }
    }
}