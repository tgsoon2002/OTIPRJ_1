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
		if (item.item.Is_Stackable)
		{
			int temp = listOfItem.FindIndex(o => o.item.Item_ID == item.item.Item_ID && 
				o.item.Base_Item_Type == item.item.Base_Item_Type);

			if (temp > -1) 
			{
				listOfItem[temp].itemQuantity += item.itemQuantity;
			} 
			else 
			{
				listOfItem.Add(new JMItemInfo(charID, item.item.Item_ID, item.itemQuantity, 
							  (int)item.item.Base_Item_Type));
			}
		} 
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
		int temp = listOfItem.FindIndex(o => o.item.Item_ID == item.item.Item_ID && 
									    o.item.Base_Item_Type == item.item.Base_Item_Type);

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
				quickBarRef.GetComponent<JMQuickBarManager>().CheckAndClearOtherSlots()
			}

		}
	}

	#endregion

	#region Private Methods

	private void AssignItemToQuickBarSlot (int id)
	{
		listOfItem [listOfItem.FindIndex (o => o.Item_Info.Item_ID == id)].Is_In_Quickbar = true;
	}

	#endregion

	#region Helper Classes/Structs

	#endregion

}
