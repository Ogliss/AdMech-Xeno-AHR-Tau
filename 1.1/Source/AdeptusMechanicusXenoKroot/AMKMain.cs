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
        public static List<ResearchProjectDef> KrootReseach => DefDatabase<ResearchProjectDef>.AllDefs.Where(x => x.defName.Contains("OG_Tau_Tech_") || x.defName.Contains("OG_Kroot_Tech_")).ToList();
        static AMKMain()
        {
            AlienRace.ThingDef_AlienRace kroot = OGKrootDefOf.OG_Alien_Kroot as AlienRace.ThingDef_AlienRace;
            foreach (ResearchProjectDef def in KrootReseach)
            {
                if (!AlienRace.RaceRestrictionSettings.researchRestrictionDict.ContainsKey(key: def))
                    AlienRace.RaceRestrictionSettings.researchRestrictionDict.Add(key: def, value: new List<AlienRace.ThingDef_AlienRace>());
                AlienRace.RaceRestrictionSettings.researchRestrictionDict[key: def].Add(item: kroot);
            }

            HarmonyPatches.TryAddRacialRestrictions(kroot, "K");
        }

    }
}