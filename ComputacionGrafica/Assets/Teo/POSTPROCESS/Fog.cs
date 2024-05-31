using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Fog : ScriptableRenderPass
{
    private RTHandle postProcessTukiTuki;

    private Material renderingMaterial;
    public Fog(Material RenderMTL)
    {
        this.renderingMaterial = RenderMTL;
    }



    // This method is called before executing the render pass.
    // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
    // When empty this render pass will render to the active camera render target.
    // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
    // The render pipeline will ensure target setup and clearing happens in a performant manner.


    ///SE LLAMA PRIMERO QUE EL EXECUTE
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {

        if (renderingMaterial == null) return;
        //Coge los datos de la camara y los almacena aquí
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

        descriptor.depthBufferBits = 0;

        RenderingUtils.ReAllocateIfNeeded( ref postProcessTukiTuki, descriptor);

    }

    // Here you can implement the rendering logic.
    // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
    // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
    // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {

        if (renderingMaterial == null) return;

        VolumeManager manager = VolumeManager.instance;
        //if (!manager.IsComponentActiveInMask<SomkeTukiTuki>(renderingData.cameraData.camera.cullingMask)) return;

        VolumeStack stack = manager.stack;

        SomkeTukiTuki fogdata = stack.GetComponent<SomkeTukiTuki>();

        renderingMaterial.SetColor("_Color", fogdata.color.value);

        renderingMaterial.SetVector("_FogParameter", new Vector4(fogdata.fogStart.value, fogdata.fogEnd.value, 0,0));

        CommandBuffer cmd = CommandBufferPool.Get("Valorant Smoke");

        RTHandle screenTexture = renderingData.cameraData.renderer.cameraColorTargetHandle;

        cmd.Blit(screenTexture, postProcessTukiTuki);
        cmd.Blit(postProcessTukiTuki, screenTexture, renderingMaterial, renderingMaterial.FindPass("Universal Forward"));

        context.ExecuteCommandBuffer(cmd);

        cmd.Release();


    }

    // Cleanup any allocated resources that were created during the execution of this render pass.
    public override void OnCameraCleanup(CommandBuffer cmd)
    {

    }
}
