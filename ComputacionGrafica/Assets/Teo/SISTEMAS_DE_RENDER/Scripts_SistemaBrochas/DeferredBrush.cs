using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeferredBrush : MonoBehaviour
{
    public Renderer Renderer => GetComponent<Renderer>();
    private void Awake()
    {
        DeferredBrushesRegistry.RegisterBrush(this);
    }
}
