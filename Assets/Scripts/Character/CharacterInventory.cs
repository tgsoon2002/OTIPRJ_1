using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventory : MonoBehaviour {

	#region Data Members
	List<ItemSlot> listOfItem;
	int currentWeight;
	int maxWeight;
	public int charID;
	#endregion

	#region Setters & Getters
	public int CharacterMaxWeight {
		get{ return  maxWeight; }
		set{ maxWeight = value; }
	}
	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () {
		listOfItem = InventoryDatabase.Instance.GetInventoryItemForCharacter(charID);
	}


	#endregion

	#region Public Methods
	public void RepopulateInventory (){
		Inventory.Instance.ClearInventory();
		foreach (var item in listOfItem) {
			Inventory.Instance.AddItem(item.itemType,item.itemID,item.quantity);
		}
	}
	#endregion

	#region Private Methods

	#endregion

}
