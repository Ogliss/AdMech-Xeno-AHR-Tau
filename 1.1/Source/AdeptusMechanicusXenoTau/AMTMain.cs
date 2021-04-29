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
    public class AMTMain
    {
        public static List<ResearchProjectDef> TauReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Tau_Tech_")).ToList();
        static AMTMain()
        {
            AlienRace.ThingDef_AlienRace tau = TauDefOf.OG_Alien_Tau as AlienRace.ThingDef_AlienRace;
            AlienRaceUtility.DoRacialRestrictionsFor(tau, "T", TauReseach);
        }

    }
}