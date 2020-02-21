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
    /*
    [HarmonyPatch(typeof(AMAMod), "SettingsCategory")]
    public static class AMK_AMAMod_SettingsCategory_Patch
    {
        [HarmonyPostfix]
        public static void SettingsCategory_Postfix(ref AMAMod __instance, ref string __result)
        {
            __result += ", " + "AMXB_ModName".Translate();
        }
    }
    [HarmonyPatch(typeof(AMAMod), "PreModOptions")]
    public static class AMK_AMAMod_PreModOptions_Patch
    {
        [HarmonyPrefix]
        public static void PreModOptions_Prefix(ref Listing_Standard listing_Standard, Rect inRect, float num, ref float num2)
        {
            //    Log.Message("PreModOptions_Prefix");
            AMKsettings settings = AMKsettings.Instance;

            //    Log.Message(string.Format("PreModOptions_Prefix num2: {0}",  num2));
            num2 = num2 + (settings.ShowKroot ? 60f : 0f);
            //    Log.Message(string.Format("PreModOptions_Prefix num2: {0}", num2));
        }

    }
    [HarmonyPatch(typeof(AMAMod), "ModOptions")]
    public static class AMK_AMAMod_ModOptions_Patch
    {
        [HarmonyPostfix]
        public static void ModOptions_Postfix(ref Listing_Standard listing_Standard, Rect inRect, float num, float num2)
        {
            AMKsettings settings = AMKsettings.Instance;
            Rect rect = new Rect(inRect.x, inRect.y - 50, num, num2);
            listing_Standard.Label("AMXB_ModName".Translate() + " Settings");
            listing_Standard.Label("AMXB_AllowedRaces".Translate());
            listing_Standard.CheckboxLabeled("AMXB_ShowImperium".Translate(), ref settings.ShowKroot, "AMXB_ShowImperiumDesc".Translate());
            Rect rectImperium = new Rect(rect.x, rect.y + 10, num, 60f);
            if (settings.ShowKroot)
            {
                listing_Standard.BeginSection(60f);
                Widgets.CheckboxLabeled(rectImperium.TopHalf().LeftHalf().ContractedBy(4), "AMXB_AllowAdeptusAstartes".Translate() + (!DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Astartes")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_HiddenFaction".Translate()), ref settings.AllowKrootTrait, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Astartes")) || !AMASettings.Instance.AllowImperialWeapons);
                Widgets.CheckboxLabeled(rectImperium.BottomHalf().LeftHalf().ContractedBy(4), "AMXB_AllowAdeptusMechanicus".Translate() + (!DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Mechanicus")) ? "AMXB_NotYetAvailable".Translate() : "AMXB_HiddenFaction".Translate()), ref settings.AllowKrootMutations, !DefDatabase<FactionDef>.AllDefs.Any(x => x.defName.Contains("OG_Mechanicus")) || !AMASettings.Instance.AllowMechanicusWeapons);
                listing_Standard.EndSection(listing_Standard);
            }

        }
    }

    [HarmonyPatch(typeof(AMAMod), "PostModOptions")]
    public static class AMK_AMAMod_PostModOptions_Patch
    {
        [HarmonyPostfix]
        public static void PostModOptions_Prefix(ref Listing_Standard listing_Standard, Rect inRect, ref float num, ref float num2)
        {
            //    Log.Message("PostModOptions_Postfix");
            AMKsettings settings = AMKsettings.Instance;
            settings.Write();
        }

    }
    */
}