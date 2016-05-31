using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Items_And_Inventory;

public class InventoryMenu : MonoBehaviour 
{
	#region Data Members

	public GameObject menuPrefab;
	public GameObject quickBarPrefab;
	public GameObject itemSlotPrefab;
	private List<GameObject> itemSlots;

	[SerializeField]
	private List<GameObject> quickBarSlots;

	private static InventoryMenu _instance;

	#endregion

	#region Setters & Getters

	public static InventoryMenu Instance
	{
		get { return _instance; }
	}

	public List<GameObject> Item_Slots
	{
		get { return itemSlots; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		_instance = this;
		itemSlots = new List<GameObject>();
	}
		
	#endregion

	#region Public Methods

	/// <summary>
	/// Call this first when new character is selected.
	/// </summary>
	/// <param name="currentCharacter">Current character.</param>
	public void StopListeningToEvent(GameObject currentCharacter)
	{
		currentCharacter.GetComponent<CharacterInventory>().ItemEvent -= UpdateSlot;
	}

	/// <summary>
	/// Call this AFTER a new character is selected.
	/// </summary>
	/// <param name="newCharacter">New character.</param>
	public void ListenToEvent(GameObject newCharacter)
	{
		newCharacter.GetComponent<CharacterInventory>().ItemEvent += UpdateSlot;
	}
		
	#endregion

	#region Private Methods

	/// <summary>
	/// Updates the item slot. Is invoked when an event is triggered
	/// by the CharacterInventory class.
	/// </summary>
	/// <param name="_item">Item.</param>
	/// <param name="state">If set to <c>true</c> state.</param>
	/// <param name="isQB">If set to <c>true</c> is Q.</param>
	private void UpdateSlot(IStoreable _item, bool state, bool isQB)
	{
		//If the state is 'true', call AddSlot
		if(state)
		{
			CreateSlot(_item);
		}
		else if(itemSlots.Count > 0)
		{
			//Will if the index is not -1
			try
			{
				Debug.Log("Derp");

				//Retrieve the index of the given item
				int index = itemSlots.FindIndex(o => o.GetComponentInChildren<ISlottable>().Item_ID == _item.Inventory_Unique_ID);	

				//Checks if the quantity to be updated is will NOT equal to zero or less
				if(itemSlots[index].GetComponent<ISlottable>().Item_Quantity + _item.Item_Quantity <= 0)
				{
					//If it does, then it means that the slot has to
					//be destroyed.
					RemoveSlot(itemSlots[index]);
				}
				else
				{
					//If not, and whether or not the item is getting added/subtracted, simply 
					//update the quantity of the Item Slot here.
					//Instead of call the getter/setter, since we'll also update the Text.
					itemSlots[index].GetComponent<ISlottable>().UpdateQuantity(_item.Item_Quantity);
				}
			}
			catch
			{
				//Throw exception error on the console (for now)
				Debug.LogError("WARNING - Accessing Item in Inventory Menu that does not exist.");
			}
		}
	}

	/// <summary>
	/// Adds the slot.
	/// </summary>
	/// <param name="item">Item.</param>
	private void CreateSlot(IStoreable item)
	{
		GameObject temp = Instantiate(itemSlotPrefab);
		temp.GetComponent<ISlottable>().InitializeItemSlot(item);
		temp.GetComponent<ISlottable>().Root_Transform = transform;
		itemSlots.Add(temp);

		//Set the Item Slot on the Menu
		if(item.Grid_Index > -1 && item.Quickbar_Index <= -1)
		{
			if(menuPrefab.GetComponent<IContainable>().Item_Containers[item.Grid_Index].GetComponentInChildren<ISlottable>() == null)
			{
				temp.transform.SetParent(menuPrefab.GetComponent<IContainable>().Item_Containers[item.Grid_Index].transform);	
			}
			else
			{
				itemSlots.Remove(temp);
				Destroy(temp);
			}
		}
		else if(item.Grid_Index <= -1 && item.Quickbar_Index > -1)
		{
			if(quickBarPrefab.GetComponent<IContainable>().Item_Containers[item.Quickbar_Index].GetComponentInChildren<ISlottable>() == null)
			{
				temp.transform.SetParent(quickBarPrefab.GetComponent<IContainable>().
					Item_Containers[item.Quickbar_Index].transform);
			}
			else
			{
				itemSlots.Remove(temp);
				Destroy(temp);
			}
		}
		else
		{
			Debug.Log("CUCKED");
			menuPrefab.GetComponent<InventoryGridHandler>().AddNewItemSlotToGrid(temp);	
		}
	}
		
	/// <summary>
	/// Removes the slot.
	/// </summary>
	/// <param name="item">Item.</param>
	private void RemoveSlot(GameObject slot)
	{
		//Destroy the GameObject here
		Destroy(slot);
	}
		
	#endregion
}
