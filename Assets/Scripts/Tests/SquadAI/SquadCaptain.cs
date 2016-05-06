using UnityEngine;
using System.Collections;

public class SquadCaptain : MonoBehaviour {
	#region Data Members

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-In Unity Methods
	// Use this for initialization
	void Start () 
	{
		Physics2D.IgnoreLayerCollision (10, 11, true);
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.A)) 
		{
			Debug.Log ("CUCKS FOR BERNIE SANDERS");
			gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * 20.0f);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			Debug.Log ("DONALD TRUMP 2016");
			gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 20.0f);
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			TestSquadManager.TriggerCommand (0);
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion

}
