using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace AdeptusMechanicus
{
	// Token: 0x02000E02 RID: 3586
	public class NecronCorpse : Corpse
    {
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x00253B04 File Offset: 0x00251F04
		private bool ShouldVanish
		{
			get
			{
				return this.InnerPawn.RaceProps.Animal && this.vanishAfterTimestamp > 0 && this.Age >= this.vanishAfterTimestamp && base.Spawned && this.GetRoom(RegionType.Set_Passable) != null && this.GetRoom(RegionType.Set_Passable).TouchesMapEdge && !base.Map.roofGrid.Roofed(base.Position);
			}
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x00253C56 File Offset: 0x00252056
		public override void PostMake()
		{
			base.PostMake();
			this.timeOfDeath = Find.TickManager.TicksGame;
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00253C6E File Offset: 0x0025206E
		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			if (this.Bugged)
			{
				Log.Error(this + " spawned in bugged state.", false);
				return;
			}
			base.SpawnSetup(map, respawningAfterLoad);
			this.InnerPawn.Rotation = Rot4.South;
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x00253CC8 File Offset: 0x002520C8
		public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
		{
			Pawn pawn = null;
			if (!this.Bugged)
			{
				pawn = this.InnerPawn;
				this.innerContainer.Clear();
			}
			base.Destroy(mode);
			if (pawn != null)
			{
				NecronCorpse.PostCorpseDestroy(pawn);
			}
		}
		// Token: 0x06005073 RID: 20595 RVA: 0x00253D70 File Offset: 0x00252170
		public override void TickRare()
		{
			base.TickRare();
			if (base.Destroyed)
			{
				return;
			}
			if (this.Bugged)
			{
				Log.Error(this + " has null innerPawn. Destroying.", false);
				this.Destroy(DestroyMode.Vanish);
				return;
			}
			this.InnerPawn.TickRare();
            int resroll = Rand.Range(1, 6);
            if (resroll==6)
            {

            }
			if (this.vanishAfterTimestamp < 0 || this.GetRotStage() != RotStage.Dessicated)
			{
				this.vanishAfterTimestamp = this.Age + 5000;
			}
			if (this.ShouldVanish)
			{
                Resurrect(this.InnerPawn);
			}
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x00253F64 File Offset: 0x00252364
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.timeOfDeath, "timeOfDeath", 0, false);
			Scribe_Values.Look<int>(ref this.vanishAfterTimestamp, "vanishAfterTimestamp", 0, false);
			Scribe_Values.Look<bool>(ref this.everBuriedInSarcophagus, "everBuriedInSarcophagus", false, false);
			Scribe_Deep.Look<BillStack>(ref this.operationsBillStack, "operationsBillStack", new object[]
			{
				this
			});
			Scribe_Deep.Look<ThingOwner<Pawn>>(ref this.innerContainer, "innerContainer", new object[]
			{
				this
			});
		}

        public static void Resurrect(Pawn pawn)
        {
            if (!pawn.Dead)
            {
                Log.Error("Tried to resurrect a pawn who is not dead: " + pawn.ToStringSafe<Pawn>(), false);
                return;
            }
            if (pawn.Discarded)
            {
                Log.Error("Tried to resurrect a discarded pawn: " + pawn.ToStringSafe<Pawn>(), false);
                return;
            }
            Corpse corpse = pawn.Corpse;
            bool flag = false;
            IntVec3 loc = IntVec3.Invalid;
            Map map = null;
            if (corpse != null)
            {
                flag = corpse.Spawned;
                loc = corpse.Position;
                map = corpse.Map;
                corpse.InnerPawn = null;
                corpse.Destroy(DestroyMode.Vanish);
            }
            if (flag && pawn.IsWorldPawn())
            {
                Find.WorldPawns.RemovePawn(pawn);
            }
            pawn.ForceSetStateToUnspawned();
            PawnComponentsUtility.CreateInitialComponents(pawn);
            pawn.health.Notify_Resurrected();
            if (pawn.Faction != null && pawn.Faction.IsPlayer)
            {
                if (pawn.workSettings != null)
                {
                    pawn.workSettings.EnableAndInitialize();
                }
                Find.StoryWatcher.watcherPopAdaptation.Notify_PawnEvent(pawn, PopAdaptationEvent.GainedColonist);
            }
            if (flag)
            {
                GenSpawn.Spawn(pawn, loc, map, WipeMode.Vanish);
                for (int i = 0; i < 10; i++)
                {
                    MoteMaker.ThrowAirPuffUp(pawn.DrawPos, map);
                }
                if (pawn.Faction != null && pawn.Faction != Faction.OfPlayer && pawn.HostileTo(Faction.OfPlayer))
                {
                    LordMaker.MakeNewLord(pawn.Faction, new LordJob_AssaultColony(pawn.Faction, true, true, false, false, true), pawn.Map, Gen.YieldSingle<Pawn>(pawn));
                }
                if (pawn.apparel != null)
                {
                    List<Apparel> wornApparel = pawn.apparel.WornApparel;
                    for (int j = 0; j < wornApparel.Count; j++)
                    {
                        wornApparel[j].Notify_PawnResurrected();
                    }
                }
            }
            PawnDiedOrDownedThoughtsUtility.RemoveDiedThoughts(pawn);
        }
        // Token: 0x040035E9 RID: 13801
        private ThingOwner<Pawn> innerContainer;

        private bool shouldres = false;
		// Token: 0x040035EA RID: 13802
		// public int timeOfDeath = -1;

		// Token: 0x040035EB RID: 13803
		private int vanishAfterTimestamp = -1;

		// Token: 0x040035EC RID: 13804
		private BillStack operationsBillStack;

		// Token: 0x040035EE RID: 13806
		private const int VanishAfterTicksSinceDessicated = 6000000;
	}
}
