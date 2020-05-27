using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AdeptusMechanicus
{
    [StaticConstructorOnStartup]
    public class CoprseModification
    {
        static CoprseModification()
        {
            //foreach Def in DefDataBase<ThingDef>.AllDefsForReading
            foreach (var Def in DefDatabase<ThingDef>.AllDefsListForReading)
            {
                if (Def.defName.StartsWith("Corpse_"))
                {
                    if (Def.IsCorpse)
                    {
                        Def.ingestible.outcomeDoers = new List<IngestionOutcomeDoer>();
                        Def.ingestible.outcomeDoers.Add(new AdeptusMechanicus.IngestionOutcomeDoer_HumanlikeCorpse()
                        {
                            chance = 0.25f
                        });
                    }
                    //Log.Message(string.Format("{0} has outcomeDoers {1}", Def.defName, Def.ingestible.outcomeDoers.Count));
                }
            }
        }
    }
}