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
    [HarmonyPatch(typeof(FoodUtility), "BestFoodSourceOnMap")]
    public static class FoodUtility_BestFoodSourceOnMap_Kroot_Patch
    {
        [HarmonyPrefix, HarmonyPriority(Priority.Last)]
        public static void Prefix(Pawn getter, Pawn eater, ref FoodPreferability minPrefOverride,ref bool allowCorpse,ref bool allowSociallyImproper)
        {
            if (eater.isKroot())
            {
                minPrefOverride = FoodPreferability.DesperateOnly;
                allowCorpse = true;
                allowSociallyImproper = true;
            }
        }
    }
}
