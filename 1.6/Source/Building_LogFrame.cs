using Verse;

namespace RimRoof
{
    public class Building_LogFrame : Building
    {
        public bool ShouldBeVisible => RimRoofMod.settings.showLogFrames;

        public override void Print(SectionLayer layer)
        {
            if (ShouldBeVisible) base.Print(layer);
        }
    }
}
