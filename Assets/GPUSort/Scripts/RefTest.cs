using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefTest : MonoBehaviour
{
	int[] arr;

	// Use this for initialization
	void Start ()
	{
		arr = new int[10]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		Test (ref arr);

		foreach (int i in arr) {
			Debug.Log (i);
		}
	}

	void Test (ref int[] _arr)
	{
		_arr [9] = 100;
	}
}
