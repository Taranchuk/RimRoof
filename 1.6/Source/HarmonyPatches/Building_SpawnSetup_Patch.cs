using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(Building), nameof(Building.SpawnSetup))]
    public static class Building_SpawnSetup_Patch
    {
        public static void Postfix(Building __instance)
        {
            if (__instance.def.entityDefToBuild is ThingDef def && typeof(Building_Roof).IsAssignableFrom(def.thingClass))
            {
                Building_Roof.NotifyAdjacentRoofs(__instance.Map, __instance.Position, def);
            }
        }
    }
}
