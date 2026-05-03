using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.BlocksConstruction))]
    public static class GenConstruct_BlocksConstruction_Patch
    {
        public static void Postfix(ref bool __result, Thing constructible, Thing t)
        {
            if (!__result) return;

            var buildDef = (constructible as Frame)?.def.entityDefToBuild ?? (constructible as Blueprint)?.def.entityDefToBuild;
            if (buildDef == null) return;

            var isRoof = typeof(Building_Roof).IsAssignableFrom((buildDef as ThingDef)?.thingClass);
            var isLogFrame = buildDef == DefsOf.RimRoof_LogFrame;

            if (isRoof || isLogFrame)
            {
                if (t.def.holdsRoof)
                {
                    __result = false;
                    return;
                }

                if (t.def.category == ThingCategory.Item || (t.def.category == ThingCategory.Building && !t.def.building.isEdifice))
                {
                    __result = false;
                }
            }
        }
    }
}
