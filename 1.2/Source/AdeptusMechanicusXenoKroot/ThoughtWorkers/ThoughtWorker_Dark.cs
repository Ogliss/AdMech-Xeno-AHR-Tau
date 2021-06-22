using AdeptusMechanicus.ExtensionMethods;
using RimWorld;
using System;
using Verse;

namespace AdeptusMechanicus
{
    // Token: 0x02000219 RID: 537
    public class ThoughtWorker_Dark_Kroot : ThoughtWorker
    {
        // Token: 0x06000A26 RID: 2598 RVA: 0x0004FC2B File Offset: 0x0004E02B
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            return p.Awake() && p.needs.mood.recentMemory.TicksSinceLastLight > 2400;
        }
    }
}
