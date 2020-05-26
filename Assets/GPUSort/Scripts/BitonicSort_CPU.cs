using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitonicSort_CPU : MonoBehaviour {

	[SerializeField] int numCnt = 32;

	int[] results;
	int[] _results;
	string str;

	int max = 0;
	int step = 0;
	int stepno = 0;
	int offset = 0;
	int stage = 0;
	int Cnt = 0;
	int logsize = 0;

	int test = 0;

	// Use this for initialization
	void Start () {
		
		results = new int[numCnt];
		_results = new int[numCnt];

		for (int i = 0; i < numCnt; i++) {
			var n = Random.Range (1, 100);
			results [i] = n;
			str += n + " ";
		}

		logsize = (int)Mathf.Log (numCnt, 2);
		max = logsize * (logsize + 1) / 2;

		Debug.Log (str);
		Debug.Log (max - 1);

		for (int i = 0; i < max; i++) {
			Calculate ();
		}

	}

	void Calculate(){

//		Debug.Log ("test = " + test);
//		test += 1;

		step = Cnt;
		int rank = 0;

		for (rank = 0; rank < step; rank++) {
			step -= rank + 1;
		}

		stepno = 1 << (rank + 1);
		offset = 1 << (rank - step);
		stage = 2 * offset;

		Debug.Log ("stepno = "+ stepno + " offset = " + offset + " stage = " + stage);


		for (int i = 0; i < numCnt; i++) {
			
			int csing = ((i % stage) < offset) ? 1 : -1;

			float cdir = (Mathf.Floor(i / stepno) % 2 <= 0.5) ? 1 : -1;

			int adr1d = csing * offset + i;


			var v0 = results [i];
			var v1 = results [adr1d];

			var min = (v0 < v1) ? i : adr1d;
			var max = (v0 < v1) ? adr1d : i;

			var dst = (csing == cdir) ? min : max;

			_results [i] = results [dst];

			Debug.Log (i + ", " + adr1d);
		}

		if (Cnt < max) {
			Cnt += 1;
		}

		str = " ";

		for (int i = 0; i < numCnt; i++) {
			str += _results [i] + " ";
			results [i] = _results[i];
		}
		Debug.Log (str);
	}
}
