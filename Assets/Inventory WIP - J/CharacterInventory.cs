using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items_And_Inventory;
using Player_Info;

public class CharacterInventory : MonoBehaviour
{
	#region Data Members

	private List<ItemInfo> itemList;
	private int currentWeight;
	private int tempMaxWeight;			//Delete sthis shit later.

	public GameObject playerReference;	//Player Reference.
	public GameObject lootPrefab;    	//Prefab for a Loot this class
								    	//shall instantiate.

	#region Events

	/*
	 *  This is the delegate for the event - ItemEvent.
	 *	PARAMETERS:
	 *  - item will be passed in so listeners shall know which
	 *    item to update and the quantity to update.
	 *  - state is mainly used for InventoryMenu
	 *	  true  : Create a slot
	 *	  false : Update the slot and possibly remove it
	 */
	public delegate void ItemAction(ItemInfo item, bool state);
	public event ItemAction ItemEvent;

	#endregion

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	void Start()
	{
		itemList = new List<ItemInfo>();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Initialiazes the inventory menu.
	/// Called when the Inventory Menu is
	/// opened by the user.
	/// </summary>
	public void InitializeInventoryMenu()
	{
		foreach(ItemInfo item in itemList)
		{
			TriggerItemEvent(item, true);
		}
	}

	/// <summary>
	/// Called when adding an Item to the CharacterInventory
	/// </summary>
	/// <param name="newItem">New item.</param>
	public void AddItem(ItemInfo newItem)
	{
		//Checks if the character's weight can carry the new item.
		if(newItem.TotalWeight() + currentWeight <= playerReference.GetComponent<ICarryable>().Player_Max_Weight)
		{
			//Checking if the new item is stackable
			if(newItem.Item_Info.Is_Stackable)
			{
				//Check if newItem already exists in the list.
				int index = itemList.FindIndex(item => item.Item_Info.Item_ID == newItem.Item_ID);

				//IList.FindIndex returns -1, if the list does not contain the new item
				if(index > -1)
				{
					itemList[index].Item_Quantity += newItem.Item_Quantity;

					if(playerReference.GetComponent<IControllable>().Character_Is_Selected)
					{ 
						//Trigger an event!
						//Passing the item to be updated.
						//The second argument is false because we
						//are just updating existing information.
						TriggerItemEvent(itemList[index], false);
					}
				}
				//This case is when a new Stackable item is going to be added
				//to the Inventory.
				else
				{
					ItemInfo temp = newItem;
					itemList.Add(temp);

					//Checking if Inventory Menu UI is displayed
					if(InventoryMenu.Instance.gameObject.activeInHierarchy)
					{
						//If true, trigger an event and pass true to create
						//an item slot element on the Menu UI!
						TriggerItemEvent(itemList[index], true);
					}
				}
			}
			else
			{
				ItemInfo temp = newItem;
				itemList.Add(temp);

				//Checking if Inventory Menu UI is displayed
				if(InventoryMenu.Instance.gameObject.activeInHierarchy)
				{
					//If true, trigger an event and pass true to create
					//an item slot element on the Menu UI!
					TriggerItemEvent(temp, true);
				}
			}
		}
	}

	/// <summary>
	/// Removes the item.
	/// </summary>
	/// <param name="itemID">Item I.</param>
	/// <param name="qty">Qty.</param>
	/// <param name="toDrop">If set to <c>true</c> to drop.</param>
	public void RemoveItem(int itemID, int qty, bool toDrop)
	{
		//Find the index if the item exists
		int index = itemList.FindIndex(item => item.Item_ID == itemID);

		//If the index returns -1, the catch state shall be executed.
		try
		{
			//Check if the Item is Stackable
			if(itemList[index].Item_Info.Is_Stackable)
			{
				//Checks if the quantity to be removed is going to
				//completely zero out the item.
				if(qty >= itemList[index].Item_Quantity)
				{
					//Completely remove the item
					itemList.RemoveAt(index);
				}
				else
				{
					//Simply update the item's quantity.
					itemList[index].Item_Quantity -= qty;
				}
			}
			else
			{
				//This is for the case if the item to be removed is an
				//Equipment item.
				itemList.RemoveAt(index);
			}

			//Trigger the event:
			//Pass false, since we will be 
			//updating the item slot. Or in
			//this case, completely removing it.
			ItemEvent(itemList[index], false);

			//Check if removing means dropping it to the Game World.
			if(toDrop)
			{
				//Instantiate a new ItemInfo object to be used by the
				//Loot GameObject.
				ItemInfo itemToDrop = new ItemInfo(itemList[index], qty);

				//Call DropLoot
				DropLoot(itemToDrop);
			}
		}
		catch
		{
			//Log an error to the console (for now).
			Debug.LogError("WARNING - Removing Item in Inventory that does not exist.");
		}
	}

	/// <summary>
	/// Equips the item.
	/// </summary>
	/// <param name="itemID">Item I.</param>
	public void EquipItem(int itemID)
	{
		
	}

	/// <summary>
	/// Uses the item. This shall be
	/// mainly used by the QuickSlot, and 
	/// sometimes used by the InventoryMenu
	/// </summary>
	/// <param name="itemID">Item I.</param>
	public void UseItem(int itemID)
	{
		
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Triggers the item event.
	/// </summary>
	/// <param name="newItem">New item.</param>
	private void TriggerItemEvent(ItemInfo newItem, bool state)
	{
		ItemEvent(newItem, state);
	}

	private void DropLoot(ItemInfo droppedItem)
	{
		GameObject loot =Instantiate(lootPrefab);

		//Initialize loot components.
		loot.GetComponent<ILootable>().Item_Info = droppedItem;

		//Initialize loot position with character's position plus offset.
		loot.transform.position = new Vector3(playerReference.transform.position.x + 1.0f,
											  playerReference.transform.position.y + 1.5f,
											  loot.transform.position.z);
									
	}

	#endregion

}