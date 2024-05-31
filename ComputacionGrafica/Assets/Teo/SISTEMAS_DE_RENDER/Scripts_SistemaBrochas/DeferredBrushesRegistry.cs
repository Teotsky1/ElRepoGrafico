using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeferredBrushesRegistry
{
    //SE ENCARGA DEL REGISTRO Y LA ORGANIZACION DE LOS PLANOS A RENDERIZAR
    public static List<DeferredCanvas> canvases = new List<DeferredCanvas>();

    public static List<DeferredBrush> brushes = new List<DeferredBrush>();
    #warning Still need to implement brushes in regisry;


    public static void RegisterCanvas(DeferredCanvas canva)
    {
        if (canvases.Contains(canva)) return;
        canvases.Add(canva);
        canvases.RemoveAll(x => x == null);
    }

    public static void RegisterBrush(DeferredBrush brush)
    {
        if (brushes.Contains(brush)) return;
        brushes.Add(brush);

        brushes.RemoveAll(x => x == null);
    }

}
