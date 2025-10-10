Shader "Custom/ScrollMask"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" }

        // First, write to the stencil buffer
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            ColorMask 0    // Don't actually render anything visible
        }
    }
}
