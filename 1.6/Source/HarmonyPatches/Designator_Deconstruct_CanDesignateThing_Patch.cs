using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(Designator_Deconstruct), nameof(Designator_Deconstruct.CanDesignateThing))]
    public static class Designator_Deconstruct_CanDesignateThing_Patch
    {
        public static void Postfix(Thing t, ref AcceptanceReport __result)
        {
            if (!__result.Accepted || t is Building_Roof) return;
            var map = t.Map;
            if (map == null) return;
            var pos = t.Position;
            if (map.thingGrid.ThingAt<Blueprint_Build>(pos)?.def.entityDefToBuild is ThingDef def && typeof(Building_Roof).IsAssignableFrom(def.thingClass))
            {
                __result = "RimRoof_CannotDeconstructUnderRoofBlueprint".Translate();
                return;
            }
            if (map.thingGrid.ThingAt<Frame>(pos)?.def.entityDefToBuild is ThingDef frameDef && typeof(Building_Roof).IsAssignableFrom(frameDef.thingClass))
            {
                __result = "RimRoof_CannotDeconstructUnderRoofFrame".Translate();
            }
        }
    }
}
