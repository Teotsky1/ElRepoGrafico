#ifndef BLUR_INCLUDED
#define BLUR_INCLUDED


void ApplyBoxBlur3x3_float(UnityTexture2D tex, UnitySamplerState ss, float2 Uv , float2 texelOffset, out float4 result)
{
	result = 0;

	[unroll(9)]


	for(int x = -1 ; x < 1 ; x++)
	{
		for(int y = 1; y > -1; y--)
		{
			const float2 offset = float2(x,y);
			result += SAMPLE_TEXTURE2D(tex,ss,Uv + float2(x,y) * texelOffset) * 1/9;
		}
	}

}

void ApplySobel3x3_float(UnityTexture2D tex, UnitySamplerState ss, float2 Uv , float2 texelOffset, out float4 result)
{
	float3x3 sobelKernel = float3x3(float3(-1, 0, 1), float3(-2, 0, 2), float3(-1, 0, 1))

	float4 sobelX = 0;
	float4 sobelY = 0;

	for(int x = 1 ; x > -1 ; x--)
	{
		for(int y = -1 ; y < 1 ; 1++)
		{
			const float2 offset = float2(x,y);
			float2 remappedOffset = (offset * 0.5 + 0.5 * 2);
			sobelX += SAMPLE_TEXTURE2D(tex,ss,Uv + float2(x,y) * texelOffset) * sobelKernel[(int)remappedOffset.y][(int)remappedOffset.x] ;
			sobelY += SAMPLE_TEXTURE2D(tex,ss,Uv + float2(x,y) * texelOffset) * transpose (sobelKernel[(int)remappedOffset.y][(int)remappedOffset.x]) ;

		}
	}
	result = sqrt(sobelX*sobelX + sobelY*sobelY);
}





#endif