using UnityEngine;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class InventoryDatabase : MonoBehaviour {

	#region Data Members
	private JsonData inventoryJson;

	private List<ItemSlot> inventoryDB;

	public Inventory in_Ref;

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
		inventoryDB = new List<ItemSlot>();
		ConstructDatabase();
	
	
	}

	// Use this for initialization
	void Start () {
		
	}

	#endregion

	#region Public Methods

	public List<ItemSlot> GetInventoryItemForCharacter (int charID){
		List<ItemSlot> tempList = new List<ItemSlot>();
		foreach (var item in inventoryDB) {
			if (item.characterID == charID) {
				tempList.Add(item);
			}
		}
		return tempList;
	}
	//	public List<ItemInventory> GetInventory (int charIndex){
//		List<ItemInventory> tempinventory = new List<ItemInventory>();
//		List<ItemSlot> temp = inventoryDB.FindAll(x=>x.characterID == charIndex);
//		for (int i = 0; i < temp.Count; i++) {
//			tempinventory.Add(new ItemInventory(ItemDatabase.Instance.GetItem(temp[i].itemID,temp[i].itemType),temp[i].quantity ));
//		}
//		return tempinventory;
//	}

	/// <summary>
	/// Saves the item in inventory to item list in itemDatabase GameObject.
	/// If item already exist then update the quantity
	/// </summary>
	/// <param name="charID">Char I.</param>
	/// <param name="newItemID">New item I.</param>
	/// <param name="newItemType">New item type.</param>
	/// <param name="quan">Quan.</param>
	public void SaveInventory (int charID, int newItemID, int newItemType, int quan){
		if (inventoryDB.Exists(o => o.characterID == charID && o.itemID == newItemID && o.itemType == newItemType)) {
			inventoryDB.Find(o => o.characterID == charID && o.itemID == newItemID && o.itemType == newItemType).quantity = quan;
		}
		else {
			inventoryDB.Add (new ItemSlot(charID,newItemID,quan,newItemType));	
		}
	}

	/// <summary>
	/// Saves the data from ItemSlot List to Json database file.
	/// </summary>
	public void SaveDatabase (){
		for (int i = 0; i < inventoryDB.Count; i++) {
			ItemSlot tempJsonData = new ItemSlot();
			tempJsonData.characterID = inventoryDB[i].characterID;
			tempJsonData.itemID = inventoryDB[i].itemID;
			tempJsonData.quantity = inventoryDB[i].quantity;
			tempJsonData.itemType = inventoryDB[i].itemType;
			//Convert Itemslot Type to string by using JsonMapper.ToJson
			string inputString = JsonMapper.ToJson(tempJsonData);
			//Write string to Json File.
			File.WriteAllText(Application.dataPath + "/StreamingAssets/InventoryDB.json",inputString);
		}
	}


	#endregion

	#region Private Methods
	void ConstructDatabase (){
		
		//Construct data for equipment item
		inventoryJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/InventoryDB.json"));
		// loop through each item in equipmentData and generate info and add to list
		for (int i = 0; i < inventoryJson.Count; i++) {
			inventoryDB.Add(
				new ItemSlot(
					(int)inventoryJson[i]["charIndex"],
							(int)inventoryJson[i]["itemIndex"],
							(int)inventoryJson[i]["quantity"],
							(int)inventoryJson[i]["itemType"]
				)
			);
		}
	}


	#endregion


}

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
