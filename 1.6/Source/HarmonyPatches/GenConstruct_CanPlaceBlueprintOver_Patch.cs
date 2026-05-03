using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.CanPlaceBlueprintOver))]
    public static class GenConstruct_CanPlaceBlueprintOver_Patch
    {
        public static void Postfix(ref bool __result, BuildableDef newDef, ThingDef oldDef)
        {
            if (!(newDef is ThingDef thingDef)) return;
            var isRoof = typeof(Building_Roof).IsAssignableFrom(thingDef.thingClass);
            var isLogFrame = thingDef == DefsOf.RimRoof_LogFrame;
            if (!isRoof && !isLogFrame) return;
            var oldDefBuilt = oldDef.entityDefToBuild ?? oldDef;
            if (newDef == oldDefBuilt) { __result = false; return; }
            __result = true;
        }
    }
}
