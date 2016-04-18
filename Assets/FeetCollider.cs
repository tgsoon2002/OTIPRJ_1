using UnityEngine;
//using System.Collections;

public class FeetCollider : MonoBehaviour {

	public bool OnTheGround = true;
	public bool IsTouchGround {
		get{ return  OnTheGround; }
		set{ OnTheGround = value;}
	}
	void OnTriggerEnter(Collider floor){
		if (floor.CompareTag ("Environment") ) {
			OnTheGround = true	;
		}
	}
	void OnTriggerExit(Collider floor){
		if (floor.CompareTag ("Environment") ) {
			OnTheGround = false	;
		}
	}
}
