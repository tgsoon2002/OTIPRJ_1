using UnityEngine;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using Items_And_Inventory;

public class InventoryDatabase : MonoBehaviour {

	#region Data Members
	private JsonData inventoryJson;

    private List<ItemInfo> inventoryDB;

	public static InventoryDatabase _instance;
	#endregion

	#region Getters/Setters



	#endregion

	#region Built-in Unity Methods

	public static InventoryDatabase Instance
	{
		get {
			return _instance;}
	}

	// Use this for initialization when awake
	void Awake(){
		
		_instance = this;
        inventoryDB = new List<ItemInfo>();
		ConstructDatabase();
	
	
	}

	// Use this for initialization
	void Start () {
		
	}

	#endregion

	#region Public Methods

	public void GetInventoryItemForCharacter (List<ItemInfo> newChar, int charID){
		newChar.Clear();
		foreach (var item in inventoryDB) {
			if (item.Owner_ID == charID) {
				newChar.Add(item);
			}
		}

	}

    public List<ItemInfo> GetInventoryItemForCharacter (int charID){
		
        List<ItemInfo> tempList = new List<ItemInfo>();
		foreach (var item in inventoryDB) {
            if (item.Owner_ID == charID) {
				tempList.Add(item);
			}
		}
		return tempList;	
//	    public List<ItemInventory> GetInventory (int charIndex){
//		List<ItemInventory> tempinventory = new List<ItemInventory>();
//		List<ItemSlot> temp = inventoryDB.FindAll(x=>x.characterID == charIndex);
//		for (int i = 0; i < temp.Count; i++) {
//			tempinventory.Add(new ItemInventory(ItemDatabase.Instance.GetItem(temp[i].itemID,temp[i].itemType),temp[i].quantity ));
//		}
//		return tempinventory;
	}

	/// <summary>
	/// Saves the item in inventory to item list in itemDatabase GameObject.
	/// If item already exist then update the quantity
	/// </summary>
	/// <param name="charID">Char I.</param>
	/// <param name="newItemID">New item I.</param>
	/// <param name="newItemType">New item type.</param>
	/// <param name="quan">Quan.</param>
    public void SaveInventory (ItemInfo newItem)
    {
//        int charID, int newItemID, int newItemType, int quan,string uID){
//        if (newItem.Item_Info.IsStackable)
//        {
//            
//        }
//        int tempPos = inventoryDB.FindIndex(o => o.Owner_ID == charID && o.Item_ID == newItemID && o.Item_Type == newItemType);
//        if (tempPos != -1) {
//            inventoryDB[tempPos].Item_Quantity = quan;
//		}
//		else {
//            BaseItem tempItem = ItemDatabase.Instance.GetItem(newItemID, newItemType);
//            inventoryDB.Add (new ItemInfo(charID,tempItem,quan,uID));
//               // charID,newItemID,quan,newItemType));	
//		}
	}

	/// <summary>
	/// Saves the data from ItemSlot List to Json database file.
	/// </summary>
	public void SaveDatabase (){
//		for (int i = 0; i < inventoryDB.Count; i++) {
//            ItemInfo tempJsonData = new ItemInfo();
//			tempJsonData = inventoryDB[i];
//            Debug.Log("Need to redo this");
//			//Convert Itemslot Type to string by using JsonMapper.ToJson
//			string inputString = JsonMapper.ToJson(tempJsonData);
//			//Write string to Json File.
//			File.WriteAllText(Application.dataPath + "/StreamingAssets/InventoryDB.json",inputString);
//		}
	}


	#endregion

	#region Private Methods
	void ConstructDatabase (){
		
		//Construct data for equipment item
		inventoryJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/InventoryDB.json"));
		// loop through each item in equipmentData and generate info and add to list
		for (int i = 0; i < inventoryJson.Count; i++) 
        {
            BaseItem temp = ItemDatabase.Instance.GetItem((int)inventoryJson[i]["itemIndex"], (int)inventoryJson[i]["itemType"]);

			inventoryDB.Add(
                new ItemInfo(
					(int)inventoryJson[i]["charIndex"],
                    temp,					
					(int)inventoryJson[i]["quantity"],
                    inventoryJson[i]["uID"].ToString(),
                    (int) inventoryJson[i]["gridIndex"],
					(int) inventoryJson[i]["quickBarIndex"],
					(bool) inventoryJson[i]["equiped"]
				)

			);

		}
	}


	#endregion


}

/*
public class ItemSlot
{
	public int characterID;
	public int quantity;
	public int itemID;
	public int itemType;
	/// <summary>
	/// constructor with char index, item index, quantity, type of item
	/// </summary>
	/// <param name="charID">Char I.</param>
	/// <param name="itemID">Item I.</param>
	/// <param name="quan">Quan.</param>
	/// <param name="type">Type.</param>
	public ItemSlot(int charID, int itemIndex, int quan, int type){
		characterID = charID;
		itemID = itemIndex;
		quantity = quan;
		itemType = type;
	}
	public ItemSlot(){}
}
*/

public class EquipmentStat{
	public int equipmentID;
	public List<float> stats;
	public List<float> attributes;
	public EquipmentStat(int index)
	{
		equipmentID = index;
		stats = new  List<float>();
		attributes = new  List<float>();
	}

	public void UpdateStats( List<float> newStat){
		stats = newStat;
	}

	public void UpdateAttr(  List<float> newAttribute)
	{
		attributes = newAttribute	;
	}
	public EquipmentStat(){}
}
