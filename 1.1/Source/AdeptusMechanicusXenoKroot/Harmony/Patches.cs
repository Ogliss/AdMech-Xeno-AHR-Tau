﻿using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;
using AlienRace;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdeptusMechanicus
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.ogliss.rimworld.mod.AdeptusMechanicus.Kroot");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message(string.Format("Adeptus Mecanicus: Kroot: successfully completed {0} harmony patches.", harmony.GetPatchedMethods().Select(new Func<MethodBase, Patches>(Harmony.GetPatchInfo)).SelectMany((Patches p) => p.Prefixes.Concat(p.Postfixes).Concat(p.Transpilers)).Count((Patch p) => p.owner.Contains(harmony.Id))), false);
        }
    }

    [HarmonyPatch(typeof(AlienRace.HarmonyPatches), "ThoughtsFromIngestingPostfix")]
    public static class AdMech_AlienRace_HarmonyPatches_ThoughtsFromIngestingPostfix_Patch
    {
        [HarmonyPrefix]
        public static bool ThoughtsFromIngestingPostPrefix(Pawn ingester, Thing foodSource)
        {
            return false;
            /*
            if (ingester.def == OGKrootDefOf.Alien_Kroot)
            {
                Log.Message(string.Format("diabled for krooty goodness"));
                return false;
            }
            if (ingester.def.defName.Contains("Alien_Ork"))
            {
                Log.Message(string.Format("diabled for orky goodness"));
                return false;
            }
            */
            return true;
        }
        /*
        [HarmonyPostfix]
        public static void ThoughtsFromIngestingPostPostfix(Pawn ingester, Thing foodSource)
        {
            Log.Message(string.Format("tet {0} eating {1}", ingester, foodSource));
            if (foodSource.TryGetComp<CompIngredients>() != null && foodSource.TryGetComp<CompIngredients>() is CompIngredients ingeds)
            {
                Log.Message(string.Format("with {0} ingredients", ingeds.ingredients.Count));
                foreach (var item in ingeds.ingredients)
                {
                    string text = string.Format("{0} has {1} ingredient", foodSource, item);
                    if (item.ingestible.sourceDef!=null && item.ingestible.sourceDef.race!=null && item.ingestible.sourceDef.race.Humanlike) text += string.Format("\n{0} is from a {1}", item, item.ingestible.sourceDef);
                    Log.Message(string.Format(text));
                }
            }
        }
        */
    }

    [HarmonyPatch(typeof(FoodUtility), "ThoughtsFromIngesting")]
    public static class AdMech_FoodUtility_ThoughtsFromIngesting_Patch
    {
        /*
        [HarmonyPrefix]
        public static bool ThoughtsFromIngestingPostPrefix(Pawn ingester, Thing foodSource)
        {
            if (ingester.def == OGKrootDefOf.Alien_Kroot)
            {
                Log.Message(string.Format("diabled for krooty goodness"));
                return false;
            }
            return true;
        }
        */

        [HarmonyPostfix]
        public static void ThoughtsFromIngestingPostPostfix(Pawn ingester, Thing foodSource, ref List<ThoughtDef> __result)
        {
            if (ingester.story.traits.HasTrait(tDef: AlienDefOf.Xenophobia) && ingester.story.traits.DegreeOfTrait(tDef: AlienDefOf.Xenophobia) == 1)
                if (__result.Contains(item: ThoughtDefOf.AteHumanlikeMeatDirect) && foodSource.def.ingestible.sourceDef != ingester.def)
                    __result.Remove(item: ThoughtDefOf.AteHumanlikeMeatDirect);
                else if (__result.Contains(item: ThoughtDefOf.AteHumanlikeMeatAsIngredient) &&
                         (foodSource.TryGetComp<CompIngredients>()?.ingredients.Any(predicate: td => FoodUtility.IsHumanlikeMeat(def: td) && td.ingestible.sourceDef != ingester.def) ?? false))
                    __result.Remove(item: ThoughtDefOf.AteHumanlikeMeatAsIngredient);

            if (!(ingester.def is ThingDef_AlienRace alienProps)) return;

            bool cannibal = ingester.story.traits.HasTrait(tDef: TraitDefOf.Cannibal);

            for (int i = 0; i < __result.Count; i++)
            {
                ThoughtDef thoughtDef = __result[index: i];
                ThoughtSettings settings = alienProps.alienRace.thoughtSettings;

                thoughtDef = settings.ReplaceIfApplicable(def: thoughtDef);

                if (thoughtDef == ThoughtDefOf.AteHumanlikeMeatDirect || thoughtDef == ThoughtDefOf.AteHumanlikeMeatDirectCannibal)
                    thoughtDef = settings.GetAteThought(race: foodSource.def.ingestible.sourceDef, cannibal: cannibal, ingredient: false);

                if (thoughtDef == ThoughtDefOf.AteHumanlikeMeatAsIngredient || thoughtDef == ThoughtDefOf.AteHumanlikeMeatAsIngredientCannibal)
                {
                    ThingDef race = null;
                    if (foodSource.TryGetComp<CompIngredients>() != null && foodSource.TryGetComp<CompIngredients>() is CompIngredients ingeds)
                    {
                        foreach (var item in ingeds.ingredients)
                        {
                            if (item.ingestible.sourceDef != null && item.ingestible.sourceDef.race != null && item.ingestible.sourceDef.race.Humanlike) race = (item.ingestible.sourceDef);
                        }
                    }

                    //    race = foodSource.TryGetComp<CompIngredients>()?.ingredients.Find(td => td.ingestible.sourceDef.race.Humanlike);

                    if (race != null)
                        thoughtDef = settings.GetAteThought(race: race, cannibal: cannibal, ingredient: true);
                }

                __result[index: i] = thoughtDef;
            }
        }
    }

}