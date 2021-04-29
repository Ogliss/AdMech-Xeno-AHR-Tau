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
            return pawn.race == TauDefOf.OG_Alien_Tau || pawn.RaceProps.BloodDef == TauDefOf.OG_FilthBlood_Tau;
        }

        public static bool isTau(this Pawn pawn)
        {
            return pawn.def == TauDefOf.OG_Alien_Tau || pawn.RaceProps.BloodDef == TauDefOf.OG_FilthBlood_Tau;
        }

        public static bool isTau(this ThingDef pawn)
        {
            return pawn == TauDefOf.OG_Alien_Tau || pawn.race.BloodDef == TauDefOf.OG_FilthBlood_Tau;
        }

        public static bool EtheralCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_AUN");
        }
        public static bool EtheralCaste(this Pawn pawn)
        {
            return pawn.story.childhood.identifier.Contains("Tau_Aun");
        }


        public static bool FireCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_SHAS");
        }
        public static bool FireCaste(this Pawn pawn)
        {
            return pawn.story.childhood.identifier.Contains("Tau_Shas");
        }


        public static bool EarthCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_FIO");
        }
        public static bool EarthCaste(this Pawn pawn)
        {
            return pawn.story.childhood.identifier.Contains("Tau_Fio");
        }

        public static bool AirCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_KOR");
        }
        public static bool AirCaste(this Pawn pawn)
        {
            return pawn.story.childhood.identifier.Contains("Tau_Kor");
        }


        public static bool WaterCaste(this PawnKindDef pawn)
        {
            return pawn.backstoryCategories.Contains("Tau_POR");
        }
        public static bool WaterCaste(this Pawn pawn)
        {
            return pawn.story.childhood.identifier.Contains("Tau_Por");
        }
    }

}
