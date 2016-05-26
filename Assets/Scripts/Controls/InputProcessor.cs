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
	public MapController mapCamera;
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
				// Inventory
				if(cmd == CharacterInputs.Open_Character_Inventory)
				{
					if (temp_Input_Value == 2) {
						MenuManager.Instance.MenuVisibility(0);
					}
				}	
				// Settings
				else if(cmd == CharacterInputs.Open_Settings)
				{
					if (temp_Input_Value == 2) {
						MenuManager.Instance.MenuVisibility(1);
					}
				}
				// Map
				else if (cmd == CharacterInputs.Open_Map) {
					if (temp_Input_Value == 1) {
						mapCamera.ShowMap();
					} else  if(temp_Input_Value == 2) {
						mapCamera.HideMap();
					}
				}
				// Skill Grid
				else if (cmd == CharacterInputs.Open_Skill_Grid) {
					if(temp_Input_Value == 2) {
						MenuManager.Instance.MenuVisibility(2);
					}
				}
				// Squad Manager
				else if (cmd == CharacterInputs.Open_Squad_Manager) {
					if(temp_Input_Value == 2) {
						MenuManager.Instance.MenuVisibility(3);
					}
				}
				// Journal
				else if (cmd == CharacterInputs.Open_Journal) {
					if(temp_Input_Value == 2) {
						MenuManager.Instance.MenuVisibility(4);
					}
				}
				// Others
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
