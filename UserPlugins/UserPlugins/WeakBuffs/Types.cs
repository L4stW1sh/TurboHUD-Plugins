using System;

namespace Turbo.Plugins.LastPlugins.WeakBuffs
{
    public enum AuraTypes
    {
        Buff = 0,
        Debuff = 1
    }

    public enum HeroTypes
    {
        Barbarian = 0,
        Wizard = 1,
        Monk = 2,
        DemonHunter = 4,
        Crusader = 8,
        WichDoctor = 16
    }

    public enum Operators
    {
        None,
        Equals,
        Greater,
        GreaterOrEquals,
        Minor,
        MinorOrEquals
    }

    public enum TriggerReturns
    {
        Unknown,
        True,
        False
    }
}