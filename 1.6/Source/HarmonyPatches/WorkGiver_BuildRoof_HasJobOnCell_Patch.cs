using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HotSwappableAttribute : Attribute
    {
    }

    [HarmonyPatch]
    [HotSwappable]
    [HarmonyPatch(typeof(WorkGiver_BuildRoof), nameof(WorkGiver_BuildRoof.HasJobOnCell))]
    public static class WorkGiver_BuildRoof_HasJobOnCell_Patch
    {
        public static bool Prefix(ref bool __result, Pawn pawn, IntVec3 c)
        {
            var map = pawn.Map;
            if (map != null && c.InBounds(map) && map.terrainGrid.FoundationAt(c)?.IsSubstructure == true)
            {
                return true;
            }
            __result = false;
            return false;
        }
    }
}
