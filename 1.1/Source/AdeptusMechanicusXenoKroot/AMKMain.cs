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
    public class AMKMain
    {
        public static List<ResearchProjectDef> KrootReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Kroot_Tech_")).ToList();
        public static List<ResearchProjectDef> TauReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Tau_Tech_")).ToList();
        static AMKMain()
        {
            AlienRace.ThingDef_AlienRace kroot = OGKrootDefOf.OG_Alien_Kroot as AlienRace.ThingDef_AlienRace;
            AlienRaceUtility.DoRacialRestrictionsFor(kroot, "K", KrootReseach);
            //    ArmouryMain.DoRacialRestrictionsFor(kroot, "T", TauReseach);
        }

    }
}