using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class OddEvenSort : MonoBehaviour
{
	[SerializeField] ComputeShader oddEvenSort;
	[SerializeField] Shader render;
	[SerializeField] Shader noise;
	[SerializeField] int Count;
	[SerializeField] int max;

	Material mat;
	Material noiseMat;
	string str;
	RenderTexture read;
	RenderTexture output;
	bool isStart = false;


	void Start ()
	{
		//create noisetexture
		read = new RenderTexture (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		read.Create ();

		output = new RenderTexture (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		output.enableRandomWrite = true;
		output.Create ();

		max = Screen.width * Screen.height;

		InitNoiseTex ();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			isStart = !isStart;
		}
	}

	void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		if (mat == null) {
			mat = new Material (render);
		}
	
		CulcSort ();

		//render screen
		Graphics.Blit (output, dst, mat);

		Graphics.Blit (output, read);
	}

	void InitNoiseTex ()
	{
		if (noiseMat == null) {
			noiseMat = new Material (noise);
		}

		Graphics.Blit (read, read, noiseMat);
		Graphics.Blit (read, output);
	}

	void CulcSort ()
	{
		if (!isStart) {
			return;
		}

		oddEvenSort.SetTexture (0, "Output", output);
		oddEvenSort.SetTexture (0, "Read", read);
		oddEvenSort.SetInt ("Count", Count);
		oddEvenSort.SetInt ("ScreenWidth", Screen.width);
		oddEvenSort.SetInt ("Max", max);
		oddEvenSort.Dispatch (0, Screen.width / 8, Screen.height / 8, 1);
		Count += 1;
	}
}
