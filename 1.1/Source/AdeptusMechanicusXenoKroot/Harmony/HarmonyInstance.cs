using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;
using AlienRace;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdeptusMechanicus.HarmonyInstance
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.ogliss.rimworld.mod.AdeptusMechanicus.Kroot");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            int patches = harmony.GetPatchedMethods().Select(new Func<MethodBase, Patches>(Harmony.GetPatchInfo)).SelectMany((Patches p) => p.Prefixes.Concat(p.Postfixes).Concat(p.Transpilers)).Count((Patch p) => p.owner.Contains(harmony.Id));
            if (Prefs.DevMode) Log.Message(string.Format("Adeptus Xenobiologis: Kroot: successfully completed {0} harmony patches.", patches), false);
        }
    }

}