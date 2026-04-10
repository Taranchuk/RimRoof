using HarmonyLib;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(Map), nameof(Map.FinalizeInit))]
    public static class Map_FinalizeInit_Patch
    {
        public static void Postfix(Map __instance)
        {
            foreach (var thing in __instance.listerThings.AllThings)
                if (thing is Building_Roof roof) roof.UpdateShape();
        }
    }
}
