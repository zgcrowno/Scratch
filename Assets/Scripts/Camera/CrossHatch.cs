using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CrossHatch : MonoBehaviour {

	private Material material;

    void Awake()
    {
        material = new Material(Shader.Find("Hidden/CrossHatch"));
    }

    //Postprocess the Image
    void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit (source, destination, material);
	}
}
