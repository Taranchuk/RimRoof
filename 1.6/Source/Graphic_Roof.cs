using UnityEngine;
using Verse;

namespace RimRoof
{
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

        public override void DrawWorker(Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation)
        {
            if (thing is Building_Roof roof && !roof.ShouldBeVisible) return;
            var graphic = thing != null ? GetSub(thing) : subGraphics[0];
            graphic.DrawWorker(loc, thing != null ? thing.Rotation : rot, thingDef, thing, extraRotation);
        }

        public override void Print(SectionLayer layer, Thing thing, float extraRotation)
        {
            if (thing is Building_Roof roof && !roof.ShouldBeVisible) return;
            GetSub(thing).Print(layer, thing, extraRotation);
        }

        private Graphic GetSub(Thing t) => (t is Building_Roof b) ? subGraphics[(int)b.CurShape] : subGraphics[0];
    }
}
