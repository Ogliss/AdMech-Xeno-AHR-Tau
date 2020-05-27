using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEngine;
using AdeptusMechanicus.settings;
using System.Linq;
using AdeptusMechanicus.ExtensionMethods;

namespace AdeptusMechanicus
{
    [HarmonyPatch(typeof(AMAMod), "ModLoaded")]
    public static class AMXB_AMAMod_SettingsCategory_Patch
    {
        [HarmonyPostfix]
        public static void ModsLoaded(ref AMAMod __instance, ref string __result)
        {
            __result += ", " + "AMT_ModName".Translate();
        }
    }

    /*
    [HarmonyPatch(typeof(AMAMod), "SettingsCategory")]
    public static class AMT_AMAMod_SettingsCategory_Patch
    {
        [HarmonyPostfix, HarmonyPriority(399)]
        public static void SettingsCategory_Postfix(ref AMAMod __instance, ref string __result)
        {
            __result += ", " + "AMT_ModName".Translate();
        }
    }
    [HarmonyPatch(typeof(AMAMod), "get_MenuLength")]
    public static class AMO_AMAMod_MenuLength_Patch
    {
        [HarmonyPostfix]
        public static void MenuLength_Postfix(ref float __result)
        {
            //    Log.Message(string.Format("PreModOptions_Prefix num2: {0}",  num2));
            __result += (AMSettings.Instance.ShowOrk ? (AdeptusIntergrationUtil.enabled_MagosXenobiologis ? 60f : 120f) : 0);

            //    Log.Message(string.Format("PreModOptions_Prefix num2: {0}", num2));
        }

    }
	*/
    [HarmonyPatch(typeof(AMAMod), "TauSettings")]
    public static class AMT_AMMod_PlayableTauSettings_Patch
    {
        [HarmonyPrefix, HarmonyPriority(401)]
        public static bool TauSettings_Prefix(ref AMAMod __instance, ref Listing_Standard listing_Main, Rect rect, Rect inRect, float num, float num2)
        {
            AMSettings settings = __instance.settings;
            bool showRaces = settings.ShowAllowedRaceSettings;
            bool setting = settings.ShowAllowedRaceSettings && settings.ShowTau;
            float lineheight = (Text.LineHeight + listing_Main.verticalSpacing);
            float w = rect.width * 0.480f;
            int Options = 3;
            float RaceSettings = __instance.Length(setting, Options, lineheight, 8, showRaces ? 1 : 0); //(settings.ShowImperium ? (lineheight * 2) : (lineheight * 1)) + (settings.ShowImperium ? 10 : 0);
            float options = __instance.Length(setting, Options - 1, lineheight, 0, 0);

            Listing_Standard listing_Race = listing_Main.BeginSection(RaceSettings);
            listing_Race.CheckboxLabeled("AMXB_ShowTau".Translate() + " Settings" + (Prefs.DevMode && SteamUtility.SteamPersonaName.Contains("Ogliss") ? " Menu Length: " + RaceSettings : "") + (Prefs.DevMode && SteamUtility.SteamPersonaName.Contains("Ogliss") && setting ? " options length: " + options : ""), ref settings.ShowTau, null, false, true);

            if (setting)
            {
                Listing_Standard listing_General = listing_Race.BeginSection(options, true);
                listing_General.ColumnWidth *= 0.32f;
                listing_General.CheckboxLabeled("AMXB_AllowTau".Translate() + (!DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Tau")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Faction".Translate()), ref settings.AllowTau, null, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Tau")), DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Tau")));
                listing_General.CheckboxLabeled("AMXB_AllowGueVesa".Translate() + (!DefDatabase<PawnKindDef>.AllDefs.Any(x => x.defName.Contains("OG_Guevesa")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Auxiliaries".Translate()), ref settings.AllowGueVesaAuxiliaries, null, !DefDatabase<PawnKindDef>.AllDefs.Any(x => x.defName.Contains("OG_Guevesa")), DefDatabase<PawnKindDef>.AllDefs.Any(x => x.defName.Contains("OG_Guevesa")));
                listing_General.NewColumn();
                listing_General.CheckboxLabeled("AMXB_AllowKroot".Translate() + (!DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Kroot")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Faction".Translate()), ref settings.AllowKroot, null, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Kroot")), DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Kroot")));
                listing_General.CheckboxLabeled("AMXB_AllowKroot".Translate() + (!DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Kroot")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Auxiliaries".Translate()), ref settings.AllowKrootAuxiliaries, null, !DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Kroot")), DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Kroot")));
                listing_General.NewColumn();
                listing_General.CheckboxLabeled("AMXB_AllowVespid".Translate() + (!DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Faction".Translate()), ref settings.AllowVespid, null, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Vespid")), DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Vespid")));
                listing_General.CheckboxLabeled("AMXB_AllowVespid".Translate() + (!DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Auxiliaries".Translate()), ref settings.AllowVespidAuxiliaries, null, !DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")), DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")));
                listing_Race.EndSection(listing_General);
            }
            listing_Main.EndSection(listing_Race);
            __instance.XenobiologisTauMenuLength = RaceSettings;

            return false;
        }
    }

    /*
    [HarmonyPatch(typeof(AMAMod), "PostModOptions")]
    public static class AMO_AMAMod_PostModOptions_Patch
    {
        [HarmonyPostfix, HarmonyPriority(399)]
        public static void PostModOptions_Prefix(ref Listing_Standard listing_Standard, Rect inRect, ref float num, ref float num2)
        {
            AMSettings settings = AMSettings.Instance;
            settings.Write();
        }

    }
    */
}