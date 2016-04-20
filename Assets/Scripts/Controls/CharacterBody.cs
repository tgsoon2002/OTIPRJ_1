using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;


public class CharacterBody : MonoBehaviour 
{
	#region Data Members

	Stack<CharacterInputs> prevInput;
	bool startTimer = false;
	int tap;
	float tapTimer;
	float lastTapped;
	delegate void CommandInput(CharacterInputs cmd, int type);

	CommandInput delHandler;

	Rigidbody physics;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () 
	{
		prevInput = new Stack<CharacterInputs> ();
		physics = gameObject.GetComponent<Rigidbody>();

		delHandler += MoveLeft;
		delHandler += MoveRight;
	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	public void ProcessCommand (float[] inputs) 
	{
		//TO DO FOR LATER: Put all the Stack.Pop commands to the else.
		foreach (CharacterInputs cmd in Enum.GetValues(typeof(CharacterInputs))) 
		{
			float temp_Input_Value = inputs [(int)cmd];

			if (temp_Input_Value != 0) 
			{
				delHandler(cmd, (int)temp_Input_Value);
			}
		}	
	}


	#endregion

	#region Private Methods

	private void MoveLeft(CharacterInputs cmd, int type)
	{	
		
	}

	private void MoveRight(CharacterInputs cmd, int type)
	{
		
	}

	private void Jump(CharacterInputs cmd)
	{
		
	}

	private void Crouch(CharacterInputs cmd)
	{
		
	}
		
	#endregion
}
