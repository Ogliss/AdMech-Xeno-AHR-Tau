using RimWorld;
using Verse;
using Harmony;
using System.Reflection;
using AlienRace;
using System.Collections.Generic;
using System.Linq;

namespace AdeptusMechanicus
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.ogliss.rimworld.mod.AdeptusMechanicus.Kroot");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
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

    /*
    [HarmonyPatch(typeof(AlienRace.ThoughtSettings), "GetAteThought")]
    public static class AdMech_ThoughtSettings_GetAteThought_Patch
    {
        [HarmonyPrefix]
        public static bool GetAteThought(AlienRace.ThoughtSettings __instance, ThingDef race, bool cannibal, bool ingredient, ref ThoughtDef __result)
        {
            __result = null;
            Log.Message(string.Format("ingested race: {0}", race));
            Log.Message(string.Format("ingester __instance: {0}", __instance));
            Pawn pawn = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();
            Log.Message(string.Format("ingester pawn: {0}", pawn));
            if (race != null)
            {
                Log.Message(string.Format("ingested race: {0}", race.defName, __instance));
                if (race.race.Humanlike)
                {
                    Log.Message(string.Format("{0} is Humanlike", race.defName));
                    if (__instance.ateThoughtSpecific.Count > 0)
                    {
                        if (__instance.ateThoughtSpecific.Any(x => x.raceList.Contains(race.defName)))
                        {
                            var item2 = __instance.ateThoughtSpecific.Find(x => x.raceList.Contains(race.defName));
                            Log.Message(string.Format("found ateThought Specific for {0}: {1}", race, __instance.ateThoughtSpecific.FindAll(x => x.raceList.Contains(race.defName)).Count));
                            string defname = ingredient ? (cannibal ? item2.ingredientThoughtCannibal : item2.ingredientThought) : (cannibal ? item2.thoughtCannibal : item2.thought);
                            ThoughtDef thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defname);
                            __result = thoughtDef;
                            Log.Message(string.Format("found Specific thoughtDef : {0}", thoughtDef));
                            return false;
                        }
                    }
                    if (__instance.ateThoughtGeneral != null && __instance.ateThoughtGeneral is AteThought item)
                    {
                        Log.Message(string.Format("found ateThought General ingredientThought: {0}, ingredientThoughtCannibal: {1} , thought: {2}, thoughtCannibal: {3}", item.ingredientThought, item.ingredientThoughtCannibal, item.thought, item.thoughtCannibal));
                        string defname = ingredient ? (cannibal ? item.ingredientThoughtCannibal : item.ingredientThought) : (cannibal ? item.thoughtCannibal : item.thought);
                        ThoughtDef thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defname);
                        __result = thoughtDef;
                        Log.Message(string.Format("found General thoughtDef : {0}", thoughtDef));
                    }
                    else
                    {
                        Log.Message(string.Format("found ateThought Vanillia ingredientThought: {0}, ingredientThoughtCannibal: {1} , thought: {2}, thoughtCannibal: {3}", ThoughtDefOf.AteHumanlikeMeatAsIngredient, ThoughtDefOf.AteHumanlikeMeatAsIngredientCannibal, ThoughtDefOf.AteHumanlikeMeatDirect, ThoughtDefOf.AteHumanlikeMeatDirectCannibal));
                        string defname = ingredient ? (cannibal ? ThoughtDefOf.AteHumanlikeMeatAsIngredientCannibal.defName : ThoughtDefOf.AteHumanlikeMeatAsIngredient.defName) : (cannibal ? ThoughtDefOf.AteHumanlikeMeatDirectCannibal.defName : ThoughtDefOf.AteHumanlikeMeatDirect.defName);
                        ThoughtDef thoughtDef = DefDatabase<ThoughtDef>.GetNamed(defname);
                        __result = thoughtDef;
                        Log.Message(string.Format("found Vanillia thoughtDef : {0}", thoughtDef));
                    }
                    return false;
                }
                return false;
            }
            
            return false;
        }
    }
    */
}