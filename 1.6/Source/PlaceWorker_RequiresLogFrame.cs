using RimWorld;
using Verse;

namespace RimRoof
{
    public class PlaceWorker_RequiresLogFrame : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef def, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            if (map.thingGrid.ThingAt<Building_LogFrame>(loc) != null) return true;
            return "RimRoof_NeedLogFrame".Translate();
        }
    }
}
