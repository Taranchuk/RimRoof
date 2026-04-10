using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(Building), nameof(Building.DeSpawn))]
    public static class Building_DeSpawn_Patch
    {
        public static void Prefix(Building __instance, out (Map map, IntVec3 pos, ThingDef entityDef) __state)
        {
            __state = (__instance.Map, __instance.Position, __instance.def.entityDefToBuild as ThingDef);
        }

        public static void Postfix((Map map, IntVec3 pos, ThingDef entityDef) __state)
        {
            if (__state.entityDef != null && typeof(Building_Roof).IsAssignableFrom(__state.entityDef.thingClass))
            {
                Building_Roof.NotifyAdjacentRoofs(__state.map, __state.pos, __state.entityDef);
            }
        }
    }
}
