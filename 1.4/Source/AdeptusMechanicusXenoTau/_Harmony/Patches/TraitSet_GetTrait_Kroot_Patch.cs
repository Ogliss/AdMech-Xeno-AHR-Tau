using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using HarmonyLib;
using Verse.Sound;
using AdeptusMechanicus;
using AdeptusMechanicus.ExtensionMethods;

namespace AdeptusMechanicus.HarmonyInstance
{
    [HarmonyPatch(typeof(TraitSet), "GetTrait", new Type[] { typeof(TraitDef) })]
    [HarmonyPatch(typeof(TraitSet), "GetTrait", new Type[] { typeof(TraitDef), typeof(int) })]
    public static class TraitSet_GetTrait_Kroot_Patch
    {
        private static Trait cannibal = new Trait(TraitDefOf.Cannibal);
        private static Trait fastLearner = new Trait(AdeptusTraitDefOf.FastLearner);
        [HarmonyPostfix]
        public static void Postfix(TraitDef tDef, Pawn ___pawn, ref Trait __result)
        {
            if (___pawn != null)
            {
                if (___pawn.isKroot())
                {
                    if (tDef == TraitDefOf.Cannibal)
                    {
                        __result = cannibal;
                    }
                    if (tDef == AdeptusTraitDefOf.FastLearner)
                    {
                        __result = fastLearner;
                    }
                }
            }
        }
    }
}
