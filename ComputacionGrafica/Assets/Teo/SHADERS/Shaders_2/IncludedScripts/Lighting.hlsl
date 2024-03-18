#ifndef LINGHTING_INCLUDED

#define LINGHTING_INCLUDED

//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

void GetMainLight_float(out float3 Direction, out float3 Color)
{
    Direction = float3(1,1,-1);
    Color = 1;

    #ifdef UNIVERSAL_LIGHTING_INCLUDED
    const Light luz = GetMainLight();
    Direction = luz.direction;
    Color = luz.color;
    #endif
}





/*
void Add_float(float x, float y, out float Result)
{
    Result = x + y;
}
*/


#endif