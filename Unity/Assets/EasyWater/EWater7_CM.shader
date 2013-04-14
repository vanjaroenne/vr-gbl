Shader "EasyWater7_CUBEMAP" // name of this shader 
{// Everything defined inside property can be accessed witihin the Inspector
	Properties 
	{
_Color("_Color", Color) = (1,1,1,1)
_Texture1("_Texture1", 2D) = "black" {}
_BumpMap1("_BumpMap1", 2D) = "black" {}
_Texture2("_Texture2", 2D) = "black" {}
_BumpMap2("_BumpMap2", 2D) = "black" {}

// creating 4 floats for the speed at which the texture and bump-maps are going to move. 
_MainTexSpeed("_MainTexSpeed", Float) = 0
_Bump1Speed("_Bump1Speed", Float) = 0
_Texture2Speed("_Texture2Speed", Float) = 0
_Bump2Speed("_Bump2Speed", Float) = 0

// create distortionmap (Related to reflections, it changes the waves coming from the bump map), its speed and power. 
_DistortionMap("_DistortionMap", 2D) = "black" {}
_DistortionSpeed("_DistortionSpeed", Float) = 0
_DistortionPower("_DistortionPower", Range(0,0.02) ) = 0


_Specular("_Specular", Range(0,7) ) = 1
_Gloss("_Gloss", Range(0.3,2) ) = 0.3
_Opacity("_Opacity", Range(-0.1,1.0) ) = 0
_Reflection("_Reflection", Cube) = "black" {}
_ReflectPower("_ReflectPower", Range(0,1) ) = 0

	}
	
	SubShader // Each shader in Unity consists of a list of subshaders. When Unity has to display a mesh, it will find the shader to use, 
	//and pick the first subshader that runs on the user's graphics card
	{
	
	


		Tags // Tags are written inside subshaders. They are used to determine rendering order and other parameters of a subshader.
	//Queue is used in order to determine in which order the objects are drawn.  Transparent shaders make sure they are drawn after all opaque objects and so on.
		{
		"Queue"="Transparent"
		"IgnoreProjector"="False"
		"RenderType"="Overlay"

		}

		// Culling controls which sides of polygons should not be drawn (culled)
		Cull Back // Don't render polygons facing from the viewer
		ZWrite On // Pixels from this objects are written in the depth buffer 
		ZTest LEqual // draw objects in from or at the distance as existing objects; hide objects behind them
		ColorMask RGBA
		Blend SrcAlpha OneMinusSrcAlpha // Aplha blending 
		Fog{
		}
		
	
		
		
		// Our Shader runs two functions: One function that will calculate everything on the surface: Textures, reflections... called "Surface surf"
		// And another function, called "BlinnPhongEditor" that will things everything related to light. 
		// As the surface surf is mentioned first, it will run first. 
		// Target 3.0 must be the minimum version, since some of the functions will not run correctly in version 2.0 
	
				CGPROGRAM
		#pragma surface surf BlinnPhongEditor 
		#pragma target 3.0
		
		// float: 32 bit 
		// half: medium precision floating point, 16 bit range of -60000 to +60000 and 3.3 decimal digits of precision. used for colors and unit lentgh vectors  
		// fixed:  low precision fixed point. Generally 11 bits, with a range of -2.0 to +2.0 and 1/256th precision.
		
		// in order to access the materials declared in property, we need to declare a HLSL variable with the same name and a matching tupe.
		fixed4 _Color;  //low precision type is enough for colors
		uniform sampler2D _Texture1; // Texture properties map to sampler2D variables for regular (2D) textures
		uniform sampler2D _BumpMap1;
		uniform sampler2D _Texture2;
		uniform sampler2D _BumpMap2;
		half _MainTexSpeed;
		half _Bump1Speed;
		half _Texture2Speed;
		half _Bump2Speed;
		uniform sampler2D _DistortionMap;
		half _DistortionSpeed;
		half _DistortionPower;
		fixed _Specular;
		fixed _Gloss;
		float _Opacity;
		uniform samplerCUBE _Reflection; // Cubemaps map to samplerCUBE. A Cubemap Texture is a collection of six separate square Textures, 
										//that are put onto the faces of an imaginary cube. The Reflective built-in shaders in Unity use Cubemaps to display reflection.
		float _ReflectPower;


// SurfaceOutput is a Surface Shader that interacts with light. 
//You define a "surface function" that takes any UVs or data you need as input,
//and fills in output structure SurfaceOutput. SurfaceOutput basically describes properties 
//of the surface (it's albedo color, normal, emission, specularity etc.). 

		struct EditorSurfaceOutput {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Gloss;
			half Specular;
			half Alpha;
			half4 Custom;
		};
			
			
			
			
			
			// The Inline functions below tell how the surface should interact with the light in the scene.
			
		inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
		{
			half3 spec = light.a * s.Gloss;
			half4 c;
			c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
			c.a = s.Alpha;
			return c;
		}
		
		
		// The function below will be called first, it returns "LightingBlinnPhongEditor_PrePas" which has been defined above. 

		inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 h = normalize (lightDir + viewDir);
			
			half diff = max (0, dot ( lightDir, s.Normal )); // 
			
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular*128.0);
			
			half4 res;
			res.rgb = _LightColor0.rgb * diff;
			res.w = spec * Luminance (_LightColor0.rgb);
			res *= atten * 2.0;

			return LightingBlinnPhongEditor_PrePass( s, res );
		}
		
		// uv is the surface (textures)
		struct Input {
			float3 viewDir;  // /gives the view direction as a float3, so you can access e.g. IN.viewDir.x
							// viewDir will contain view direction, for computing Parallax effects, rim lighting etc.
							// As we can see viewDir is set as a float3 since we in world space only work with x,y and z. 
			float2 uv_DistortionMap;
			float2 uv_Texture1;
			float2 uv_Texture2;
			float2 uv_BumpMap1;
			float2 uv_BumpMap2;
		};



// The surface function is the function that calculates everything laid on top of the plane/surface such as reflection, textures etc. 
		void surf (Input IN, inout EditorSurfaceOutput o) {
			o.Normal = float3(0.0,0.0,1.0);
			o.Alpha = 1.0;
			o.Albedo = 0.0;
			o.Emission = 0.0;
			o.Gloss = 0.0;
			o.Specular = 0.0;
			o.Custom = 0.0;
			
			 
			// Here we now get the ViewDirection in a float4, as we in shaders work with 4 values (because of the transformation matrix). The last number is just put to 0.
			float4 ViewDirection=float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,0 ); 	
					
			
			
			// Animate distortionMap 
			float DistortSpeed=_DistortionSpeed * _Time;
			float2 DistortUV=(IN.uv_DistortionMap.xy) + DistortSpeed; // Moves texture with the amount of speed we have calculated. 
																	  //We add DistortionSpeed to the X and Y value of distortionmap
																	   
			// Create Normal for DistorionMap 
			// We take the normals from UV distortionmap; we put a 1.0 at the end so we have 4 values. 
			
			float4 DistortNormal = float4(UnpackNormal( tex2D(_DistortionMap,DistortUV)).xyz, 1.0 ); 
			// Multiply Tex2DNormal effect by DistortionPower
			float4 FinalDistortion = DistortNormal * _DistortionPower;// Now that we have fund the normal, we multiply it with the distortion power
			
			
			
			
			
			
			// Animate MainTex
			float MainTexPos=_Time * _MainTexSpeed; // The mainTexposition now equals time multiplied with the speed 
			float2 MainTexUV=(IN.uv_Texture1.xy) + MainTexPos; // We add the X and Y position from texture 1 to maintexpos
			
			// Apply Distorion in MainTex
			// Comparing MainTexUV+FinalDistortion with Texture 1 (comparing a position with a picture in order to make a new picture)
			float4 DistortedMainTex=tex2D(_Texture1,MainTexUV + FinalDistortion); 
			
			// Animate Texture2
			float Texture2Pos=_Time * _Texture2Speed;
			float2 Tex2UV=(IN.uv_Texture2.xy) + Texture2Pos;
			
			// Apply Distorion in Texture2
			float4 DistortedTexture2=tex2D(_Texture2,Tex2UV + FinalDistortion); 
			
			// Merge MainTex and Texture2
			float4 TextureMix=DistortedMainTex * DistortedTexture2;
			
			// Add TextureMix with Distortion					
			TextureMix.xy =  TextureMix.xy   + FinalDistortion.xy; 
			
			
			
			// Merge Textures, Texture and Color
			float4 FinalDiffuse = _Color * TextureMix; 			
			
			
			// Animate BumpMap1
			float BumpMap1Pos=_Time * _Bump1Speed;
			float2 Bump1UV=(IN.uv_BumpMap1.xy) + BumpMap1Pos;
			
			// Apply Distortion to BumpMap
			half4 DistortedBumpMap1=tex2D(_BumpMap1,Bump1UV + FinalDistortion);
			
			// Animate BumpMap2
			half BumpMap2Pos=_Time * _Bump2Speed;
			half2 Bump2UV=(IN.uv_BumpMap2.xy) + BumpMap2Pos;
			
			// Apply Distortion to BumpMap2			
			fixed4 DistortedBumpMap2=tex2D(_BumpMap2,Bump2UV + FinalDistortion);
			
			// Get Average from BumpMap1 and BumpMap2
			 // In order to get the wave.effect, we get the average from bumpmap1 and bumpmap2 
			fixed4 AvgBump= (DistortedBumpMap1 + DistortedBumpMap2) / 2;
			
			// Unpack Normals
			fixed4 UnpackNormal1=float4(UnpackNormal(AvgBump).xyz, 1.0);
			
			
			// Apply DistorionMap in Reflection's UV	
			// (Calculation in order to make a 4x4 matrix) so we can put it in a cube-texture				
			float4 viewInvert=float4(float4( ViewDirection.x, ViewDirection.x, ViewDirection.x, ViewDirection.x).x, float4( ViewDirection.z, ViewDirection.z, ViewDirection.z, ViewDirection.z).y, float4( ViewDirection.y, ViewDirection.y, ViewDirection.y, ViewDirection.y).z, 0);
			
			
		
			
			// We make a new cube where we create it by taking the coordinaes from the water.surface (viewInvert*AvgBump) 
			// in order to see which color the water should have in relation to the position of the cube above the water. 
			 					
			float4 TexCUBE0=texCUBE(_Reflection,viewInvert * AvgBump); 
			
			// Get Fresnel from viewDirection angle.
			// Fresnel is used in order to calculated the reflection and refraction of the watersurface. 
			// here we find the angle between the view vector and the object's surface. IN.viewDir is the 
			//cameras rotations (where it points in the world). 
			float FresnelView=(1.0 - dot( normalize( float3( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z ).xyz), float3(0,0,1)  ));
			
			// Multiply reflection by fresnel so it's stronger when it's far.
			// How much there should be reflected in relation to the angle you are looking at the water. We take the FresnelView we have calculated
			// and and multiply it by _Reflectpower and get the amount of reflection called "ReflectPowerByAngle".  
			 float ReflectPowerByAngle =_ReflectPower * FresnelView;
			float4 FinalReflex = TexCUBE0 * ReflectPowerByAngle; 
			
			
			
			
			
			// Opacity
			fixed FinalAlpha = 1 + _Opacity;
			
			o.Albedo = FinalDiffuse; // go to outputstruct and put values in as we have defined them 
			o.Normal = UnpackNormal1;
			o.Emission = FinalReflex;
			o.Specular = _Gloss;
			o.Gloss = _Specular;
			o.Alpha = FinalAlpha;

			o.Normal = normalize(o.Normal);
		}
	ENDCG
	}
	Fallback "Diffuse" //if shader cannot run, run diffuse (standard diffuse shader) 
}