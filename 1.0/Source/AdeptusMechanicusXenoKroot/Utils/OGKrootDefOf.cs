using System;
using Verse;

namespace RimWorld
{
    // Token: 0x02000945 RID: 2373
    [DefOf]
    public static class OGKrootDefOf
    {
        // Token: 0x0600376E RID: 14190 RVA: 0x001A82DE File Offset: 0x001A66DE
        static OGKrootDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OGKrootDefOf));
        }

        // Token: 0x04001E56 RID: 7766
        public static BodyDef OG_Kroot;
        public static BodyDef OG_Tau;


        public static ThingDef Alien_Tau;
        public static ThingDef OG_Alien_Tau;
        //    public static ThingDef OG_Alien_Vespid;
        public static ThingDef Alien_Kroot;
        public static ThingDef OG_Alien_Kroot;
        public static ThingDef KrootHound;
        public static ThingDef KrootOx;
        public static ThingDef OG_Knarloc;

    }
}
