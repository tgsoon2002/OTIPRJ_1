using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JMInventoryMenu : MonoBehaviour 
{
	#region Data Members

	public GameObject lootPrefab;
	public GameObject invSlotPrefab;
	public GameObject playerReference;
	public GameObject itemOptionPanel;
	public GameObject amountOptionPanel;
	public GameObject detailBlockPanel;
	public GameObject itemContainer;

	public Text weightText;

	private JMInventorySlot currentItem;

	[SerializeField]
	private List<GameObject> containers;

	public JMInventorySlot rightClickItem;
	public List<JMInventorySlot> itemSlots;

	private int characterCurrentWeight = 50;
	private int characterMaxWeight = 50;

	// Might take this out later.
	public static JMInventoryMenu _instance;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods


	/// <summary>
	/// Retrive the loot and add to base on "type,id and quantity"
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="itemID">Item I.</param>
	/// <param name="quan">Quan.</param>
	public void AddItem (JMItemInfo item)
	{
		//BaseItem newItem = ItemDatabase.Instance.GetItem(item.charID, (int) item.item.Base_Item_Type);

		/*
		GameObject newGameObject = Instantiate(lootPrefab);
		newGameObject.transform.localScale = Vector3.one;
		newGameObject.GetComponent<JMItemInfo>().item = newItem;
		newGameObject.GetComponent<JMItemInfo>().itemQuantity = 1;
		*/

		JMItemInfo tempItem = item;

		if(!AddItemFromMenu(tempItem))
		{
			Debug.Log("Ay yo, HOL UP!");
		}

	}

	/// <summary>
	/// Updates the selecting item by left click. 
	/// If currently another item then deselect that.
	/// If detail block is deactive then active that. 
	/// Then send information to detailblock.
	/// </summary>
	/// <param name="selectedItem">Selected item.</param>
	public void UpdateSelectingItem(GameObject selectedItem)
	{
		// if an item was seleted. delesect it.
		if (currentItem != null) {
			currentItem.Deselected();
		}

		//Check if detail canvas is on, if not turn it on.
		if (!detailBlockPanel.gameObject.activeSelf)
		{
			detailBlockPanel.gameObject.SetActive(true);
		}

		// put detail of the item to detail block
		currentItem = selectedItem.GetComponent<JMInventorySlot>();

		detailBlockPanel.GetComponent<DetailBlock>().UpdateItemDetail(currentItem.Inventory_Item);
	}

	/// <summary>
	/// Check if inventory is not full then add item to iventory.
	/// If item is already exist then add the quantity only,else create new slot. 
	/// If item is not stackable, create new slot for each item.
	/// After that update the avaluable weight of the character and weight text.
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public bool AddItemFromMenu (JMItemInfo item)
	{
		Debug.Log ("GGG");

		// Checks if the inventory max carry is reached.
		if(!CheckInventoryWeight(item)) 
		{
			// Checks if the item is stackable.
			if(item.Item_Info.Is_Stackable) 
			{
				//Checks if the item exists in the character's inventory.
				if(itemSlots.Exists(o => o.Inventory_Item.Item_Info.Item_ID == item.Item_Info.Item_ID))
				{
					//Obtains the item index.
					int tempIndex = itemSlots.FindIndex(o => o.Inventory_Item.Item_Info.Item_ID == item.Item_Info.Item_ID);

					//Updates the inventory quantity.
					itemSlots[tempIndex].Inventory_Item.Item_Qty += item.Item_Qty;

					//Update the GUI
					itemSlots[tempIndex].ItemQuantity = itemSlots[tempIndex].Inventory_Item.Item_Qty;
				} 
				else 
				{
					CreateSlot(item);		
				}
			} 
			else 
			{
				CreateSlot(item);	
			}

			characterCurrentWeight -= item.Item_Info.Item_Weight*item.Item_Qty;

			//UNCOMMENT LATER
			//SquadManager.Instance.focusedUnit.GetComponent<JMCharacterInventory>().AddItem(item);
		
			return true;
		}

		// Since the item has been "picked up" by the player, 
		// The item GameObject is destroyed from the game scene.
		// The value of canAddItem shall determine that.
		return false;
	}

	/// <summary>
	/// If itemOptionPanel is deactivate then activate.
	/// Then call PopulateOption fnc in ItemOptionPanel with parameter is slotbeing select by right click.
	/// </summary>
	/// <param name="slot">Slot.</param>
	public void ItemOptionWindow(GameObject slot)
	{
		if (!itemOptionPanel.gameObject.activeSelf) 
		{
			itemOptionPanel.gameObject.SetActive(true);
		}
		itemOptionPanel.GetComponent<ItemOption>().PopulateOption(slot);
	}

	/// <summary>
	/// Drops the selected item. with ammount.
	/// </summary>
	/// <param name="drop">If set to <c>true</c> drop.</param>
	/// <param name="ammount">Ammount.</param>
	public void DropSelectedItem(bool drop, int ammount)
	{
		Debug.Log(itemSlots.FindIndex(o => o == rightClickItem));
		SquadManager.Instance.focusedUnit.GetComponent<JMCharacterInventory>().RemoveItem(itemSlots.Find(o => o == rightClickItem).Inventory_Item);
		DropItem(itemSlots.FindIndex(o => o == rightClickItem)   ,ammount,drop);
	}

	/// <summary>
	/// Drops or remove the item, base on "drop" variable.
	/// update the weightText and weight of character inventory
	/// If drop item, generate a new loot. 
	/// If ammount is > 0, change the quantity
	/// 	else remove item.
	/// Update quicklist.
	/// </summary>
	/// <param name="slotIndex">Slot index.</param>
	/// <param name="amt">Amt.</param>
	/// <param name="drop">If set to <c>true</c> drop.</param>
	public void DropItem(int slotIndex, int amt, bool drop)
	{
		//Declaring local variables
		GameObject temp;

		// since player decide to remove or drop then update the ammount.
		itemSlots[slotIndex].Inventory_Item.Item_Qty -= amt;

		// add the weight back equal to ammount of item time weight of item.
		characterCurrentWeight += itemSlots[slotIndex].Inventory_Item.Item_Info.Item_Weight * amt;
		UpdateWeightText();

		// if player choose to drop instead of remove then instatiate the item
		// WE need to update this function to 3D model instead of the icon of the item.
		if(drop)
		{
			GameObject copy;
			copy = Instantiate(lootPrefab);

			copy.GetComponent<ILootable>().CopyItemInfo(itemSlots[slotIndex].Inventory_Item);
			copy.gameObject.transform.parent = null;
			copy.gameObject.transform.localScale = new Vector3(10, 10, 10);
			copy.gameObject.GetComponent<Rigidbody>().useGravity = true;
			copy.gameObject.GetComponent<Rigidbody>().isKinematic = false;
			copy.gameObject.transform.position = new Vector3(playerReference.transform.position.x,
				playerReference.transform.position.y + 10.0f,
				playerReference.transform.position.z);
		}


		//Check if still have item of same type in the inventory
		if(itemSlots[slotIndex].Inventory_Item.Item_Qty > 0)
		{
			itemSlots[slotIndex].ItemQuantity= itemSlots[slotIndex].Inventory_Item.Item_Qty;
			//UpdateQuickItemSlot(slotIndex);
		}
		// if item is not enough then remove the slot.
		else
		{
			//UpdateQuickItemSlot(slotIndex);
			temp = itemSlots[slotIndex].gameObject;
			itemSlots.RemoveAt(slotIndex);
			Destroy(temp.gameObject);
		}
	}

	#endregion

	#region Private Methods

	//Checks if item weight is more than player weight.
	/// <summary>
	/// If the inventory currentWeight is avaliable to add more item. then return true,
	/// Else return false.
	/// </summary>
	/// <returns><c>true</c>, if inventory weight was checked, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	private bool CheckInventoryWeight(JMItemInfo item)
	{
		if(characterCurrentWeight - (item.Item_Info.Item_Weight * item.Item_Qty) <= 0)
		{
			return true;
		}
		return false;
	}

	//This function is used to create 'slots' in the Inventory
	//and store its respective Item Information.
	/// <summary>
	/// Instantiate slot then add information to the slot.
	/// Turn off the rigit body and deactivate the sprite of the item.
	/// Set parent of slot to inventory panel.
	/// Set sprite to be visible and update quantity of item
	/// </summary>
	/// <param name="newItem">New item.</param>
	private void CreateSlot(JMItemInfo newItem)
	{
		// create item slot. and put information of new item into the slot.
		GameObject slot = Instantiate(itemContainer);
		//GameObject item = Instantiate(invSlotPrefab);

		slot.transform.SetParent (transform);
		//item.transform.SetParent (slot.transform);
		//item.transform.position = Vector2.zero;
		//item.GetComponent<JMInventorySlot>().InitItemSlot(newItem);
		slot.transform.GetChild(0).GetComponent<JMInventorySlot>().InitItemSlot(newItem);

		if (newItem.Item_Info.Base_Item_Type == BaseItemType.EQUIPMENT)
		{
			//	item.GetComponent<JMInventorySlot> ().equipmentPart = (int)((EquipmentItem)newItem.item).Equipment_Type;
		}

		containers.Add(slot);

		/*
		tempSlot.GetComponent<JMInventorySlot>().Inventory_Item = newItem;

		//newItem.item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		//newItem.item.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		// Set parent newslot to this inventory gameobject, reconfigure the sprite and info.
		tempSlot.transform.SetParent(transform);
		tempSlot.GetComponent<JMInventorySlot>().Sprite_GUI.sprite = tempSlot.GetComponent<InventorySlot>().Inventory_Item.Item_Object.Item_Sprite;
		tempSlot.GetComponent<JMInventorySlot>().Sprite_GUI.color = new Vector4(255, 255, 255, 255);
		tempSlot.GetComponent<JMInventorySlot>().ItemQuantity = tempSlot.GetComponent<InventorySlot>().Inventory_Item.Item_Qty;
		*/

		// add new slot to slot list to controll.
		//itemSlots.Add(tempSlot.GetComponent<JMInventorySlot>());

		/*
		if(newItem.item.Base_Item_Type == BaseItemType.EQUIPMENT) 
		{
			item.GetComponent<JMInventorySlot> ().equipmentPart = (int)((EquipmentItem)newItem.item).Equipment_Type;
			//tempSlot.GetComponent<JMInventorySlot>().equipmentPart = (int)((EquipmentItem)newItem.item).Equipment_Type;
		}
		*/
	}

	/// <summary>
	/// Updates the weight text with current weight and max weight.
	/// </summary>
	private void UpdateWeightText(){
		weightText.text = "Weight : " + characterCurrentWeight.ToString() + " / " +characterMaxWeight.ToString();
	}

	private void CreateItemContainer ()
	{
		containers = new List<GameObject> ();

		for (int i = 0; i < playerReference.GetComponent<JMCharacterInventory> ().List_Of_Item.Count; i++) 
		{
			GameObject tmp = Instantiate (itemContainer);

			tmp.transform.SetParent (transform);
		}
	}

	#endregion

	#region Helper Classes/Structs

	#endregion

}
