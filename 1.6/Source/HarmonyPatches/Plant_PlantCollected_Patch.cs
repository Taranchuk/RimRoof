using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(Plant), nameof(Plant.PlantCollected))]
    public static class Plant_PlantCollected_Patch
    {
        public static void Prefix(Plant __instance, Pawn by)
        {
            if (__instance.def != DefsOf.Plant_Grass && __instance.def != DefsOf.Plant_TallGrass) return;
            var hay = ThingMaker.MakeThing(ThingDefOf.Hay);
            hay.stackCount = Rand.RangeInclusive(2, 4);
            GenPlace.TryPlaceThing(hay, __instance.Position, __instance.Map, ThingPlaceMode.Near);
        }
    }
}
