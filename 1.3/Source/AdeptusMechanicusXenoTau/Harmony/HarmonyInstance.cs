﻿using AlienRace;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AdeptusMechanicus.HarmonyInstance
{
    [StaticConstructorOnStartup]
    static class HarmonyInstance
    {
        static HarmonyInstance()
        {
            Harmony harmony = new Harmony("rimworld.ogliss.adeptusmechanicus.tau");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            /*
            if (AdeptusIntergrationUtility.enabled_SOS2)
            {
                HarmonyPatches.SOSConstructPatch();
            }
            */
            if (Prefs.DevMode) Log.Message(string.Format("Adeptus Xenobiologis: Tau: successfully completed {0} harmony patches.", harmony.GetPatchedMethods().Select(new Func<MethodBase, Patches>(Harmony.GetPatchInfo)).SelectMany((Patches p) => p.Prefixes.Concat(p.Postfixes).Concat(p.Transpilers)).Count((Patch p) => p.owner.Contains(harmony.Id))));
        }
    }
}