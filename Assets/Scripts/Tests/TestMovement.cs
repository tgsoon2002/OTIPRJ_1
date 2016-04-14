using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;

public class TestMovement : MonoBehaviour {

	private Stack<CharacterInputs> prevInput;
	private float direction;
	private Rigidbody physics;
	bool isDashing = false;


	// Use this for initialization
	void Start () {
		prevInput = new Stack<CharacterInputs>();
		physics = gameObject.GetComponent<Rigidbody>();
		direction = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("STACK SIZE: " + prevInput.Count);

		if (prevInput.Count > 0) {
			Debug.Log (prevInput.Peek());
		}
	}

	public void ProcessCommand (float [] inputs) {

		//TO DO FOR LATER: Put all the Stack.Pop commands to the else.
		foreach (CharacterInputs cmd in Enum.GetValues(typeof(CharacterInputs))) 
		{
			float temp_Input_Value = inputs [(int)cmd];

			if (temp_Input_Value != 0) 
			{
				if(temp_Input_Value == 1)
				{
					switch (cmd) 
					{
					case CharacterInputs.Character_Move_Left:
						if (prevInput.Count > 0 && prevInput.Peek() == CharacterInputs.Character_Dash_Left) 
						{
							prevInput.Pop ();
							prevInput.Push(CharacterInputs.Character_Move_Left);
							physics.AddForce (Vector3.left * 50.0f);
						} 
						else
						{
							prevInput.Push(CharacterInputs.Character_Move_Left);
							physics.AddForce (Vector3.left);

							Debug.Log ("MOVE LEFT!!!");
						}
						break;
					case CharacterInputs.Character_Move_Right:

						if(prevInput.Count > 0 && prevInput.Peek() == CharacterInputs.Character_Dash_Left)
						{
							prevInput.Pop();
							prevInput.Push (CharacterInputs.Character_Move_Right);
							physics.AddForce (Vector3.right * 50.0f);

						}
						else
						{
							prevInput.Push(CharacterInputs.Character_Move_Right);
							physics.AddForce (Vector3.right);

							Debug.Log ("MOVE RIGHT!!!");
						}

						prevInput.Push(CharacterInputs.Character_Move_Right);
						//Debug.Log("MOVE RIGHT");

						break;
					case CharacterInputs.Character_Crouch:
						{
							if(prevInput.Count > 0)
							{

								prevInput.Pop();

							}

							prevInput.Push(CharacterInputs.Character_Crouch);
							//Debug.Log("Holding S");

							break;
						}

					case CharacterInputs.Character_Dash_Left:
						{
							prevInput.Push (CharacterInputs.Character_Dash_Left);
							break;
						}
					}
				}
				else
				{
					Debug.Log("Keyup!");
					prevInput.Clear ();
				}

			}

		}
	}

}
