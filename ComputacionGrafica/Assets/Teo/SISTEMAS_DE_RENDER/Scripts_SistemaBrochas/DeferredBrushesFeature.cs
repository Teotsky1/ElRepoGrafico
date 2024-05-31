using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class DeferredBrushesFeature : ScriptableRendererFeature
{
    DeferredBrushesPass pass;
    //Render diferidos, render por pases, es crear un pase especifico para algo que queramos hacer especificamente 
    public override void Create()
    {
        pass = new DeferredBrushesPass();

        pass.renderPassEvent = RenderPassEvent.AfterRenderingPrePasses;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        throw new System.NotImplementedException();
    }


}
