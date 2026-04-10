using RimWorld;
using Verse;

namespace RimRoof
{
    public class PlaceWorker_Roof : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef def, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            if (!RoofCollapseUtility.WithinRangeOfRoofHolder(loc, map))
            {
                return "TooFarFromSupport".Translate();
            }
            return true;
        }
    }
}
