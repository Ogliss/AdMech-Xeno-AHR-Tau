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
            return pawn.def == KrootDefOf.OG_Alien_Kroot;
        }

    }

}
