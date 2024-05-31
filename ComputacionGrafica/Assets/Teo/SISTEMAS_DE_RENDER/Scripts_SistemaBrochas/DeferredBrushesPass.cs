using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class DeferredBrushesPass : ScriptableRenderPass //se encarga de la logica de render
{
    private RTHandle renderTargetColor;
    private RTHandle renderTargetDepth;


    private Lazy<Material> renderingMaterial;
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.width = 256;
        descriptor.height = 256;


        RenderingUtils.ReAllocateIfNeeded(ref renderTargetColor, descriptor, name: "_DeferredBrushes_Color");


        //Decirle a Unity que renderice en una textura que nosotros queramos
        ConfigureTarget(renderTargetColor);
        ConfigureClear(ClearFlag.All, Color.black);




    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        //Lista para decirle a unity que haga lo siguiente cuando pueda
        CommandBuffer cmd = CommandBufferPool.Get("Defferred Brushes");

        try
        {

        }
        catch 
        {

           
        }

        DeferredCanvas canva = DeferredBrushesRegistry.canvases[0];

        (Matrix4x4 view, Matrix4x4 projection) = canva.getRenderinMaterices();

        cmd.SetViewProjectionMatrices(view , projection);
        //utilizar todos los tipos de renderer de unity
        foreach (DeferredBrush brush in DeferredBrushesRegistry.brushes)
        {
            //Para todas las brochas creadas en la escena lo renderizo

            cmd.DrawRenderer(brush.Renderer ,renderingMaterial.Value );
        }
        cmd.SetViewProjectionMatrices(renderingData.cameraData.GetViewMatrix(), renderingData.cameraData.GetViewMatrix());


        cmd.SetGlobalTexture("TrailDisplacement", renderTargetColor);



        context.ExecuteCommandBuffer(cmd);
        
        CommandBufferPool.Release(cmd);
        

    }
}
