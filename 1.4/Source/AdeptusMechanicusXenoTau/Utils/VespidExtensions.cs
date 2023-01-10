using Verse;

namespace AdeptusMechanicus.ExtensionMethods
{
    public static class VespidExtensions
    {
        public static bool isVespid(this Pawn pawn)
        {
            return pawn.def.isVespid();
        }
        public static bool isVespid(this Thing pawn)
        {
            return pawn.def.isVespid();
        }
        public static bool isVespid(this ThingDef def)
        {
            return def == AdeptusThingDefOf.OG_Alien_Vespid;
        }

    }

}
