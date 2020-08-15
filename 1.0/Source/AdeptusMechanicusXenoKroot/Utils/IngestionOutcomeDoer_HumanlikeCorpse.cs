using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace AdeptusMechanicus
{
    // Token: 0x02000275 RID: 629
    public class IngestionOutcomeDoer_HumanlikeCorpse : IngestionOutcomeDoer
    {
        public new void DoIngestionOutcome(Pawn pawn, Thing ingested)
        {
            
            if (Rand.Value < this.chance)
            {
                this.DoIngestionOutcomeSpecial(pawn, ingested);
            }
        }
        // Token: 0x06000AED RID: 2797 RVA: 0x00057114 File Offset: 0x00055514
        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            Corpse corpse = (Corpse)ingested;
            BodyDef Kroot = OGKrootDefOf.OG_Kroot;
            int maxTraits;
            if (pawn.RaceProps.body == Kroot && corpse.InnerPawn.RaceProps.Humanlike)
            {
                bool replace=false;
                if (MoreTraitSlotsUtil.TryGetMaxTraitSlots(out int max))
                {
                    maxTraits = max;
                }
                else { maxTraits = 4; }
                int countTraits = pawn.story.traits.allTraits.Count;
                if (countTraits>=maxTraits)
                {
                    replace = true;
                }
             //   Log.Message(string.Format("i have {0} of a max of {1} traits", countTraits, maxTraits));
                Trait replacedTrait = pawn.story.traits.allTraits.RandomElement();
                TraitDef replacedTraitDef = replacedTrait.def;
                if (countTraits>=maxTraits)
                {
                }

                Trait targetTrait = corpse.InnerPawn.story.traits.allTraits.RandomElement();
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
                if (!pawn.story.traits.HasTrait(targetTraitDef) && targetTrait!= replacedTrait)
                {
                    if (countTraits < maxTraits)
                    {
                        MoteMaker.ThrowText(pawn.Position.ToVector3(), pawn.Map, "OGKroot_Genetic_Absorb_Add".Translate(pawn.LabelDefinite(), targetTrait.LabelCap, corpse.InnerPawn.LabelDefinite()), 6f);
                    }
                    else if (countTraits >= maxTraits ||replace==true)
                    {
                        MoteMaker.ThrowText(pawn.Position.ToVector3(), pawn.Map, "OGKroot_Genetic_Absorb_Replace".Translate(pawn.LabelShortCap, targetTrait.LabelCap, corpse.InnerPawn.LabelDefinite(), replacedTrait.LabelCap), 6f);
                        pawn.story.traits.allTraits.Remove(replacedTrait);
                    }
                    pawn.story.traits.GainTrait(targetTrait);
                    return;
                }
                // IList<Trait> list = new List<Trait>(corpse.InnerPawn.story.traits.allTraits);
                //pawn.story.traits.GetTrait(replacedTraitDef);
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