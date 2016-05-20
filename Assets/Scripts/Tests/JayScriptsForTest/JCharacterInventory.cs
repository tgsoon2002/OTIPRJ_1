using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JCharacterInventory : MonoBehaviour 
{
	#region Data Members

	private List<JItemInfo> items;
	private NinjaMan player;
	private int currentWeight = 0;
	private bool isCurrent;
	private bool isInventoryMenuActive;

	#region Events

	public delegate void InventoryObserver(int index, int id);
	public static event InventoryObserver itemEvent;

	#endregion

	#endregion

	#region Setters & Getters

	public List<JItemInfo> Character_Items
	{
		get { return items; }
	}

	public bool Inventory_Menu_Active
	{
		set { isInventoryMenuActive = value; }
	}

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () 
	{
		player = gameObject.GetComponent<NinjaMan>();
		items = new List<JItemInfo>();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Adds the item to the list. 
	/// </summary>
	/// <param name="item">Item.</param>
	public void AddItem(JItemInfo item)
	{
		//Checks if the character's weight can carry the new Item.
		if(item.TotalWeight() + currentWeight <= player.Max_Weight)
		{
			//Checks if the item to be added is NOT Stackable.
			//If it is, we will create a new Slot in the list. Doing so
			//shall allow the Inventory Menu to determined which Equipment
			//Items are 'equipped' later.
			if(!item.Item_Info.Is_Stackable)
			{
				//If the item already exists in the list, get its index.
				//If it doesn't exist, List.FindIndex shall return -1.
				int temp = items.FindIndex(o => o.Item_Info.Item_ID == item.Item_Info.Item_ID);

				//If the item exists, update the Quantity.
				if(temp > -1)
				{
					items[temp].Item_Quantity += item.Item_Quantity;

					//Trigger an event -- 
					//1. QuickBar Manager will mainly retrieve this event
					//to update any associated Quick Bar slots.
					//2. If the Inventory Menu listening, this will
					//make it update the menu.
					itemEvent(items[temp].Is_On_Quick_Bar, items[temp].Item_Info.Item_ID);
				}
				//Else, Instantiate an ItemInfo object and store it into
				//the list.
				else
				{
					JItemInfo _tmp = item;
					items.Add(_tmp);

					if(isInventoryMenuActive)
					{
						JInventoryMenu.Instance.CreateSlot(_tmp);
					}
				}
			}

			//If the object is Stackable, also make a new ItemInfo object
			//to add into the list.
			else
			{
				JItemInfo _tmp = item;
				items.Add(_tmp);

				if(isInventoryMenuActive)
				{
					JInventoryMenu.Instance.CreateSlot(_tmp);
				}
			}

			//Update the value of the current weight with the 
			//new item.
			currentWeight += item.TotalWeight();
		}
	}

	/// <summary>
	/// Removes the item. If isDrop is true, then we will
	/// need to instantiate a prefab to the game world and
	/// store ItemInfo to its Loot component.
	/// </summary>
	/// <param name="item">Item.</param>
	/// <param name="isDrop">If set to <c>true</c> is drop.</param>
	public void RemoveItem(JItemInfo item, bool isDrop)
	{
		//Find the index if the Item exists.
		int index = items.FindIndex(o => o.Item_Info.Item_ID == item.Item_Info.Item_ID);

		//If the item to be removed does not exist, the index shall be
		//have a value of -1.
		try 
		{
			//Check if the Item is stackable
			if(item.Item_Info.Is_Stackable)
			{
				//Checks if the quantity of item will completely
				//remove the item within the Character.
				if(item.Item_Quantity >= items[index].Item_Quantity)
				{
					//If true, then completely remove the Item.
					items.RemoveAt(index);

					//Trigger an event -- 
					//1. QuickBar Manager will mainly retrieve this event
					//to update any associated Quick Bar slots.
					//2. If the Inventory Menu listening, this will
					//make it update the menu.
					itemEvent(items[index].Is_On_Quick_Bar, items[index].Item_Info.Item_ID);
				}
				else
				{
					//Simply update the respective item's quantity,
					//if it doesn't.
					items[index].Item_Quantity -= item.Item_Quantity;

					//Trigger an event -- 
					//1. QuickBar Manager will mainly retrieve this event
					//to update any associated Quick Bar slots.
					//2. If the Inventory Menu listening, this will
					//make it update the menu.
					itemEvent(items[index].Is_On_Quick_Bar, items[index].Item_Info.Item_ID);
				}
			}
			else
			{
				//This case will be true if the item to be removed is an Equipment item.
				items.RemoveAt(index);
			}

			//Check here if isDrop is true or false
			if(isDrop)
			{
				DropLoot(item);
			}

			//Update weight
			currentWeight -= item.TotalWeight();
		}
		//Log as an error to the console.
		catch
		{
			Debug.LogError("Removing Item in Inventory that does not Exist.");
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Helper method to drop the loot to the
	/// game world.
	/// </summary>
	/// <param name="info">Info.</param>
	private void DropLoot(JItemInfo info)
	{
		
	}

	//TO DO:
	//Still not done
	private void SetItemOnQuickBar(string id, int index)
	{
		int tmp = index;
		int tmpIndex = items.FindIndex(o => o.Item_Info.Item_Name == id);

		if(JQuickBarManager.Instance.IsSlotOccupied(index))
		{
			//Change JItemInfo - isOnQuickBar to an int. With the value being the index.
		}

		//items[tmpIndex].Is_On_Quick_Bar = true;
		JQuickBarManager.Instance.UpdateSlot(tmp, items[tmpIndex]);
	}

	#endregion
}
