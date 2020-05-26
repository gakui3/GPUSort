//https://t-pot.com/program/90_BitonicSort/index.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitonicSort : MonoBehaviour
{
	[SerializeField] ComputeShader bitonicSort;
	[SerializeField] Shader render;
	[SerializeField] Shader noise;
	[SerializeField] int Count;
	[SerializeField] int max;

	[SerializeField, Range (0, 300)] int value;

	Material mat;
	Material noiseMat;
	string str;
	RenderTexture read;
	RenderTexture output;
	bool isStart = false;

	int step = 0;
	int stepno = 0;
	int offset = 0;
	int stage = 0;

	int logsize;

	void Start ()
	{
		//create noisetexture
		read = new RenderTexture (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		read.Create ();

		output = new RenderTexture (Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		output.enableRandomWrite = true;
		output.Create ();

		logsize = (int)Mathf.Log (Screen.width * Screen.height, 2);
		max = logsize * (logsize + 1) / 2;


		InitNoiseTex ();

        StartCoroutine(startSort());
	}

    IEnumerator startSort()
    {
        yield return new WaitForSeconds(4f);

        isStart = !isStart;

        yield return null;
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
			
		step = Count;
		int rank = 0;

		for (rank = 0; rank < step; rank++) {
			step -= rank + 1;
		}
			
		//2,4,8,16,32,64,128...
		stepno = 1 << (rank + 1);
		//Debug.Log ("stepno = " + stepno);

		//1,2,4,8,16,32,64...
		offset = 1 << (rank - step);
		//Debug.Log ("offset = " + offset);

		//2,4,8,16,32,64,128...
		stage = 2 * offset;
		//Debug.Log ("stage = " + stage);

		bitonicSort.SetTexture (0, "Output", output);
		bitonicSort.SetTexture (0, "Read", read);
		bitonicSort.SetInt ("Count", Count);
		bitonicSort.SetInt ("ScreenWidth", Screen.width);
		bitonicSort.SetInt ("Max", max);
		bitonicSort.SetInt ("stepno", stepno);
		bitonicSort.SetInt ("offset", offset);
		bitonicSort.SetInt ("stage", stage);
		bitonicSort.Dispatch (0, Screen.width / 8, Screen.height / 8, 1);

		if (Count < max - 1) {
			Count += 1;
		}
	}
}
