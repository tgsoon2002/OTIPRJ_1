using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;



public class Commands : MonoBehaviour
{
	#region Data Members

	delegate void CommandMethods(CharacterInputs cmd, int type);
	CommandMethods commands;
	static Commands instance;
	GameObject objToControl;


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

	public static Commands Instance
	{
		get { return instance; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
		commands += MoveLeft;
		commands += MoveRight;
		commands += Jump;
		commands += Crouch;
		commands += PrimaryWeapon;
		commands += SecondaryWeapon;
	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	#region GUICommands

	public void GetInput(CharacterInputs cmd)
	{
		
	}

	public void GUIAccept(CharacterInputs cmd)
	{
		
	}

	public void GUIMoveUp(CharacterInputs cmd)
	{
		
	}

	public void GUIMoveDown(CharacterInputs cmd)
	{
		
	}
		
	public void GUIMoveLeft(CharacterInputs cmd)
	{
		
	}

	public void GUIMoveRight(CharacterInputs cmd)
	{

	}

	#endregion

	#region Character Commands

	public void SetGameObjectToControl(GameObject obj)
	{
		objToControl = obj;
	}

	public void ReceiveInput(CharacterInputs cmd, int type)
	{
		commands(cmd, type);
	}

	public void MoveLeft(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Move_Left)
		{
			if(type == 1)
			{
				Debug.Log("calling move left");
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
	}

	public void MoveRight(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Move_Left)
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
	}

	public void Jump(CharacterInputs cmd, int type)
	{
		
	}

	public void Crouch(CharacterInputs cmd, int type)
	{
		
	}

	public void PrimaryWeapon(CharacterInputs cmd, int type)
	{
		
	}

	public void SecondaryWeapon(CharacterInputs cmd, int type)
	{

	}

	#endregion

	#endregion

	#region Private Methods

	#endregion

}
