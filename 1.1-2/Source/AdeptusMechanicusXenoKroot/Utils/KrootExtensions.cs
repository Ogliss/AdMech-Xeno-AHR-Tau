using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace AdeptusMechanicus.ExtensionMethods
{

    public static class KrootExtensions
    {
        public static bool isKroot(this Pawn pawn)
        {
            return pawn.def.isKroot();
        }
        public static bool isKrootoid(this Pawn pawn)
        {
            return pawn.def.isKrootoid();
        }
        
        public static bool isKroot(this Thing pawn)
        {
            return pawn.def.isKroot();
        }
        public static bool isKrootoid(this Thing pawn)
        {
            return pawn.def.isKrootoid();
        }

        public static bool isKroot(this ThingDef def)
        {
            return def == AdeptusThingDefOf.OG_Alien_Kroot;
        }
        public static bool isKrootoid(this ThingDef def)
        {
            return def.race.BloodDef == AdeptusThingDefOf.OG_FilthBlood_Kroot;
        }

    }

}
