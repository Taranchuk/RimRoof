using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(ThingSelectionUtility), nameof(ThingSelectionUtility.SelectableByMapClick))]
    public static class ThingSelectionUtility_SelectableByMapClick_Patch
    {
        public static void Postfix(Thing t, ref bool __result)
        {
            if (!__result) return;
            if (t is Building_Roof roof && !roof.ShouldBeVisible)
            {
                __result = false;
            }
            else if (t is Building_LogFrame frame && !frame.ShouldBeVisible)
            {
                __result = false;
            }
        }
    }
}
