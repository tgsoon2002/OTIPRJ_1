using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventory : MonoBehaviour {

	#region Data Members
	List<ItemSlot> listOfItem;
	int maxWeight = 50;
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
		// Get list of item from database if the list is empty
		if (listOfItem == null) {
			listOfItem = InventoryDatabase.Instance.GetInventoryItemForCharacter(charID);
		}
		//update inventory block. 
		Inventory.Instance.ClearInventory(maxWeight);
		foreach (var item in listOfItem) {
			Inventory.Instance.PopulateInventoryFromCharacter(item.itemType,item.itemID,item.quantity);
		}
		//Update character Block

	}

	public void AddItem (ItemInfo item){

		if (item.Item_Object.Is_Stackable) {
			int temp = listOfItem.FindIndex(o => o.itemID == item.Item_Object.Item_ID && o.itemType == (int)item.Item_Object.Base_Item_Type);
			if (temp > -1) {
				listOfItem[temp].quantity += item.Item_Qty;
			} else {
				listOfItem.Add(new ItemSlot(charID, item.Item_Object.Item_ID, item.Item_Qty, (int)item.Item_Object.Base_Item_Type));
			}
		} else {
			listOfItem.Add(new ItemSlot(charID, item.Item_Object.Item_ID, item.Item_Qty, (int)item.Item_Object.Base_Item_Type));
		}
	}

	public void RemoveItem(ItemInfo item){
		int temp = listOfItem.FindIndex(o => o.itemID == item.Item_Object.Item_ID && o.itemType == (int)item.Item_Object.Base_Item_Type);
		if (temp > -1) {
			listOfItem.RemoveAt(temp);
		}

	}


	#endregion

	#region Private Methods

	#endregion

}
