using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Items_And_Inventory;

public class InventoryMenu : MonoBehaviour, IPointerClickHandler 
{
	#region Data Members

	public GameObject menuPrefab;
	public GameObject quickBarPrefab;

	public GameObject itemSlotPrefab;
	public GameObject itemSlotOptionPrefab;

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

	#endregion

	#region Built-in Unity Methods

	void OnEnable()
	{
		//Make it listen

	}

	void Awake()
	{
		_instance = this;
	}

	void OnDisable()
	{
		//Disable listening		
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Raises the pointer click event.
	/// Called when the user clicks on an ItemSlot.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick(PointerEventData eventData)
	{
		//When user presses the Right Mouse click
		if(Input.GetMouseButtonUp(1))
		{
			//Checks if the Right Mouse click pressed an Item Slot GameObject
			if(eventData.pointerCurrentRaycast.gameObject.GetComponent<ISlottable>() != null)
			{
				//If it is, then disable the Inventory, and all of its children
				//so the user won't be clicking by accident all over the place.
				gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

				//Makes the sub-menu appear on the menu.
				//When any button is pressed on this prefab,
				//blocksRayCasts should be set to true again.
				itemSlotOptionPrefab.SetActive(true);
			}
		}
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
		else
		{
			//Will if the index is not -1
			try
			{
				//Retrieve the index of the given item
				int index = itemSlots.FindIndex(o => o.GetComponent<ISlottable>().Item_ID == _item.Item_ID);	

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

		//Set the Item Slot on the Menu
		if(item.Quickbar_Index > -1)
		{
			temp.transform.SetParent(menuPrefab.transform);	
		}
		//Set it on the Quick Bar otherwise
		else
		{
			temp.transform.SetParent(quickBarPrefab.GetComponent<IContainable>().
								     Item_Containers[item.Quickbar_Index].transform);
		}
	}
		
	/// <summary>
	/// Removes the slot.
	/// </summary>
	/// <param name="item">Item.</param>
	private void RemoveSlot(GameObject slot)
	{
		//Destroy the GameObject here
	}
		
	#endregion
}
