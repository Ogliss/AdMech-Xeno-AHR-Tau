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
using AlienRace;

namespace AdeptusMechanicus.HarmonyInstance
{
    
    [HarmonyPatch(typeof(Thing), "Ingested")]
    public static class Thing_Ingested_KrootTraitAbsorbtion_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Thing __instance, Pawn ingester, float nutritionWanted)
        {
            if (__instance is Corpse corpse)
            {
                if (ingester.def == AdeptusThingDefOf.OG_Alien_Kroot)
                {
                    int num;
                    float result;
                    __instance.IngestedCalculateAmounts(ingester, nutritionWanted, out num, out result);
                    Rand.PushState();
                    if (Rand.Value < result)
                    {
                        if (ModsConfig.BiotechActive && corpse.InnerPawn.genes != null) DoIngestionOutcomeSpecialBiotech(ingester, corpse);
                        DoIngestionOutcomeSpecial(ingester, corpse);
                    }
                    Rand.PopState();
                }
            }
        }

        public static void DoIngestionOutcomeSpecialBiotech(Pawn pawn, Corpse corpse)
        {
            List<Gene> possibleEndoGenes = new List<Gene>();
            if (corpse.InnerPawn.genes == null)
            {
                return;
            }
            foreach (var item in corpse.InnerPawn.genes.Endogenes)
            {
                if (pawn.genes.HasEndogene(item.def))
                {
                    continue;
                }
                possibleEndoGenes.Add(item);
            }
            possibleEndoGenes.RemoveAll(x => RaceRestrictionSettings.CanHaveGene(x.def, pawn.def));
            List<Gene> possibleXenoGenes = new List<Gene>();
            foreach (var item in corpse.InnerPawn.genes.Xenogenes)
            {
                if (pawn.genes.HasXenogene(item.def))
                {
                    continue;
                }
                possibleXenoGenes.Add(item);    
            }
            possibleXenoGenes.RemoveAll(x => RaceRestrictionSettings.CanHaveGene(x.def, pawn.def));

            List<Gene> possibleGenes = new List<Gene>(possibleEndoGenes.ConcatIfNotNull(possibleXenoGenes));
            foreach (var item in pawn.genes.cachedGenes)
            {
                possibleGenes.RemoveAll(x=> x.def.ConflictsWith(item.def));
            }
            if (!possibleGenes.NullOrEmpty())
            {
                Rand.PushState();
                int genes = Rand.RangeInclusive(1, 3);
                for (int i = 0; i < genes; i++)
                {
                    Gene gene = possibleGenes.RandomElement();
                    pawn.genes.AddGene(gene.def, possibleXenoGenes.Contains(gene));
                    possibleXenoGenes.Remove(gene);
                }
                Rand.PopState();
            }
        }

        public static void DoIngestionOutcomeSpecial(Pawn pawn, Corpse corpse)
        {
            int maxTraits;
            List<Trait> possibleTraits = new List<Trait>();
            bool replace = false;
            if (MoreTraitSlotsUtil.TryGetMaxTraitSlots(out int max))
            {
                maxTraits = max;
            }
            else { maxTraits = 4; }
            int countTraits = pawn.story.traits.allTraits.Count;
            if (countTraits >= maxTraits)
            {
                replace = true;
            }
            //   Log.Message(string.Format("i have {0} of a max of {1} traits", countTraits, maxTraits));
            Trait replacedTrait = pawn.story.traits.allTraits.RandomElement();
            TraitDef replacedTraitDef = replacedTrait.def;
            if (corpse.InnerPawn.RaceProps.Humanlike)
            {
                if (countTraits >= maxTraits)
                {

                }
                possibleTraits = corpse.InnerPawn.story.traits.allTraits;
                // IList<Trait> list = new List<Trait>(corpse.InnerPawn.story.traits.allTraits);
                //pawn.story.traits.GetTrait(replacedTraitDef);
            }
            if (corpse.InnerPawn.RaceProps.FleshType.defName == "OG_Flesh_Ork")
            {
                possibleTraits.Add(new Trait(TraitDefOf.Tough));
                possibleTraits.Add(new Trait(TraitDefOf.Bloodlust));
            }
            if (corpse.InnerPawn.RaceProps.FleshType.defName == "OG_Flesh_Eldar")
            {
                Rand.PushState();
                possibleTraits.Add(new Trait(TraitDefOf.PsychicSensitivity, Rand.Bool ? 1 : 2));
                Rand.PopState();
            }
            if (!possibleTraits.NullOrEmpty())
            {
                Trait targetTrait = possibleTraits.RandomElement();
                TraitDef targetTraitDef = targetTrait.def;
                bool flag = false;
                if (pawn.story.traits.HasTrait(targetTraitDef) || targetTrait == replacedTrait)
                {
                    foreach (var t in corpse.InnerPawn.story.traits.allTraits)
                    {
                        if (!pawn.story.traits.HasTrait(targetTrait.def))
                        {
                            foreach (var ct in t.def.conflictingTraits)
                            {
                                if (pawn.story.traits.HasTrait(ct))
                                {
                                    replacedTrait = pawn.story.traits.GetTrait(ct);
                                    replacedTraitDef = ct;
                                    replace = true;
                                }
                            }
                            if (flag == false)
                            {
                                targetTrait = t;
                                targetTraitDef = targetTrait.def;
                                break;
                            }
                        }
                    }
                }
                if (!pawn.story.traits.HasTrait(targetTraitDef) && targetTrait != replacedTrait)
                {
                    if (countTraits < maxTraits)
                    {
                        MoteMaker.ThrowText(pawn.Position.ToVector3(), pawn.Map, "OGKroot_Genetic_Absorb_Add".Translate(pawn.LabelDefinite(), targetTrait.LabelCap, corpse.InnerPawn.LabelDefinite()), 6f);
                    }
                    else if (countTraits >= maxTraits || replace == true)
                    {
                        MoteMaker.ThrowText(pawn.Position.ToVector3(), pawn.Map, "OGKroot_Genetic_Absorb_Replace".Translate(pawn.LabelShortCap, targetTrait.LabelCap, corpse.InnerPawn.LabelDefinite(), replacedTrait.LabelCap), 6f);
                        pawn.story.traits.allTraits.Remove(replacedTrait);
                    }
                    pawn.story.traits.GainTrait(targetTrait);
                    return;
                }
            }
            /*
            Hediff hediff = HediffMaker.MakeHediff(this.hediffDef, pawn, null);
            float num;
            if (this.severity > 0f)
            {
                num = this.severity;
            }
            else
            {
                num = this.hediffDef.initialSeverity;
            }
            if (this.divideByBodySize)
            {
                num /= pawn.BodySize;
            }
            AddictionUtility.ModifyChemicalEffectForToleranceAndBodySize(pawn, this.toleranceChemical, ref num);
            hediff.Severity = num;
            pawn.health.AddHediff(hediff, null, null, null);
            */
        }
    }
}
    
