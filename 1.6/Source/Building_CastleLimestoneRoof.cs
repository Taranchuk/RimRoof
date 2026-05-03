using RimWorld;
using Verse;

namespace RimRoof
{
    public class Building_CastleLimestoneRoof : Building_Roof
    {
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            map.roofGrid.SetRoof(Position, RoofDefOf.RoofRockThick);
        }

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            var map = Map;
            base.DeSpawn(mode);
            map.roofGrid.SetRoof(Position, null);
        }
    }
}
