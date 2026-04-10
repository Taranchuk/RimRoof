using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimRoof
{
    public enum RoofShape { Center = 0, Side = 1, OuterCorner = 2, InnerCorner = 3 }

    public class Building_Roof : Building
    {
        private RoofShape curShape;

        public RoofShape CurShape => curShape;
        public bool ShouldBeVisible => curShape != RoofShape.Center || RimRoofMod.settings.showInnerRoofs;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            map.roofGrid.SetRoof(Position, RoofDefOf.RoofConstructed);
            UpdateShape();
            NotifyNeighbors(map);
        }

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            var map = Map;
            base.DeSpawn(mode);
            map.roofGrid.SetRoof(Position, null);
            NotifyNeighbors(map);
        }

        private void NotifyNeighbors(Map map)
        {
            for (int i = 0; i < 8; i++)
            {
                var c = Position + GenAdj.AdjacentCells[i];
                if (c.InBounds(map)) (map.thingGrid.ThingAt(c, def) as Building_Roof)?.UpdateShape();
            }
        }

        public void UpdateShape()
        {
            bool n = IsS(IntVec3.North), e = IsS(IntVec3.East), s = IsS(IntVec3.South), w = IsS(IntVec3.West);
            bool hasSupport = HasWallSupport();
            if (n && e && s && w)
            {
                if (!IsS(new IntVec3(1, 0, 1)))
                {
                    curShape = hasSupport ? RoofShape.InnerCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.North : Rot4.North;
                }
                else if (!IsS(new IntVec3(1, 0, -1)))
                {
                    curShape = hasSupport ? RoofShape.InnerCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.East : Rot4.North;
                }
                else if (!IsS(new IntVec3(-1, 0, -1)))
                {
                    curShape = hasSupport ? RoofShape.InnerCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.South : Rot4.North;
                }
                else if (!IsS(new IntVec3(-1, 0, 1)))
                {
                    curShape = hasSupport ? RoofShape.InnerCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.West : Rot4.North;
                }
                else
                {
                    curShape = RoofShape.Center;
                    Rotation = Rot4.North;
                }
            }
            else
            {
                if (!n && !w)
                {
                    curShape = hasSupport ? RoofShape.OuterCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.North : Rot4.North;
                }
                else if (!n && !e)
                {
                    curShape = hasSupport ? RoofShape.OuterCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.East : Rot4.North;
                }
                else if (!s && !e)
                {
                    curShape = hasSupport ? RoofShape.OuterCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.South : Rot4.North;
                }
                else if (!s && !w)
                {
                    curShape = hasSupport ? RoofShape.OuterCorner : RoofShape.Center;
                    Rotation = hasSupport ? Rot4.West : Rot4.North;
                }
                else if (!n) { curShape = RoofShape.Side; Rotation = Rot4.North; }
                else if (!e) { curShape = RoofShape.Side; Rotation = Rot4.East; }
                else if (!s) { curShape = RoofShape.Side; Rotation = Rot4.South; }
                else if (!w) { curShape = RoofShape.Side; Rotation = Rot4.West; }
                else
                {
                    curShape = RoofShape.Center;
                    Rotation = Rot4.North;
                }
            }
            Map.mapDrawer.MapMeshDirty(Position, MapMeshFlagDefOf.Things);
        }

        private bool IsS(IntVec3 o)
        {
            var c = Position + o;
            if (Map.thingGrid.ThingAt(c, def) != null) return true;
            var frame = Map.thingGrid.ThingAt<Frame>(c);
            return frame != null && frame.def.entityDefToBuild == def;
        }

        private bool HasWallSupport()
        {
            return Position.GetEdifice(Map)?.def.IsWall ?? false;
        }
    
            public override string GetInspectString()
            {
                return $"Shape: {curShape}\nRotation: {Rotation.ToStringHuman()}";
            }
    
            public override IEnumerable<Gizmo> GetGizmos()
            {
                foreach (var g in base.GetGizmos())
                {
                    yield return g;
                }
    
                if (DebugSettings.ShowDevGizmos)
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Set Shape: " + curShape,
                        action = () =>
                        {
                            curShape = (RoofShape)(((int)curShape + 1) % 4);
                            Map.mapDrawer.MapMeshDirty(Position, MapMeshFlagDefOf.Things);
                        }
                    };
    
                    yield return new Command_Action
                    {
                        defaultLabel = "Set Rotation: " + Rotation,
                        action = () =>
                        {
                            Rotation = new Rot4((Rotation.AsInt + 1) % 4);
                            Map.mapDrawer.MapMeshDirty(Position, MapMeshFlagDefOf.Things);
                        }
                    };
                }
            }
    
        public static void NotifyAdjacentRoofs(Map map, IntVec3 pos, ThingDef entityDef)
        {
            for (int i = 0; i < 8; i++)
            {
                var c = pos + GenAdj.AdjacentCells[i];
                if (c.InBounds(map)) (map.thingGrid.ThingAt(c, entityDef) as Building_Roof)?.UpdateShape();
            }
        }
    }
}
