using System;
using System.Linq;

namespace Turbo.Plugins.LastPlugins.WeakBuffs.Triggers
{
    public class BuffTrigger : ITrigger
    {
        public uint Sno { get; set; }
        public int IconIndex { get; set; }
        public int StackCount { get; set; }
        public Operators StackOperator { get; set; }
        public double RemainingTime { get; set; }
        public Operators RemainingTimeOperator { get; set; }
        public double Tolerance { get; set; }

        public bool IsTriggered(IController hud)
        {
            var triggered = false;
            if (hud != null && Sno != 0)
            {
                var buff = hud.Game.Me.Powers.GetBuff(Sno);
                if (buff != null)
                {
                    if (StackCount != -1)
                    {
                        SimonSays.SimonSays.Debug(string.Format("[{1}] Stacks: {0}", buff.IconCounts.Length, Sno));
                        triggered = Compare(buff.IconCounts.Length, StackCount, StackOperator);
                        SimonSays.SimonSays.Debug(string.Format("Triggered: {0}", triggered));
                    }
                    if (Math.Abs(RemainingTime - -1) < Tolerance)
                    {
                        SimonSays.SimonSays.Debug(string.Format("[{1}] RemainingTime: {0}", buff.TimeLeft(), Sno));
                        triggered = Compare(buff.TimeLeft(), RemainingTime, RemainingTimeOperator);
                        SimonSays.SimonSays.Debug(string.Format("Triggered: {0}", triggered));
                    }
                }
            }

            SimonSays.SimonSays.Debug(string.Format("[{1}] Return Triggered: {0}", triggered, Sno));
            return triggered;
        }

        public BuffTrigger()
        {
            Sno = 0;
            IconIndex = -1;
            StackCount = -1;
            StackOperator = Operators.None;
            RemainingTime = -1.0;
            RemainingTimeOperator = Operators.None;
            Tolerance = 0.0001;
        }

        private bool Compare(int value1, int value2, Operators op)
        {
            var result = false;
            switch (op)
            {
                case Operators.Equals:
                    result = value1 == value2;
                    break;
                case Operators.Greater:
                    result = value1 > value2;
                    break;
                case Operators.GreaterOrEquals:
                    result = value1 >= value2;
                    break;
                case Operators.Minor:
                    result = value1 < value2;
                    break;
                case Operators.MinorOrEquals:
                    result = value1 <= value2;
                    break;
            }
            return result;
        }

        private bool Compare(double value1, double value2, Operators op)
        {
            var result = false;
            switch (op)
            {
                case Operators.Equals:
                    result = Math.Abs(value1 - value2) < Tolerance;
                    break;
                case Operators.Greater:
                    result = value1 > value2;
                    break;
                case Operators.GreaterOrEquals:
                    result = value1 >= value2;
                    break;
                case Operators.Minor:
                    result = value1 < value2;
                    break;
                case Operators.MinorOrEquals:
                    result = value1 <= value2;
                    break;
            }
            return result;
        }
    }
}