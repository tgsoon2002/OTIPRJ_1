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
		if(type == 1)
		{
			if(cmd == CharacterInputs.Character_Move_Left)
			{
				if(!startTimer)
				{
					tapTimer = Time.time;
					startTimer = true;
				}

				float temp = 0.0f;
	
				temp = Time.time - tapTimer;

				if( temp > 0.5f && tap <= -0.5f)
				{
					Debug.Log("Sprint...");

					physics.AddForce(Vector3.left * 100.0f);
					prevInput.Push(CharacterInputs.Character_Move_Left);

					tap = -2;
				}
				else
				{
					physics.AddForce(Vector3.left * 20.0f);
					prevInput.Push(CharacterInputs.Character_Move_Left);
					//tap = false;
				}
			}
		}
		else if(type == 2)
		{
			if(cmd == CharacterInputs.Character_Move_Left)
			{
				startTimer = false;
				tapTimer = Time.time - tapTimer;

				if(tap == 2 || tap == -2) 
				{
					tap = 0;
				}

				if(tapTimer < 0.3f && prevInput.Peek() == CharacterInputs.Character_Move_Left)
				{
					lastTapped = Time.time;
					if(tap == -1)
					{
						physics.AddForce(Vector3.left * 1000.0f);
						prevInput.Pop();
						prevInput.Push(CharacterInputs.Character_Move_Left);
						tap = 0;
						Debug.Log("DASH");
					}
					else
					{
						tap = -1;
					}
				}

				prevInput.Clear();
			}
		}
	}

	private void MoveRight(CharacterInputs cmd, int type)
	{
		if(type == 1)
		{
			if(cmd == CharacterInputs.Character_Move_Right)
			{
				if(!startTimer)
				{
					tapTimer = Time.time;
					startTimer = true;
				}

				float temp = 0.0f;

				temp = Time.time - tapTimer;

				if( temp > 0.5f && tap <= -0.5f)
				{
					Debug.Log("Sprint...");

					physics.AddForce(Vector3.right * 100.0f);
					prevInput.Push(CharacterInputs.Character_Move_Right);

					tap = -2;
				}
				else
				{
					physics.AddForce(Vector3.right * 20.0f);
					prevInput.Push(CharacterInputs.Character_Move_Right);
					//tap = false;
				}
			}
		}
		else if(type == 2)
		{
			if(cmd == CharacterInputs.Character_Move_Right)
			{
				startTimer = false;
				tapTimer = Time.time - tapTimer;

				if(tap == 2 || tap == -2) 
				{
					tap = 0;
				}

				if(tapTimer < 0.3f && prevInput.Peek() == CharacterInputs.Character_Move_Right)
				{
					lastTapped = Time.time;
					if(tap == -1)
					{
						physics.AddForce(Vector3.right * 1000.0f);
						prevInput.Pop();
						prevInput.Push(CharacterInputs.Character_Move_Right);
						tap = 0;
					}
					else
					{
						tap = -1;
					}
				}

				prevInput.Clear();
			}
		}
	}

	private void Jump(CharacterInputs cmd)
	{
		
	}

	private void Crouch(CharacterInputs cmd)
	{
		
	}
		
	#endregion
}
