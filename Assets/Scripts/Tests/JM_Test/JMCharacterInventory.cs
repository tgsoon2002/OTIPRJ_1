using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JMCharacterInventory : MonoBehaviour 
{
	#region Data Members

	List<JMItemInfo> listOfItem;
	int maxWeight = 50;				// This value is hard coded. Character stats should
									// determine the weight.
	public int charID;				// The character's ID.
	public GameObject quickBarRef;

	#endregion

	#region Setters & Getters

	// Gets and sets the character's max weight
	public int CharacterMaxWeight 
	{
		get{ return  maxWeight; }
		set{ maxWeight = value; }
	}

	public List<JMItemInfo> List_Of_Item
	{
		get { return listOfItem; }
	}

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
	void Start () 
	{
		//listOfItem = JMInventoryDB.Instance.GetInventoryItemForCharacter(charID);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Ask Kien WTF this does later.  
	/// Might remove this function later.
	/// </summary>
	public void RepopulateInventory ()
	{
		// Get list of item from database if the list is empty
		if (listOfItem == null) 
		{
			listOfItem = JMInventoryDB.Instance.GetInventoryItemForCharacter(charID);
		}

		//update inventory block. 
		//Inventory.Instance.ClearInventory(maxWeight);


		foreach (var item in listOfItem) 
		{
			//to do: 
			//Implement JMInventory later.
			//JMInventoryMenu._instance.PopulateInventoryFromCharacter();
		}
		//Update character Block
	}

	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void AddItem (JMItemInfo item)
	{
		// Checking to see if the item is stackable...
		if (item.item.Is_Stackable)
		{
			// Obtain the index of the item on the list
			int temp = listOfItem.FindIndex(o => o.item.Item_ID == item.item.Item_ID && 
				o.item.Base_Item_Type == item.item.Base_Item_Type);

			// If the temp > -1, the item has been found
			if (temp > -1) 
			{
				listOfItem[temp].itemQuantity += item.itemQuantity;
				JMQuickBarManager.Instance.UpdateQuickBarSlot (item);
			} 
			// Otherwise, the item has not been found.
			// This means the item is a new stackable item.
			// Therefore, the new item is added to the list.
			else 
			{
				listOfItem.Add(new JMItemInfo(charID, item.item.Item_ID, item.itemQuantity, 
							  (int)item.item.Base_Item_Type));
			}
		} 
		// Otherwise, the item is an Equipment.
		// Equipment shall always be added to the list.
		else 
		{
			listOfItem.Add(new JMItemInfo(charID, item.item.Item_ID, item.itemQuantity, 
						  (int)item.item.Base_Item_Type));
		}
	}

	/// <summary>
	/// Removes the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void RemoveItem(JMItemInfo item)
	{
		// Grabbing the index of the item to be removed.
		int temp = listOfItem.FindIndex(o => o.item.Item_ID == item.item.Item_ID && 
									    o.item.Base_Item_Type == item.item.Base_Item_Type);

		// If a valid index of the item has been found, the item is removed.
		// REMINDER: Possible unit testing (try/catch block) to be added here later.
		if (temp > -1) 
		{
			listOfItem.RemoveAt(temp);
		}

	}

	public void AccessInventoryMenu (bool isMenuOpen)
	{
		if (isMenuOpen) 
		{
			JMQuickBarManager.itemAssigned += AssignItemToQuickBarSlot;
		} 
		else 
		{
			JMQuickBarManager.itemAssigned -= AssignItemToQuickBarSlot;
		}
	}

	public void DropItem (int id, int amount)
	{
		int tempIndex = listOfItem.FindIndex (o => o.Item_Info.Item_ID == id);

		if (listOfItem [tempIndex].itemQuantity - amount <= 0) 
		{
			if (listOfItem [tempIndex].Is_In_Quickbar) 
			{
				quickBarRef.GetComponent<JMQuickBarManager> ().CheckAndClearOtherSlots (id);
			}

		}
	}

	#endregion

	#region Private Methods

	// The item is assigned to the Quickbar slot.
	// When the user drags the item to the Quickbar slot,
	// An event is called in conjunction with this function.
	private void AssignItemToQuickBarSlot (JMItemInfo item)
	{
		listOfItem [listOfItem.FindIndex (o => o.Item_Info.Item_ID == item.Item_Info.Item_ID)].Is_In_Quickbar = true;
	}


	// TODO: to complete later.
	private void UpdateFromQuickbar (JMItemInfo item)
	{



	}

	#endregion

	#region Helper Classes/Structs

	#endregion

}
