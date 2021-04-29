using RimWorld;
using System;
using Verse;

namespace AdeptusMechanicus
{
    [DefOf]
    public static class TauDefOf
    {
        static TauDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TauDefOf));
        }

        public static ThingDef OG_Alien_Tau;
        public static BodyDef OG_Tau_Body;
        public static ThingDef OG_FilthBlood_Tau;

        public static ThingDef OG_Alien_Kroot;
        public static BodyDef OG_Kroot_Body;
        public static ThingDef OG_FilthBlood_Kroot;

        public static BodyDef OG_Vespid_Body;
        public static ThingDef OG_Alien_Vespid;
        public static ThingDef OG_FilthBlood_Vespid;

    }
}
