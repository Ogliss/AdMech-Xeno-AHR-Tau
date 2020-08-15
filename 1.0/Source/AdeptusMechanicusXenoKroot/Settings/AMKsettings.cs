using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using UnityEngine;
using Verse;
using RimWorld;
using AdeptusMechanicus.settings;

namespace AdeptusMechanicus.settings
{
    public class AMKsettings : ModSettings
    {
        public bool ShowKroot = false,
            AllowKrootTrait = false,
            AllowKrootMutations = true;
        
        public AMKsettings()
        {
            AMKsettings.Instance = this;
        }

        public static AMKsettings Instance;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.ShowKroot, "AMXB_AllowImperial", false);
            Scribe_Values.Look(ref this.AllowKrootTrait, "AMXB_AllowAdeptusAstartes", false && AMASettings.Instance.AllowImperialWeapons);
            Scribe_Values.Look(ref this.AllowKrootMutations, "AMXB_AllowAdeptusMechanicus", true && AMASettings.Instance.AllowMechanicusWeapons);
        }


    }
}