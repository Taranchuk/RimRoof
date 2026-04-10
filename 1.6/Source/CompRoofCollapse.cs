using System;
using RimWorld;
using Verse;

namespace RimRoof
{
    public class CompProperties_RoofCollapse : CompProperties
    {
        public float totalCollapseChancePerDay;
        public float singleCollapseChancePerDay;
        public CompProperties_RoofCollapse() => compClass = typeof(CompRoofCollapse);
    }

    public class CompRoofCollapse : ThingComp
    {
        public override void CompTickRare()
        {
            if (parent.Map.thingGrid.ThingAt<Building_LogFrame>(parent.Position) != null) return;

            var p = (CompProperties_RoofCollapse)props;
            float totalChance = p.totalCollapseChancePerDay * GenTicks.TickRareInterval / GenDate.TicksPerDay;
            float singleChance = p.singleCollapseChancePerDay * GenTicks.TickRareInterval / GenDate.TicksPerDay;

            var outcomes = new (Action action, float weight)[]
            {
                (() => { RoofCollapserImmediate.DropRoofInCells(parent.Position, parent.Map); parent.Destroy(DestroyMode.KillFinalize); }, totalChance),
                (() => parent.Destroy(DestroyMode.Vanish), singleChance),
                (null, 1f - totalChance - singleChance)
            };

            outcomes.RandomElementByWeight(o => o.weight).action?.Invoke();
        }
    }
}
