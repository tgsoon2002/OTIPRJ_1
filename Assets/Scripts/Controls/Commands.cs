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
	public BasePlayerCharacter focusedUnit;

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
		commands += SwitchLeft;
		commands += SwitchRight;
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
			// move left button down and hold
			if(type == 1)
			{
				if(!startTimer)
				{
					tapTimer = Time.time;
					startTimer = true;
				}


				// check if character should sprint or walk
				if( Time.time - tapTimer > 0.2f && tap <= -1)
				{
					Debug.Log("Sprint...");
					SquadManager.Instance.focusedUnit.MoveThisUnit(-5.0f);
					prevInput.Push(CharacterInputs.Character_Move_Left);
					tap = -2;
				}
				else
				{
					SquadManager.Instance.focusedUnit.MoveThisUnit(-1.0f);
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
					if(tapTimer < 0.2f && prevInput.Peek() == CharacterInputs.Character_Move_Left)
					{
						lastTapped = Time.time;
						if(tap == -1)
						{
							SquadManager.Instance.focusedUnit.DashThisUnit(-1.0f);
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
	/// 
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
					if( Time.time - tapTimer > 0.2f && tap >= 1)
					{
						SquadManager.Instance.focusedUnit.MoveThisUnit(5.0f);
						prevInput.Push(CharacterInputs.Character_Move_Right);
						tap = 2;
					}
					else
					{
						SquadManager.Instance.focusedUnit.MoveThisUnit(1.0f);
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

					if(tapTimer < 0.2f && prevInput.Peek() == CharacterInputs.Character_Move_Right)
					{
						lastTapped = Time.time;
						if(tap == 1)
						{
							SquadManager.Instance.focusedUnit.DashThisUnit(1.0f);
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

	public void SwitchLeft (CharacterInputs cmd, int type)
	{
		if (cmd == CharacterInputs.Character_Switch_Left) 
		{
			Debug.Log ("Cucks for Bernie Sanders");

			if (type == 2) 
			{
				Debug.Log ("Gibs me dat");
				Vector3 curPlayPosition = SquadManager.Instance.focusedUnit.gameObject.transform.position;
				bool isLeft = false;

				foreach (BasePlayerCharacter tmp in SquadManager.Instance.Player_Char_List) 
				{
					Vector3 tmpPos = tmp.gameObject.transform.position;

					if (tmpPos.x < curPlayPosition.x && !isLeft) 
					{
						Debug.Log ("CUCKED");
						isLeft = true;
						//SquadManager.Instance.focusedUnit.gameObject = tmp.gameObject;
						//SquadManager.Instance.SwitchCurrent(tmp);
						SquadManager.Instance.focusedUnit = tmp;
					}	
				}

				if (!isLeft) 
				{
					//float mostRight = curPlayPosition.x;
					BasePlayerCharacter theRight = SquadManager.Instance.focusedUnit;

					foreach (BasePlayerCharacter tmp in SquadManager.Instance.Player_Char_List) 
					{
						Vector3 tmpPos = tmp.gameObject.transform.position;
						float distance = 0.0f;

						if(tmpPos.x > theRight.gameObject.transform.position.x) 
						{
							theRight = tmp;
						}	
					}

					SquadManager.Instance.focusedUnit = theRight;
				}
			}
		}
	}

	public void SwitchRight (CharacterInputs cmd, int type)
	{
		if (cmd == CharacterInputs.Character_Switch_Right) 
		{
			Debug.Log ("Help us Donald Trump");

			if (type == 2) 
			{
				Debug.Log ("Help I'm being oppressed by the patriarchy!!!");
				Vector3 curPlayPosition = SquadManager.Instance.focusedUnit.gameObject.transform.position;
				bool isRight = false;

				foreach (BasePlayerCharacter tmp in SquadManager.Instance.Player_Char_List) 
				{
					Vector3 tmpPos = tmp.gameObject.transform.position;

					if (tmpPos.x > curPlayPosition.x && !isRight) 
					{
						isRight = true;
						SquadManager.Instance.focusedUnit = tmp;
					}	
				}

				if (!isRight) 
				{
					//float mostRight = curPlayPosition.x;
					BasePlayerCharacter theLeft = SquadManager.Instance.focusedUnit;

					foreach (BasePlayerCharacter tmp in SquadManager.Instance.Player_Char_List) 
					{
						Vector3 tmpPos = tmp.gameObject.transform.position;
						float distance = 0.0f;

						if(tmpPos.x < theLeft.gameObject.transform.position.x) 
						{
							theLeft = tmp;
						}	
					}

					SquadManager.Instance.focusedUnit = theLeft;
				}
			}
		}
	}


	public void Jump(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Jump)
		{
			if(type == 2)
			{
				SquadManager.Instance.focusedUnit.Jump();
			}
		}
	}

	public void Crouch(CharacterInputs cmd, int type)
	{
		if(cmd == CharacterInputs.Character_Crouch)
		{
			if(type == 1)
			{
				Debug.Log("Crouch down!");
				//focusUnit.Crouch();
			}
			if (type == 2) 
			{
				Debug.Log("Stand up!");
			}
		}
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
