using Verse;
using HarmonyLib;
using AdeptusMechanicus.ExtensionMethods;

namespace AdeptusMechanicus.HarmonyInstance
{
    [HarmonyPatch(typeof(AlienRaceUtility), "RaceAgeSkillFactor")]
    public static class AlienRaceUtility_RaceAgeSkillFactor_Tau_Patch
    {
        [HarmonyPostfix]
        public static SimpleCurve Postfix(SimpleCurve __result, Pawn pawn)
        {
            if (pawn != null && pawn.RaceProps.Humanlike)
            {
                if (pawn.isTau()) return AgeSkillFactorTau;
                if (pawn.isKroot()) return AgeSkillFactorKroot;
                if (pawn.isVespid()) return AgeSkillFactorVespid;
            //    if (AMAMod.Dev) Log.Message($"{pawn.Name} Using Tau AgeSkillMaxFactorCurve");

            }
            return __result;
        }

        private static readonly SimpleCurve AgeSkillFactorTau = new SimpleCurve
        {
            {
                new CurvePoint(0f, 0f),
                true
            },
            {
                new CurvePoint(5f, 0.75f),
                true
            },
            {
                new CurvePoint(10f, 1f),
                true
            }
        };
        private static readonly SimpleCurve AgeSkillFactorKroot = new SimpleCurve
        {
            {
                new CurvePoint(0f, 0f),
                true
            },
            {
                new CurvePoint(3f, 0.75f),
                true
            },
            {
                new CurvePoint(7f, 1f),
                true
            }
        };
        private static readonly SimpleCurve AgeSkillFactorVespid = new SimpleCurve
        {
            {
                new CurvePoint(0f, 0f),
                true
            },
            {
                new CurvePoint(3f, 0.75f),
                true
            },
            {
                new CurvePoint(7f, 1f),
                true
            }
        };
    }
}
