using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BitTest : MonoBehaviour
{
	[SerializeField] int x;
	[SerializeField] int shift;

	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("before 2 number x = " + Convert.ToString (x, 2));
		var _x = x << shift;
		Debug.Log ("after 10 number x = " + _x);
		Debug.Log ("after 2 number x = " + Convert.ToString (_x, 2));
	}
}
