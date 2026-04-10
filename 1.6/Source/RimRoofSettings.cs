using Verse;

namespace RimRoof
{
    public class RimRoofSettings : ModSettings
    {
        public bool showLogFrames = true;
        public bool showInnerRoofs = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref showLogFrames, "showLogFrames", true);
            Scribe_Values.Look(ref showInnerRoofs, "showInnerRoofs", true);
        }
    }
}
