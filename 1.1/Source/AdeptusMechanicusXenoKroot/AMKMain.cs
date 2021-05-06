using System;
using System.Collections.Generic;
using System.Linq;
using AdeptusMechanicus.HarmonyInstance;
using AdeptusMechanicus.settings;
using AdeptusMechanicus.ExtensionMethods;
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
            List<string> blackTags = new List<string>() { "I", "C" };
            List<ResearchProjectDef> blackProjects = new List<ResearchProjectDef>();
            blackProjects.AddRange(ArmouryMain.ReseachImperial);
            blackProjects.AddRange(ArmouryMain.ReseachChaos);

            List<ResearchProjectDef> whiteProjects = new List<ResearchProjectDef>(KrootReseach);

            List<ThingDef> whiteAnimals = DefDatabase<ThingDef>.AllDefsListForReading.Where(x => x.race != null && x.race.Animal && x.isKrootoid()).ToList();

            AlienRaceUtility.DoRacialRestrictionsFor(KrootDefOf.OG_Alien_Kroot, "K", blackTags, whiteProjects, blackProjects, whiteAnimals: whiteAnimals);
            //    ArmouryMain.DoRacialRestrictionsFor(kroot, "T", TauReseach);
        }

    }
}