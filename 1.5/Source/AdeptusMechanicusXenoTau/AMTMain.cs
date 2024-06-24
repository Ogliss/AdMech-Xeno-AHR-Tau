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
    public class AMTMain
    {
        public static List<ResearchProjectDef> KrootReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Kroot_Tech_")).ToList();
        public static List<ResearchProjectDef> TauReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Tau_Tech_")).ToList();
        public static List<ResearchProjectDef> VespidReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Vespid_Tech_")).ToList();
        static AMTMain()
        {
            List<string> blackTags = ArmouryMain.humansTags;
            List<ResearchProjectDef> blackProjects = new List<ResearchProjectDef>();
            blackProjects.AddRange(ArmouryMain.ReseachImperial);
            blackProjects.AddRange(ArmouryMain.ReseachChaos);

            List<ResearchProjectDef> whiteProjects = new List<ResearchProjectDef>(TauReseach);
            AlienRaceUtility.DoRacialRestrictionsFor(AdeptusThingDefOf.OG_Alien_Tau, "T", blackTags, whiteProjects, blackProjects);

            whiteProjects = new List<ResearchProjectDef>(KrootReseach);
            List<ThingDef> whiteAnimals = DefDatabase<ThingDef>.AllDefsListForReading.Where(x => x.race != null && x.race.Animal && x.isKrootoid()).ToList();
            AlienRaceUtility.DoRacialRestrictionsFor(AdeptusThingDefOf.OG_Alien_Kroot, "K", blackTags, whiteProjects, blackProjects, whiteAnimals: whiteAnimals);



            whiteProjects = new List<ResearchProjectDef>(VespidReseach);
            AlienRace.ThingDef_AlienRace vespid = AdeptusThingDefOf.OG_Alien_Vespid as AlienRace.ThingDef_AlienRace;
            AlienRaceUtility.DoRacialRestrictionsFor(vespid, "V", blackTags, whiteProjects, blackProjects);
        }

    }
}