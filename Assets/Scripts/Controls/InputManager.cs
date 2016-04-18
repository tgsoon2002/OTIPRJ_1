﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;

public class InputManager : MonoBehaviour
{
    #region Data Members
    bool isShiftPressed = false;
	Stack<CharacterInputs> lastInput = new Stack<CharacterInputs>();
<<<<<<< HEAD
<<<<<<< HEAD
	public GameObject playerRef;
=======
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
	bool isCharMovement = true;

    //Enumeration of Control Type
    enum BindType
	{
		// These should be assigned numbers.
		Bind_Type_JoystickInput = 0,
		Bind_Type_KeyBoardInput,
		Bind_Type_MouseInput,
		Bind_Type_Axis
	}

	//Struct to contain all information about a 'Key Binding'
	struct KeyBinds
	{
		//Joystick, KB, or Analog Sticks
		public BindType bindType;
	
		//This is for the Analog Sticks. So that '+' and '-'
		//axis values can be mapped to different commands (e.g. Left/Right)
		public bool axisPositive;

		//Binding name for Axis/Directional buttons
		public string controllerBindName;

		//Binding name for Keyboard Inputs
		public KeyCode keyBindName;

		//Binding name for Mouse Input
		public string mouseBindName;

	}

	//Input per frame buffer array, will hold a float for 
	//each type of Game Command. Updated once per frame. 
	//This shall be passed to the GameObject that requires
	//user input.
	private float[] inputFrameBuffer;

	/// <summary>
	/// This arrary holds the buffer for the inputs towards
	/// the UI.  Updated once per frame.  Passed to the 
	/// GameObject that requires user input.
	/// </summary>
	private float[] uiInputBuffer;

	//Dictionary to map our Enumerated Commands to actual Inputs
	Dictionary<CharacterInputs, List<KeyBinds>> gameCommandTable;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods


	// Use this for initialization
	void Start () 
	{
		InitKeyBinds();
		SetDefaultControls();
		inputFrameBuffer = new float[Enum.GetValues(typeof(CharacterInputs)).Length];

		// Be sure to change the typeof to another enumeration for UI inputs.
		uiInputBuffer = new float[Enum.GetValues(typeof(CharacterInputs)).Length];
	}

	// Update is called once per frame
	void Update () 
	{
		ClearInputBuffer();
        ProcessInput();
        PassInput();

        /*
        if (isShiftPressed == false)
        {
            //ClearInputBuffer();
            //ProcessInput();
            PassInput();
        }
        */

        //PassInput();
    }

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void InitKeyBinds()
	{
		//Initialize the Dictionary
		gameCommandTable = new Dictionary<CharacterInputs, List<KeyBinds>>();

		foreach(CharacterInputs gameCmd in Enum.GetValues(typeof(CharacterInputs)))
		{
			gameCommandTable.Add(gameCmd, new List<KeyBinds>());
		}
	}

	private void ClearInputBuffer()
	{
		foreach(CharacterInputs cmd in Enum.GetValues(typeof(CharacterInputs)))
		{
			inputFrameBuffer[(int)cmd] = 0;
		}
	}

	private void ProcessInput()
	{
		foreach(KeyValuePair<CharacterInputs, List<KeyBinds>> cmd in gameCommandTable)
		{
			foreach(KeyBinds bind in cmd.Value)
			{
				switch(bind.bindType)
				{
					case BindType.Bind_Type_KeyBoardInput:
					{
						AddInputToBuffer(cmd.Key, GetKeyBoardInput(bind));
						break;		
					}

					case BindType.Bind_Type_Axis:
					{
						break;
					}

					case BindType.Bind_Type_JoystickInput:
					{
						break;
					}

					case BindType.Bind_Type_MouseInput:
					{
						AddInputToBuffer(cmd.Key, GetMouseClickInput(bind));
						break;
					}
				}
			}
		}
	}

<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======

>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
	private void AddInputToBuffer(CharacterInputs cmd, float cmdVal)
	{
		//Nothing happens if the Command Value is zero
		if(cmdVal == 0)
		{
			return;
		}
<<<<<<< HEAD
<<<<<<< HEAD

		// Checks for type of input (character movement or UI)
		if (isCharMovement) 
		{
			//Store value of integers to be actually processed
			//later on. If two devices made an input at the same
			//time. The most recent input shall be the one to use.
			inputFrameBuffer[(int)cmd] = cmdVal;
		} 
		else 
		{
			uiInputBuffer[(int)cmd] = cmdVal;
		}
=======
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
		
		//Store value of integers to be actually processed
		//later on. If two devices made an input at the same
		//time. The most recent input shall be the one to use.
		inputFrameBuffer[(int)cmd] = cmdVal;

<<<<<<< HEAD
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
	}

	private float GetKeyBoardInput(KeyBinds bind)
	{
		//Declaring local variables
		float toReturn = 0.0f;

<<<<<<< HEAD
<<<<<<< HEAD
		if(Input.GetKeyDown(bind.keyBindName))
		{
			Debug.Log ("DONALD TRUMP 2016");
=======
		if(Input.GetKey(bind.keyBindName))
		{
			//Debug.Log ("TAP");
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
		if(Input.GetKey(bind.keyBindName))
		{
			//Debug.Log ("TAP");
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
			toReturn = 1.0f;
		}

		if(Input.GetKeyUp(bind.keyBindName))
		{
<<<<<<< HEAD
<<<<<<< HEAD
=======
			//Debug.Log("HOLD");
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
			//Debug.Log("HOLD");
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
			toReturn = 2.0f;
		}

		return toReturn;
	}

	private float GetJoyStickButtonInput(KeyBinds bind)
	{
		return Input.GetAxis(bind.controllerBindName);
	}

	private float GetAxisInput(KeyBinds bind)
	{
		//Declaring local variables
		float axisVal = Input.GetAxis(bind.controllerBindName);
		float toReturn = 0.0f;

		if(bind.axisPositive)
		{
			if(axisVal > 0.0f)
			{
				toReturn = axisVal;
			}
		}
		else
		{
			if(axisVal < 0.0f)
			{
				toReturn = axisVal;
			}
		}
			
		return toReturn;
	}
		
	private float GetMouseClickInput(KeyBinds bind)
	{
<<<<<<< HEAD
<<<<<<< HEAD
		//Declaring local variables
		float toReturn = 0.0f;

		if(bind.mouseBindName == "mouse_1" || bind.mouseBindName == "mouse_2")
		{
			toReturn = 1.0f;
		}

		return toReturn;
=======
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
		
		if(bind.mouseBindName == "mouse_1" || bind.mouseBindName == "mouse_2")
		{
			return 1.0f;
		}
		else 
		{
			return 0.0f;
		}
<<<<<<< HEAD


>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
			
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
	}

	private void PassInput()
	{
<<<<<<< HEAD
<<<<<<< HEAD
		playerRef.GetComponent<TestMovement> ().ProcessCommand (inputFrameBuffer);
=======
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
//HACK
		//playerRef.GetComponent<CharacterBody>().ProcessCommand(inputFrameBuffer);
		InputProcessor.Instance.ReadInput(inputFrameBuffer);
//HACK
<<<<<<< HEAD
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
    }
		
	//This function is called internally should an outside source wants to reset
	//controls to their Default state.
	//No easy way around this...

	//NOTE: Need to add more
	private void SetDefaultControls()
	{
		#region Keyboard & Mouse Mapping

		#region Move Left

		KeyBinds moveLeft = new KeyBinds();
		moveLeft.bindType = BindType.Bind_Type_KeyBoardInput;
		moveLeft.keyBindName = KeyCode.A;

		//Add the KeyBind to the Dictionary
		gameCommandTable[CharacterInputs.Character_Move_Left].Add(moveLeft);

        #endregion

<<<<<<< HEAD
<<<<<<< HEAD
        #region Dash

        KeyBinds dashLeft  = new KeyBinds();
        dashLeft.bindType = BindType.Bind_Type_KeyBoardInput;
		dashLeft.keyBindName = KeyCode.LeftShift;

		gameCommandTable[CharacterInputs.Character_Dash_Left].Add(dashLeft);

        #endregion

=======
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
=======
>>>>>>> 9ed8b8f7e09796e796746d4740337136e8e2f54e
        #region Move Right

        KeyBinds moveRight = new KeyBinds();
		moveRight.bindType = BindType.Bind_Type_KeyBoardInput;
		moveRight.keyBindName = KeyCode.D;

		//Add the KeyBind to the Dictionary
		gameCommandTable[CharacterInputs.Character_Move_Right].Add(moveRight);

		#endregion

		#region Crouch

		KeyBinds crouch = new KeyBinds();
		crouch.bindType = BindType.Bind_Type_KeyBoardInput;
		crouch.keyBindName = KeyCode.S;

		//Add the KeyBind to the Dictionary
		gameCommandTable[CharacterInputs.Character_Crouch].Add(crouch);

		#endregion

		#region Primary Weapon

		KeyBinds primaryWpn = new KeyBinds();
		primaryWpn.bindType = BindType.Bind_Type_MouseInput;
		primaryWpn.mouseBindName = "mouse_1";

		//Add the KeyBind to the Dictionary
		gameCommandTable[CharacterInputs.Character_Primary_Weapon].Add(primaryWpn);

		#endregion

		#region Secondary Weapon

		KeyBinds secondWpn = new KeyBinds();
		secondWpn.bindType = BindType.Bind_Type_MouseInput;
		secondWpn.mouseBindName = "mouse_2";

		//Add the KeyBind to the Dictionary
		gameCommandTable[CharacterInputs.Character_Secondary_Weapon].Add(secondWpn);

		#endregion

		#region Jump

		KeyBinds jump = new KeyBinds();
		jump.bindType = BindType.Bind_Type_KeyBoardInput;
		jump.keyBindName = KeyCode.Space;

		//Add the KeyBind to the Dictionary
		gameCommandTable[CharacterInputs.Character_Jump].Add(jump);

		#endregion

		#endregion
	}

	#endregion
}
