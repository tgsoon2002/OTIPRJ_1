using UnityEngine;
using System.Collections;

public class RotateModel : MonoBehaviour {

	float direction = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f,direction*2.0f,0.0f);
	}
	public void RoateModel(float newDirection){
		direction = newDirection;
	}
	public void StopRotate(){
		direction = 0.0f;
	}
}
