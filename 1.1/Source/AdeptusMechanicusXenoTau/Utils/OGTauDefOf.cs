using System;
using Verse;

namespace RimWorld
{
    // Token: 0x02000945 RID: 2373
    [DefOf]
    public static class OGTauDefOf
    {
        // Token: 0x0600376E RID: 14190 RVA: 0x001A82DE File Offset: 0x001A66DE
        static OGTauDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OGTauDefOf));
        }

        // Token: 0x04001E56 RID: 7766
        public static BodyDef OG_Kroot_Body;
        public static BodyDef OG_Tau_Body;

        // Token: 0x04001E56 RID: 7766
        public static ThingDef OG_Alien_Tau;
        public static ThingDef OG_FilthBlood_Tau;
        public static ThingDef OG_Alien_Kroot;
        public static ThingDef OG_FilthBlood_Kroot; 
        public static ThingDef OG_Alien_Vespid;
        public static ThingDef OG_FilthBlood_Vespid;

    }
}
