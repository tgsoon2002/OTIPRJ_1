﻿using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	#region Data Members
	public Transform characterModel;
	public GameObject inventoryCanvas;
	public GameObject skillGridCanvas;
	public CharacterBlock charModelManager;
	private static MenuManager _instance;
	[SerializeField]
	int currentMenu;
	#endregion

	#region Setters & Getters
	public static MenuManager Instance {
		get{ return  _instance; }
		set{ _instance = value; }
	}
	/// <summary>
	/// Gets the current menu. 0:inventory, 1: setting, 2 skill grid, 3 : squad manager 4: journal
	/// </summary>
	/// <value>The current menu.</value>
	public int CurrentMenu {
		get{ return  currentMenu; }
	}
	#endregion

	#region Built-in Unity Methods
	void Awake(){
		_instance = this;
	}
	// Use this for initialization
	void Start () {
		//menu = transform.FindChild("Menu");
		inventoryCanvas.SetActive (false) ;
		skillGridCanvas.SetActive (false) ;
		characterModel.gameObject.SetActive (false) ;
	}

	#endregion

	#region Public Methods
	public void UpdateCharacterBlock(){
		if (currentMenu == 0) {
			charModelManager.UpdateChar();
		}
	}
	/// <summary>
	/// this close all the menu then open the new one accodingly.
	/// 0 : Inventory, 1:Settings, 
	/// 2: SkillGrid, 3: Squad, 4: Journal
	/// </summary>
	/// <param name="visible">If set to <c>true</c> visible.</param>
	public void MenuVisibility (int menuNumber){
		CloseAllMenu();
		if (currentMenu != menuNumber) {
			currentMenu = menuNumber;
			switch (menuNumber) {
			case 0:
				OpenInventory();
				break;
			case 1:
				OpenSettings();
				break;
			case 2:
				OpenSkillGrid();
				break;
			case 3:
				OpenSquadManager();
				break;
			case 4:
				OpenJournal();
				break;
			default:
				break;
			}
		}else{
			currentMenu = -1;
		}
	}
	#endregion

	#region Private Methods
	/// <summary>
	/// Opens the inventory. Turn on the canvas.
	/// turn on the model, update the character model with equip the same as character.
	/// 
	/// </summary>
	void OpenInventory()
	{
		inventoryCanvas.SetActive (true) ;
		characterModel.gameObject.SetActive (true) ;
		charModelManager.UpdateChar();
        SquadManager.Instance.FocusedUnit.GetComponent<CharacterInventory>().InitializeMenu();
	}

	void OpenSettings(){}

	void OpenSkillGrid(){
		skillGridCanvas.SetActive (true) ;
		skillGridCanvas.gameObject.GetComponent<SkillGridManager>().GridObject = true;
	}

	void OpenSquadManager(){}

	void OpenJournal(){}

	void CloseAllMenu()
	{

        Debug.Log("Closing menu. . .");

        switch (CurrentMenu) {
            case 0:
                // Close Inventory
                InventoryMenu.Instance.FlushData();
                inventoryCanvas.SetActive (false) ;
                characterModel.gameObject.SetActive (false) ;
                break;
            case 1:
              
                break;
            case 2:
                // Close SkillGrid
                skillGridCanvas.SetActive (false) ;
                skillGridCanvas.gameObject.GetComponent<SkillGridManager>().GridObject = false;
                break;
            case 3:
     
                break;
            case 4:
       
                break;
            default:
                break;
        }

		
       
		


	}

	#endregion


}
