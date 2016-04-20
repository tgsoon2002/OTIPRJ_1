using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;

public class InputProcessor : MonoBehaviour
{
	#region Data Members

	private GameObject charRef;

	private static InputProcessor instance;

	#endregion

	#region Setters & Getters

	public static InputProcessor Instance
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

	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	public void ReadInput(float[] input)
	{
		foreach (CharacterInputs cmd in Enum.GetValues(typeof(CharacterInputs))) 
		{
			float temp_Input_Value = input [(int)cmd];

			if (temp_Input_Value != 0) 
			{
				if(cmd == CharacterInputs.Open_Character_Inventory)
				{
					//Open Inventory here
					if (temp_Input_Value == 2) {
						MenuManager.Instance.MenuVisibility();
					}

				}	

				else if(cmd == CharacterInputs.Character_Switch_Left)
				{
					//Switch character here
					if (temp_Input_Value == 2) {
						SquadManager.Instance.SwitchFocusCharacter();
					}
				
				}
				else if(cmd == CharacterInputs.Character_Switch_Right)
				{
					//Switch
				}
				else if(cmd == CharacterInputs.Open_Settings)
				{
					//Open settings here
				}
				else
				{

					Debug.Log(cmd);
					
					//Default case is Character movement or GUI Movement
					Commands.Instance.ReceiveInput(cmd, (int)temp_Input_Value);
				}
			}
		}	
	}

	#endregion

	#region Private Methods

	#endregion
}
