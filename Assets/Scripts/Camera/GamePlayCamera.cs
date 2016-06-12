using UnityEngine;
using System.Collections;

public class GamePlayCamera : MonoBehaviour {
	#region Member
	Transform focusUnit;
	bool following = false;
	#endregion
	#region Built-in Method
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (following) {
			FollowFocusUnit();
		}
	}
	#endregion

	#region MainMethod
	public void ChangeFocusUnit(Transform newTrans){
		focusUnit = newTrans;
		following = true;
	}

	void FollowFocusUnit(){
		this.transform.position = Vector3.Lerp (transform.position, new Vector3(focusUnit.position.x,focusUnit.position.y+ 15.0f,transform.position.z),Time.deltaTime);
	}
	#endregion

}
