using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SomkeTukiTuki : VolumeComponent    
{
    public ColorParameter color =  new ColorParameter(Color.white);

    public FloatParameter fogStart = new FloatParameter(3f);

    public FloatParameter fogEnd = new FloatParameter(6f);


}
