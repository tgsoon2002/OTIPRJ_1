using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using CommandsNameSpace;
using GameInputNameSpace;



public class CharacterInput : MonoBehaviour 
{
	#region Data Members

	Stack<CharacterInputs> prevInput;

	bool isSprinting = false;

	#endregion

	#region Setters & Getters

	public bool Is_Sprinting
	{
		get{ return isSprinting; }
		set{ isSprinting = value; }
	}


	#endregion

	#region Built-In Unity Methods

	// Use this for initialization

	void Start () 
	{

		prevInput = new Stack<CharacterInputs> ();
	}

	// Update is called once per frame


	void Update () 
	{


	}

	#endregion

	#region Public Methods

	public void processInput (float[] inputs) 
	{
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

			if(temp_Input_Value != 0) 
			{
		
			}
		}	
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion


}
