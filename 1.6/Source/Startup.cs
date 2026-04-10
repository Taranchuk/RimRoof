using UnityEngine;
using Verse;

namespace RimRoof
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        public static readonly Texture2D IconLogFrame = ContentFinder<Texture2D>.Get("Log_Frame");
        public static readonly Texture2D IconInnerRoof = ContentFinder<Texture2D>.Get("Haygrass_Roof");
    }
}
