using RimWorld;
using UnityEngine;
using Verse;

namespace RimRoof
{
    [HotSwappable]
    public class Graphic_Roof : Graphic
    {
        private Graphic[] subGraphics = new Graphic[4];

        public override void Init(GraphicRequest req)
        {
            path = req.path;
            color = req.color;
            drawSize = req.drawSize;
            subGraphics[0] = GraphicDatabase.Get<Graphic_Single>(req.path, req.shader, drawSize, color);
            subGraphics[1] = GraphicDatabase.Get<Graphic_Single>(req.path + "_side", req.shader, drawSize, color);
            subGraphics[2] = GraphicDatabase.Get<Graphic_Single>(req.path + "_outer_corner", req.shader, drawSize, color);
            subGraphics[3] = GraphicDatabase.Get<Graphic_Single>(req.path + "_inner_corner", req.shader, drawSize, color);
        }

        public Vector3 RoofDrawOffset => new Vector3(0f, 0f, 0.2f);

        public override void DrawWorker(Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation)
        {
            if (thing is Building_Roof roof && !roof.ShouldBeVisible) return;
            loc += RoofDrawOffset;
            var graphic = thing != null ? GetSub(thing) : subGraphics[0];
            graphic.DrawWorker(loc, thing != null ? thing.Rotation : rot, thingDef, thing, extraRotation);
        }

        public override void Print(SectionLayer layer, Thing thing, float extraRotation)
        {
            if (thing is Building_Roof roof && !roof.ShouldBeVisible) return;
            var graphic = GetSub(thing);
            var rot = thing.Rotation;
            var angle = rot.AsAngle + extraRotation;
            var pos = thing.TrueCenter() + RoofDrawOffset;
            Printer_Plane.PrintPlane(layer, pos, drawSize, graphic.MatAt(rot, thing), angle);
        }

        private Graphic GetSub(Thing t) => (t is Building_Roof b) ? subGraphics[(int)b.CurShape] : subGraphics[0];
    }
}
