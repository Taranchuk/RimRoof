using HarmonyLib;
using Verse;

namespace RimRoof
{
    public class RimRoofMod : Mod
    {
        public static RimRoofSettings settings;

        public RimRoofMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<RimRoofSettings>();
            new Harmony("Harvest.RimRoof").PatchAll();
        }
    }
}
