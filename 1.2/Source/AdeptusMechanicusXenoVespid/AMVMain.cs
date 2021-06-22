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
            List<string> blackTags = new List<string>() { "I", "C" };
            List<ResearchProjectDef> blackProjects = new List<ResearchProjectDef>();
            blackProjects.AddRange(ArmouryMain.ReseachImperial);
            blackProjects.AddRange(ArmouryMain.ReseachChaos);

            List<ResearchProjectDef> whiteProjects = new List<ResearchProjectDef>(VespidReseach);
            AlienRace.ThingDef_AlienRace vespid = AdeptusThingDefOf.OG_Alien_Vespid as AlienRace.ThingDef_AlienRace;
            AlienRaceUtility.DoRacialRestrictionsFor(vespid, "V", blackTags, whiteProjects, blackProjects);
            //    ArmouryMain.DoRacialRestrictionsFor(vespid, "T", TauReseach);
        }

    }
}