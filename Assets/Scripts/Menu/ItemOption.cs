﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemOption : MonoBehaviour {
	#region Data Members
//	public Transform equipBtn;
//	public Transform dropBtn;
//	public Transform removeBtn;
//	public Transform assignBtn;
	public GameObject ammontPanel;
	public List<Transform> button;
	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods
	void Awake(){
		for (int i = 0; i < 4; i++) {
			button.Add(transform.GetChild(i));	
		}

	}

	// Use this for initialization
	void Start () {
		ClosePanel();
	}

	#endregion

	#region Public Methods

	public void _CallDropPanel(){
		ammontPanel.SetActive(true);
		ClosePanel();
	}

	public void ClosePanel()
	{
		gameObject.SetActive(false);
	}
	/// <summary>
	/// Populates the option for item base on type of item.
	/// </summary>
	/// <param name="slot">Slot in as gameobject containt data.</param>
	public void PopulateOption (GameObject slot){
		transform.position = slot.transform.position;
		switch ((int)slot.GetComponent<InventorySlot>().Inventory_Item.Item_Object.Base_Item_Type) {
		case 0:
			button[0].gameObject.SetActive(true);
			button[3].gameObject.SetActive(false);
			break;
		case 1:
			button[0].gameObject.SetActive(false);
			button[3].gameObject.SetActive(true);
			break;
		default:
			button[0].gameObject.SetActive(false);
			button[3].gameObject.SetActive(false);


			break;
		} 
	}
	#endregion

	#region Private Methods

	#endregion

}
