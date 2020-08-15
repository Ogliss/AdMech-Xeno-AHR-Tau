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
    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new Type[] { typeof(PawnGenerationRequest) })]
    public static class AMXBT_PawnGenerator_GeneratePawn_Tau_Patch
    {
        public static void Postfix(ref Pawn __result)
        {
            if (__result.isTau())
			{
				NameTriple nameTriple = __result.Name as NameTriple;
                string pref = string.Empty;
				if (__result.EtheralCaste() && !nameTriple.First.Contains("Aun"))
                {
                    pref = "Aun";
                }
                if (__result.FireCaste() && !nameTriple.First.Contains("Shas"))
                {
                    pref = "Shas";
				}
                if (__result.AirCaste() && !nameTriple.First.Contains("Kor"))
                {
                    pref = "Kor";
                }
                if (__result.EarthCaste() && !nameTriple.First.Contains("Fio"))
                {
                    pref = "Fio";
                }
                if (__result.WaterCaste() && !nameTriple.First.Contains("Por"))
                {
                    pref = "Por";
                }
                if (!pref.NullOrEmpty())
                {
                    if (__result.story?.adulthood != null)
                    {
                        if (__result.story.adulthood.title.Contains("'La"))
                        {
                            pref += "'La";
                        }
                        else
                        if (__result.story.adulthood.title.Contains("'Ui"))
                        {
                            pref += "'Ui";
                        }
                        else
                        if (__result.story.adulthood.title.Contains("'Vre"))
                        {
                            pref += "'Vre";
                        }
                        else
                        if (__result.story.adulthood.title.Contains("'El"))
                        {
                            pref += "'El";
                        } 
                        else
                        if (__result.story.adulthood.title.Contains("'O"))
                        {
                            pref += "'O";
                        }
                        else
                        {
                            pref += "'Saal";
                        }
                    }
                    else
                    {
                        pref += "'Saal";
                    }
                    __result.Name = new NameTriple(pref + " " + nameTriple.First, nameTriple.Nick, nameTriple.Last);
                }
            }
        }

	}
}
