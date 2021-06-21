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
            List<string> blackTags = new List<string>() { "I", "C" };
            List<ResearchProjectDef> blackProjects = new List<ResearchProjectDef>();
            blackProjects.AddRange(ArmouryMain.ReseachImperial);
            blackProjects.AddRange(ArmouryMain.ReseachChaos);

            List<ResearchProjectDef> whiteProjects = new List<ResearchProjectDef>(TauReseach);
            AlienRaceUtility.DoRacialRestrictionsFor(AdeptusThingDefOf.OG_Alien_Tau, "T", blackTags, whiteProjects, blackProjects);
        }

    }
}