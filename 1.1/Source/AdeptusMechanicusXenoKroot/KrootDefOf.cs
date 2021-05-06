﻿using System;
using Verse;
using RimWorld;

namespace AdeptusMechanicus
{
    // PLAYABLE KROOT
    [DefOf]
    public static class KrootDefOf
    {
        static KrootDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(KrootDefOf));
        }

        // Kroot ThingDefs
        public static AlienRace.ThingDef_AlienRace OG_Alien_Kroot;
        public static AlienRace.ThingDef_AlienRace OG_Alien_Tau;
        public static AlienRace.ThingDef_AlienRace OG_Alien_Vespid;

        public static ThingDef OG_FilthBlood_Kroot;
        //    public static ThingDef OG_Alien_Vespid;

        public static ThingDef OG_Kroothound;
        public static ThingDef OG_KrootOx;
        public static ThingDef OG_Knarloc;

        public static ThingDef OG_Kroothound_Kindred;
        public static ThingDef OG_KrootOx_Kindred;
        public static ThingDef OG_Knarloc_Kindred;

        // Kroot BodyDefs
        public static BodyDef OG_Kroot_Body;
        public static BodyDef OG_Kroothound_Body;
        public static BodyDef OG_KrootOx_Body;
        public static BodyDef OG_Knarloc_Body;
        //    public static BodyDef OG_Vespid;

        // Kroot FactionDefs
        public static FactionDef OG_Kroot_Faction;
        public static FactionDef OG_Kroot_PlayerColony;
        public static FactionDef OG_Kroot_PlayerTribe;

        // Kroot IncidentDefs
        //    public static IncidentDef ;

        // Kroot DamageDefs
        //    public static DamageDef ;

        // Kroot HediffDefs
        //    public static HediffDef ;

        // Kroot MentalStateDefs
        //    public static MentalStateDef ;

        // Kroot GameConditionDefs
        //    public static GameConditionDef ;

        // Kroot PawnKindDefs
        //    public static PawnKindDef ;

    }
}