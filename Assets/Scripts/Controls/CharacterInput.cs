using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using CommandsNameSpace;
using GameInputNameSpace;

public class NewBehaviourScript : MonoBehaviour {
=======
using GameInputNameSpace;

public class CharacterInput : MonoBehaviour 
{
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76

	#region Data Members

	Stack<CharacterInputs> prevInput;
<<<<<<< HEAD
=======
	bool isSprinting = false;
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76

	#endregion

	#region Setters & Getters

<<<<<<< HEAD
=======
	public bool Is_Sprinting
	{
		get{ return isSprinting; }
		set{ isSprinting = value; }
	}

>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
<<<<<<< HEAD
	void Start () {
=======
	void Start () 
	{
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
		prevInput = new Stack<CharacterInputs> ();
	}

	// Update is called once per frame
<<<<<<< HEAD
	void Update () {
=======
	void Update () 
	{
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76

	}

	#endregion

	#region Public Methods

	public void processInput (float[] inputs) 
	{
		//TO DO FOR LATER: Put all the Stack.Pop commands to the else.
		foreach (CharacterInputs cmd in Enum.GetValues(typeof(CharacterInputs))) 
		{
<<<<<<< HEAD
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

						} 
						else
						{
							prevInput.Push(CharacterInputs.Character_Move_Left);


							Debug.Log ("MOVE LEFT!!!");
						}
						break;
					case CharacterInputs.Character_Move_Right:

						if(prevInput.Count > 0 && prevInput.Peek() == CharacterInputs.Character_Dash_Left)
						{
							prevInput.Pop();
							prevInput.Push (CharacterInputs.Character_Move_Right);


						}
						else
						{
							prevInput.Push(CharacterInputs.Character_Move_Right);


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

=======
			float temp_Input_Value = inputs[(int)cmd];

			if(temp_Input_Value != 0) 
			{
		
			}
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
		}	
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion


}
