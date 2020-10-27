using System;
using System.Collections.Generic;
using System.Linq;
using AdeptusMechanicus.HarmonyInstance;
using AdeptusMechanicus.settings;
using RimWorld;
using Verse;
using Verse.AI;

namespace AdeptusMechanicus
{
    [StaticConstructorOnStartup]
    public class AMVMain
    {
        public static List<ResearchProjectDef> VespidReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Vespid_Tech_")).ToList();
        public static List<ResearchProjectDef> TauReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Tau_Tech_")).ToList();
        static AMVMain()
        {
            AlienRace.ThingDef_AlienRace vespid = OGVespidDefOf.OG_Alien_Vespid as AlienRace.ThingDef_AlienRace;
            ArmouryMain.DoRacialRestrictionsFor(vespid, "V", VespidReseach);
            //    ArmouryMain.DoRacialRestrictionsFor(vespid, "T", TauReseach);
        }

    }
}