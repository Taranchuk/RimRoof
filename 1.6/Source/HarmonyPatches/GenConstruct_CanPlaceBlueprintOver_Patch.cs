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
            if (!__result) return;
            if (newDef is ThingDef thingDef && typeof(Building_Roof).IsAssignableFrom(thingDef.thingClass))
            {
                BuildableDef oldDefBuilt = oldDef.entityDefToBuild ?? oldDef;
                if (newDef == oldDefBuilt)
                {
                    __result = false;
                }
            }
        }
    }
}
