using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace AdeptusMechanicus.ExtensionMethods
{

    public static class TauExtensions
    {
        public static bool isTau(this PawnKindDef pawn)
        {
            return pawn.race == AdeptusThingDefOf.OG_Alien_Tau || pawn.RaceProps.BloodDef == AdeptusThingDefOf.OG_FilthBlood_Tau;
        }

        public static bool isTau(this Pawn pawn)
        {
            return pawn.def == AdeptusThingDefOf.OG_Alien_Tau || pawn.RaceProps.BloodDef == AdeptusThingDefOf.OG_FilthBlood_Tau;
        }

        public static bool isTau(this ThingDef pawn)
        {
            return pawn == AdeptusThingDefOf.OG_Alien_Tau || pawn.race.BloodDef == AdeptusThingDefOf.OG_FilthBlood_Tau;
        }

        public static bool EtheralCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories != null && pawn.backstoryCategories.Contains("Tau_AUN");
        }
        public static bool EtheralCaste(this Pawn pawn)
        {
            return pawn.story?.childhood != null && pawn.story.childhood.defName.ToLower().Contains("tau_aun");
        }


        public static bool FireCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_SHAS");
        }
        public static bool FireCaste(this Pawn pawn)
        {
            return pawn.story.childhood.defName.ToLower().Contains("tau_shas");
        }


        public static bool EarthCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_FIO");
        }
        public static bool EarthCaste(this Pawn pawn)
        {
            return pawn.story.childhood.defName.ToLower().Contains("tau_fio");
        }

        public static bool AirCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_KOR");
        }
        public static bool AirCaste(this Pawn pawn)
        {
            return pawn.story.childhood.defName.ToLower().Contains("tau_kor");
        }


        public static bool WaterCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_POR");
        }
        public static bool WaterCaste(this Pawn pawn)
        {
            return pawn.story.childhood.defName.ToLower().Contains("tau_por");
        }
    }

}
