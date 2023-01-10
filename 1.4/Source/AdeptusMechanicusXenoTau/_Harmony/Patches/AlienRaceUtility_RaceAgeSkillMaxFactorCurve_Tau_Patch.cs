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
using AdeptusMechanicus.settings;

namespace AdeptusMechanicus.HarmonyInstance
{
    [HarmonyPatch(typeof(AlienRaceUtility), "RaceAgeSkillMaxFactorCurve")]
    public static class AlienRaceUtility_RaceAgeSkillMaxFactorCurve_Tau_Patch
    {
        [HarmonyPostfix]
        public static SimpleCurve Postfix(SimpleCurve __result, Pawn pawn)
        {
            if (pawn != null && pawn.RaceProps.Humanlike)
            {
                if (pawn.isTau()) return AgeSkillMaxFactorCurveTau;
                if (pawn.isKroot()) return AgeSkillMaxFactorCurveKroot;
                if (pawn.isVespid()) return AgeSkillMaxFactorCurveVespid;
                //    if (AMAMod.Dev) Log.Message($"{pawn.Name} Using Aeldari AgeSkillMaxFactorCurve");
            }
            
            return __result;
        }

        private static readonly SimpleCurve AgeSkillMaxFactorCurveTau = new SimpleCurve
        {
            {
                new CurvePoint(0f, 0.5f),
                true
            },
            {
                new CurvePoint(8f, 1f),
                true
            },
            {
                new CurvePoint(30f, 1.3f),
                true
            },
            {
                new CurvePoint(40f, 1.6f),
                true
            }
        };
        private static readonly SimpleCurve AgeSkillMaxFactorCurveKroot = new SimpleCurve
        {
            {
                new CurvePoint(0f, 0.5f),
                true
            },
            {
                new CurvePoint(3f, 1f),
                true
            },
            {
                new CurvePoint(30f, 1.3f),
                true
            },
            {
                new CurvePoint(50f, 1.9f),
                true
            }
        };
        private static readonly SimpleCurve AgeSkillMaxFactorCurveVespid = new SimpleCurve
        {
            {
                new CurvePoint(0f, 0.5f),
                true
            },
            {
                new CurvePoint(3f, 1f),
                true
            },
            {
                new CurvePoint(30f, 1.3f),
                true
            },
            {
                new CurvePoint(50f, 1.9f),
                true
            }
        };
    }
}
