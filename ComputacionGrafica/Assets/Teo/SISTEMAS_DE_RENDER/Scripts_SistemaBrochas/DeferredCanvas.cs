using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeferredCanvas : MonoBehaviour
{
    [SerializeField] private Vector3 boundPadding;

    public void Awake()
    {
        DeferredBrushesRegistry.RegisterCanvas(this);
    }

    public (Matrix4x4, Matrix4x4) getRenderinMaterices()
    {
        Matrix4x4 view = Matrix4x4.TRS(transform.position + transform.up * 2f, //Mover el plano 2 metros hacia arriba

            transform.rotation * Quaternion.AngleAxis(90f, transform.right),//Para mover las rotaciones como quiero

            Vector3.one).inverse;//Vector one por la escala 

        Vector3 scaleBounds =
               Vector3.Scale(GetComponent<MeshFilter>().sharedMesh.bounds.extents, transform.lossyScale) + boundPadding;



        Matrix4x4 Orthoprojection = Matrix4x4.Ortho(-scaleBounds.x, scaleBounds.x, -scaleBounds.z, scaleBounds.z, 0.01f, scaleBounds.y * 2);    //Matriz ortografica, genera el frootome de la camara

           


            return (view , Orthoprojection);
    }


    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var rend = GetComponent<MeshRenderer>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rend.bounds.center, rend.bounds.size);
        var filter = GetComponent<MeshFilter>();
        Matrix4x4 originalMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(filter.sharedMesh.bounds.center, filter.sharedMesh.bounds.size + boundPadding);
        Gizmos.matrix = originalMatrix;
    }
    #endif
}
