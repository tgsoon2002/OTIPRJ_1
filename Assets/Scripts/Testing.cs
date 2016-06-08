using UnityEngine;
using System;
using System.Collections;

public class Testing : MonoBehaviour {

	string s;
	string b;
	string c;

	// Use this for initialization
	void Start () {
	
		s = Guid.NewGuid().ToString();
		b = Guid.NewGuid().ToString();
		c = Guid.NewGuid().ToString();

		Debug.Log(s +  " " + b + " " + c);
		Debug.Log(s == b);
		Debug.Log(c == c);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
