using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    #region Data Members

    public GameObject lootPrefab;
	public GameObject invSlotPrefab;
	public GameObject playerReference;
	public ItemOption itemOptionPanel;
	public AmmountDropOption ammountOptionPanel;
	public QuickBar quickItemBarPanel;
    public DetailBlock detailBlockPanel;

	public Text weightText;

	private InventorySlot currentItem;
	public InventorySlot rightClickItem;
    public List<InventorySlot> itemSlots;

	private int characterCurrentWeight;
	private int characterMaxWeight;

	public static Inventory _instance;

	#endregion

	#region Setters & Getters

	public static Inventory Instance 
	{
		get{ return  _instance; }

	}

    //Setter for characterWeight
    public int Max_Weight
    {
		set { characterMaxWeight = value; 
			UpdateWeightText();}
    }

	#endregion

	#region Built-In Unity Methods
	void Awake(){
		_instance = this;
	}
	// Use this for initialization
	void Start ()
    {
		
		ClearInventory(50);
        itemSlots = new List<InventorySlot>();
		UpdateWeightText();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Check if inventory is not full then add item to iventory.
	/// If item is already exist then add the quantity only,else create new slot. 
	/// If item is not stackable, create new slot for each item.
	/// After that update the avaluable weight of the character and weight text.
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public bool AddItem (ItemInfo item)
    {
		// Checks if the inventory max carry is reached.
		if(!CheckInventoryWeight(item)) 
		{
			
			// Checks if the item is stackable.
            if(item.Item_Object.Is_Stackable) 
			{
                //Checks if the item exists in the character's inventory.
				if(itemSlots.Exists(o => o.Inventory_Item.Item_Object.Item_ID == item.Item_Object.Item_ID))
                {
                    //Obtains the item index.
					int tempIndex = itemSlots.FindIndex(o => o.Inventory_Item.Item_Object.Item_ID == item.Item_Object.Item_ID);

                    //Updates the inventory quantity.
					itemSlots[tempIndex].Inventory_Item.Item_Qty += item.Item_Qty;

                    //Update the GUI
					itemSlots[tempIndex].ItemQuantity = itemSlots[tempIndex].Inventory_Item.Item_Qty;

                    //Check if Item is on Quick Item Bar
                    UpdateQuickItemSlot(tempIndex);
                   
                    //Destroy the item since we don't need it.
                    Destroy(item.gameObject);
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
			characterCurrentWeight -= item.Item_Object.Item_Weight*item.Item_Qty;
			SquadManager.Instance.focusedUnit.GetComponent<CharacterInventory>().AddItem(item);
			UpdateWeightText();


			return true;
		}

		// Since the item has been "picked up" by the player, 
		// The item GameObject is destroyed from the game scene.
		// The value of canAddItem shall determine that.
		return false;
	}
		
	/// <summary>
	/// Retrive the loot and add to base on "type,id and quantity"
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="itemID">Item I.</param>
	/// <param name="quan">Quan.</param>
	public void AddItem (int type, int itemID, int quan)
	{
		BaseItem newItem = ItemDatabase.Instance.GetItem(itemID, type);
		GameObject newGameObject = Instantiate(lootPrefab);
		newGameObject.transform.localScale = Vector3.one;
		newGameObject.GetComponent<ItemInfo>().Item_Object = newItem;
		newGameObject.GetComponent<ItemInfo>().Item_Qty = 1;
		if(!AddItem(newGameObject.GetComponent<ItemInfo>()))
		{
			Debug.Log("Ey yo, HOL UP!");
		}

	}

	/// <summary>
	/// Called by CharacterInventory after clear the inventory
	/// Called to create new item slot equivalent to each item in character inventory
	/// </summary>
	/// <param name="type">Type.</param>
	/// <param name="itemID">Item I.</param>
	/// <param name="quan">Quan.</param>
	public void PopulateInventoryFromCharacter(int type, int itemID, int quan)
	{
		BaseItem newItem = ItemDatabase.Instance.GetItem(itemID, type);
		GameObject newGameObject = Instantiate(lootPrefab);
		newGameObject.transform.localScale = Vector3.one;
		newGameObject.GetComponent<ItemInfo>().Item_Object = newItem;
		newGameObject.GetComponent<ItemInfo>().Item_Qty = 1;

		CreateSlot(newGameObject.GetComponent<ItemInfo>());	
		characterCurrentWeight -= ItemDatabase.Instance.GetItem(itemID,type).Item_Weight * quan;
		UpdateWeightText();
	}

	/// <summary>
	/// Reset max weight equal to new maxweight.
	/// Remove all itemin inventory, 
	/// Update the weigthText
	/// </summary>
	/// <param name="maxWeight">Max weight.</param>
	public void ClearInventory (int maxWeight){
		characterMaxWeight = maxWeight ;
		characterCurrentWeight = characterMaxWeight;
		foreach (var item in itemSlots) {
			GameObject temp = item.gameObject;
			Destroy(temp);
		}
		itemSlots.Clear();
		UpdateWeightText();
	}

	/// <summary>
	/// Drops the selected item. with ammount.
	/// </summary>
	/// <param name="drop">If set to <c>true</c> drop.</param>
	/// <param name="ammount">Ammount.</param>
	public void DropSelectedItem(bool drop, int ammount)
	{
		Debug.Log(itemSlots.FindIndex(o => o == rightClickItem));
		SquadManager.Instance.focusedUnit.GetComponent<CharacterInventory>().RemoveItem(itemSlots.Find(o => o == rightClickItem).Inventory_Item);
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
		characterCurrentWeight += itemSlots[slotIndex].Inventory_Item.Item_Object.Item_Weight * amt;
		UpdateWeightText();

		// if player choose to drop instead of remove then instatiate the item
		// WE need to update this function to 3D model instead of the icon of the item.
		if(drop)
		{
			GameObject copy;
			copy = Instantiate(lootPrefab);
			copy.GetComponent<ItemInfo>().Item_Object = itemSlots[slotIndex].Inventory_Item.Item_Object;
			copy.GetComponent<ItemInfo>().Item_Qty = amt;
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
			UpdateQuickItemSlot(slotIndex);
		}
		// if item is not enough then remove the slot.
		else
		{
			UpdateQuickItemSlot(slotIndex);
			temp = itemSlots[slotIndex].gameObject;
			itemSlots.RemoveAt(slotIndex);
			Destroy(temp.gameObject);
		}
    }
		
	// Allows a character to equip a specific item.
	// This need to work alot on.
	public void EquipItem()
    {
		if(SquadManager.Instance.focusedUnit.GetComponent<EquipmentSet>().EquipArmor(((EquipmentItem)rightClickItem.Inventory_Item.Item_Object))){
			RetriveItem(rightClickItem.equipmentPart);
		}

		rightClickItem.IsEquip = true;
	}

	public void RetriveItem(int part)
	{
		itemSlots.Find(o=>o.equipmentPart == part && o.IsEquip == true).IsEquip = false;
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
		if (!detailBlockPanel.gameObject.activeSelf) {
			detailBlockPanel.gameObject.SetActive(true);
		}

		// put detail of the item to detail block
		currentItem = selectedItem.GetComponent<InventorySlot>();
		BaseItem tempItem = currentItem.Inventory_Item.Item_Object;
		detailBlockPanel.UpdateDetail(tempItem.Item_ID,currentItem.Inventory_Item.Item_Qty,tempItem.Base_Item_Type);
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
		itemOptionPanel.PopulateOption(slot);
	}


	#endregion

	#region Private Methods
	/// <summary>
	/// Updates the weight text with current weight and max weight.
	/// </summary>
	private void UpdateWeightText(){
		weightText.text = "Weight : " + characterCurrentWeight.ToString() + " / " +characterMaxWeight.ToString();
	}

    //Checks if item weight is more than player weight.
	/// <summary>
	/// If the inventory currentWeight is avaliable to add more item. then return true,
	/// Else return false.
	/// </summary>
	/// <returns><c>true</c>, if inventory weight was checked, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
    private bool CheckInventoryWeight(ItemInfo item)
    {
        if(characterCurrentWeight - (item.Item_Object.Item_Weight * item.Item_Qty) <= 0)
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
    private void CreateSlot(ItemInfo newItem)
    {
		
		// create item slot. and put information of new item into the slot.
		GameObject tempSlot = Instantiate(invSlotPrefab);
		tempSlot.GetComponent<InventorySlot>().Inventory_Item = newItem;
		// set parent of item just add to the slot, and turn off rigid body, turn off sprite so no conflict.
        newItem.gameObject.transform.SetParent(tempSlot.transform);
        newItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        newItem.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		// Set parent newslot to this inventory gameobject, reconfigure the sprite and info.
        tempSlot.transform.SetParent(transform);
		tempSlot.GetComponent<InventorySlot>().Sprite_GUI.sprite = tempSlot.GetComponent<InventorySlot>().Inventory_Item.Item_Object.Item_Sprite;
        tempSlot.GetComponent<InventorySlot>().Sprite_GUI.color = new Vector4(255, 255, 255, 255);
		tempSlot.GetComponent<InventorySlot>().ItemQuantity = tempSlot.GetComponent<InventorySlot>().Inventory_Item.Item_Qty;
		// add new slot to slot list to controll.
        itemSlots.Add(tempSlot.GetComponent<InventorySlot>());
		if (newItem.Item_Object.Base_Item_Type == BaseItemType.EQUIPMENT) {
			tempSlot.GetComponent<InventorySlot>().equipmentPart = (int)((EquipmentItem)newItem.Item_Object).Equipment_Type;
		}
    }

	/// <summary>
	/// If Quicklist containt infomation of the item. 
	///  then Update that quickItem.
	/// </summary>
	/// <param name="index">Index.</param>
    private void UpdateQuickItemSlot(int index)
    {
		Debug.Log(quickItemBarPanel.CheckIfItemOnBar(itemSlots[index].Inventory_Item.Item_Object.Item_ID));
        //Check if Item is on Quick Item Bar
		if(quickItemBarPanel.CheckIfItemOnBar(itemSlots[index].Inventory_Item.Item_Object.Item_ID))
        {
			quickItemBarPanel.UpdateQuickItemSlot(itemSlots[index].Inventory_Item.Item_Object.Item_ID);
        }
    }

	#endregion
}
