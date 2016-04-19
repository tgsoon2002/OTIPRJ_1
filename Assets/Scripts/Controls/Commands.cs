using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;


/// <summary>
/// Command.cs recive chacteracter control input, proccess then send command to focus unit character
/// </summary>
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
	public BasePlayerCharacter focusUnit;

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
		prevInput = new Stack<CharacterInputs>();
		commands += MoveLeft; // move, dash, sprint
		commands += MoveRight; // move, dash, sprint
		commands += Jump; // ready to jump
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
	/// <summary>
	/// Move character to the left, base on the combo key, can walk or sprint
	/// </summary>
	/// <param name="cmd">Cmd.</param>
	/// <param name="type">Type.</param>
	public void MoveLeft(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Move_Left)
		{
			// move left button down and hold
			if(type == 1)
			{
				if(!startTimer)
				{
					tapTimer = Time.time;
					startTimer = true;
				}


				// check if character should sprint or walk
				if( Time.time - tapTimer > 0.5f && tap <= -1)
				{
					Debug.Log("Sprint...");
					focusUnit.MoveThisUnit(-3.0f);
					prevInput.Push(CharacterInputs.Character_Move_Left);
					tap = -2;
				}
				else
				{
					focusUnit.MoveThisUnit(-1.0f);
					prevInput.Push(CharacterInputs.Character_Move_Left);
				
				}
			}
			// move left button up
			else if(type == 2)
			{
				
					startTimer = false;
					tapTimer = Time.time - tapTimer;

					if(tap == 2 || tap == -2) 
					{
						tap = 0;
					}
					// If previous button is tap left then perform dash left
					if(tapTimer < 0.3f && prevInput.Peek() == CharacterInputs.Character_Move_Left)
					{
						lastTapped = Time.time;
						if(tap == -1)
						{
							focusUnit.DashThisUnit(-1.0f);
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
	/// <summary>
	/// Move character to the right, base on the combo key, can walk or sprint
	/// </summary>
	/// <param name="cmd">Cmd.</param>
	/// <param name="type">Type.</param>
	public void MoveRight(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Move_Right)
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
					// check if character should sprint or walk
					if( Time.time - tapTimer > 0.6f && tap >= 1)
					{
						focusUnit.MoveThisUnit(3.0f);
						prevInput.Push(CharacterInputs.Character_Move_Right);
						tap = 2;
					}
					else
					{
						focusUnit.MoveThisUnit(1.0f);
						prevInput.Push(CharacterInputs.Character_Move_Right);
					}
				}
			}
			else if(type == 2)
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
						if(tap == 1)
						{
							focusUnit.DashThisUnit(1.0f);
							prevInput.Pop();
							prevInput.Push(CharacterInputs.Character_Move_Right);
							tap = 0;
						}
						else
						{
							tap = 1;
						}
					}

					prevInput.Clear();

			}
		}
	}
	/// <summary>
	/// Call fucntion jump
	/// </summary>
	/// <param name="cmd">Cmd.</param>
	/// <param name="type">Type.</param>
	public void Jump(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Jump)
		{
			if(type == 2)
			{
				focusUnit.Jump();
			}
		}
	}

	public void Crouch(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Crouch)
		{
			if(type == 1)
			{
				focusUnit.Crounch(true);
			}
			else if (type == 2) {
				focusUnit.Crounch(false);
			}
		}
	}

	public void PrimaryWeapon(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Primary_Weapon)
		{
			if(type == 2)
			{
				focusUnit.PrimaryAttack();
			}
		}
	}

	public void SecondaryWeapon(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Secondary_Weapon)
		{
			if(type == 2)
			{
				focusUnit.SecondaryAttack();
			}
		}
	}

	#endregion

	#endregion

	#region Private Methods

	#endregion

}
