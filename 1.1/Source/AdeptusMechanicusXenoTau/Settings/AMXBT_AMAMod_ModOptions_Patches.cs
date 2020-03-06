using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEngine;
using AdeptusMechanicus.settings;
using System.Linq;

namespace AdeptusMechanicus
{
    [HarmonyPatch(typeof(AMAMod), "SettingsCategory")]
    public static class AMT_AMAMod_SettingsCategory_Patch
    {
        [HarmonyPostfix, HarmonyPriority(399)]
        public static void SettingsCategory_Postfix(ref AMAMod __instance, ref string __result)
        {
            __result += ", " + "AMT_ModName".Translate();
        }
    }
	/*
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
    [HarmonyPatch(typeof(AMMod), "TauSettings")]
    public static class AMT_AMMod_PlayableTauSettings_Patch
    {
        [HarmonyPrefix, HarmonyPriority(401)]
        public static bool TauSettings_Prefix(ref Listing_Standard listing_Standard, Rect rect, Rect inRect, float num, float num2)
        {
            AMSettings AMAsettings = SettingsHelper.latest;
            listing_Standard.CheckboxLabeled("AMT_ModName".Translate() + " Settings", ref AMSettings.Instance.ShowTau);
            if (AMAsettings.ShowTau)
            {
                Rect rect2 = new Rect(rect.x, rect.y + 10, num, 120f);
                Show(ref listing_Standard, rect2, AMAsettings);
            }
            return false;
        }
        static void Show(ref Listing_Standard listing_Standard, Rect rect2, AMSettings settings)
        {
            listing_Standard.BeginSection(60f);
            Widgets.CheckboxLabeled(rect2.TopHalf().LeftHalf().ContractedBy(4), "AMXB_AllowTau".Translate() + (!DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Tau")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Faction".Translate()), ref settings.AllowTau, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Tau")));
            Widgets.CheckboxLabeled(rect2.BottomHalf().RightHalf().LeftHalf().ContractedBy(4), "AMXB_AllowKroot".Translate() + (!DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Kroot")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Auxiliaries".Translate()), ref settings.AllowKrootAuxiliaries, !DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Kroot")));
            Widgets.CheckboxLabeled(rect2.BottomHalf().RightHalf().RightHalf().ContractedBy(4), "AMXB_AllowKroot".Translate() + (!DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Kroot")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Faction".Translate()), ref settings.AllowKroot, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Kroot")));
            Widgets.CheckboxLabeled(rect2.BottomHalf().LeftHalf().ContractedBy(4), "AMXB_AllowGueVesa".Translate() + (!DefDatabase<PawnKindDef>.AllDefs.Any(x => x.defName.Contains("OG_Guevesa")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Auxiliaries".Translate()), ref settings.AllowGueVesaAuxiliaries, !DefDatabase<PawnKindDef>.AllDefs.Any(x => x.defName.Contains("OG_Guevesa")));
            Widgets.CheckboxLabeled(rect2.TopHalf().RightHalf().LeftHalf().ContractedBy(4), "AMXB_AllowVespid".Translate() + (!DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Auxiliaries".Translate()), ref settings.AllowVespidAuxiliaries, !DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")));
            Widgets.CheckboxLabeled(rect2.TopHalf().RightHalf().RightHalf().ContractedBy(4), "AMXB_AllowVespid".Translate() + (!DefDatabase<ThingDef>.AllDefs.Any(x => x.defName.Contains("OG_Alien_Vespid")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_Faction".Translate()), ref settings.AllowVespid, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Vespid")));
            listing_Standard.EndSection(listing_Standard);
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