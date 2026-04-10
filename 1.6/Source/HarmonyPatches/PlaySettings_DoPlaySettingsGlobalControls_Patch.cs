using HarmonyLib;
using RimWorld;
using Verse;

namespace RimRoof
{
    [HarmonyPatch(typeof(PlaySettings), nameof(PlaySettings.DoPlaySettingsGlobalControls))]
    public static class PlaySettings_DoPlaySettingsGlobalControls_Patch
    {
        public static void Postfix(WidgetRow row, bool worldView)
        {
            if (worldView) return;
            bool log = RimRoofMod.settings.showLogFrames;
            bool roof = RimRoofMod.settings.showInnerRoofs;

            row.ToggleableIcon(ref RimRoofMod.settings.showLogFrames, Startup.IconLogFrame, "RimRoof_ToggleLog".Translate());
            row.ToggleableIcon(ref RimRoofMod.settings.showInnerRoofs, Startup.IconInnerRoof, "RimRoof_ToggleRoof".Translate());

            if (log != RimRoofMod.settings.showLogFrames || roof != RimRoofMod.settings.showInnerRoofs)
                Find.CurrentMap?.mapDrawer.WholeMapChanged(MapMeshFlagDefOf.Things);
        }
    }
}
