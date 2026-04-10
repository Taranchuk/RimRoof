using Verse;

namespace RimRoof
{
    public class Building_LogFrame : Building
    {
        public override void Print(SectionLayer layer)
        {
            if (RimRoofMod.settings.showLogFrames) base.Print(layer);
        }
    }
}
