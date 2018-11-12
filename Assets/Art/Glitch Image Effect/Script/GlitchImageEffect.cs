using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchImageEffect : MonoBehaviour
{
    public enum GlitchType
    {
        Type1 = 0, 
        Type2 = 1
    }

    public GlitchType type = GlitchType.Type1;

    [Range(0, 1)]
    public float blend = 1;

    [Header("Parameters of Type1")]
	[Range(0, 10)]
	public float frequency = 1;

	[Range(0, 500)]
	public float interference = 130;

	[Range(0, 5)]
	public float noise = 0.15f;

	[Range(0, 20)]
	public float scanLine = 1;

	[Range(0, 1)]
	public float colored = 0.25f;

    private Shader shader = null;

    private Material mtrl = null;

    private Texture2D noiseTex = null;

    private void Awake()
    {
        shader = Shader.Find("Hidden/GlitchImageEffect");
        if (!shader.isSupported)
        {
            enabled = false;
            return;
        }

        mtrl = new Material(shader);

        noiseTex = Resources.Load<Texture2D>("GlitchNoiseTex");
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (mtrl == null || mtrl.shader == null || !mtrl.shader.isSupported)
        {
            enabled = false;
            return;
        }

        mtrl.SetFloat("_Blend", blend);
		mtrl.SetFloat("_Frequency", frequency);
		mtrl.SetFloat("_Interference", interference);
		mtrl.SetFloat("_Noise", noise);
		mtrl.SetFloat("_ScanLine", scanLine);
		mtrl.SetFloat("_Colored", colored);
        mtrl.SetTexture("_NoiseTex", noiseTex);
        Graphics.Blit(src, dest, mtrl, (int)type);
    }

    private void OnDestroy()
    {
        shader = null;

        if (mtrl != null)
        {
            DestroyImmediate(mtrl);
            mtrl = null;
        }

        if(noiseTex != null)
        {
            Resources.UnloadAsset(noiseTex);
            noiseTex = null;
        }
    }
}
