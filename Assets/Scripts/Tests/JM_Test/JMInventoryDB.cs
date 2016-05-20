using UnityEngine;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class JMInventoryDB : MonoBehaviour 
{
	#region Data Members

	private JsonData inventoryJson;
	private List<JMItemInfo> inventoryDB;
	private static JMInventoryDB _instance;
	public Inventory in_Ref;

	public struct TempItem
	{
		public int char_ID;
		public int item_ID;
		public int item_Qty;
		public int item_Type; 
	}

	#endregion

	#region Setters & Getters

	public static JMInventoryDB Instance
	{
		get { return _instance; }
	}

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization when awake
	void Awake()
	{
		_instance = this;
		inventoryDB = new List<JMItemInfo>();
		ConstructDatabase();
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Gets the inventory item for character.
	/// </summary>
	/// <returns>The inventory item for character.</returns>
	/// <param name="charID">Char I.</param>
	public List<JMItemInfo> GetInventoryItemForCharacter (int charID)
	{
		List<JMItemInfo> tempList = new List<JMItemInfo>();

		foreach (var itemObj in inventoryDB) 
		{
			if (itemObj.Item_Info.Item_ID == charID) 
			{
				tempList.Add(itemObj);
			}
		}

		return tempList;
	}

	/// <summary>
	/// Saves the item in inventory to item list in itemDatabase GameObject.
	/// If item already exist then update the quantity
	/// </summary>
	/// <param name="charID">Char I.</param>
	/// <param name="newItemID">New item I.</param>
	/// <param name="newItemType">New item type.</param>
	/// <param name="quan">Quan.</param>
	public void SaveInventory (int charID, int newItemID, int newItemType, int qty)
	{
		if (inventoryDB.Exists(o => o.Item_Info.Item_ID == charID && (int) o.Item_Info.Base_Item_Type == newItemType)) 
		{
			inventoryDB.Find(o => o.Item_Info.Item_ID == charID && (int) o.Item_Info.Base_Item_Type == newItemType).Item_Qty = qty;
		}
		else
		{
			inventoryDB.Add (new JMItemInfo(charID, newItemID, newItemType, qty));	
		}
	}

	/// <summary>
	/// Saves the data from ItemSlot List to Json database file.
	/// </summary>
	public void SaveDatabase ()
	{
		TempItem temp = new TempItem();

		for (int i = 0; i < inventoryDB.Count; i++) 
		{
			temp.char_ID = inventoryDB[i].Char_ID;
			temp.item_ID = inventoryDB[i].Item_Info.Item_ID;
			temp.item_Qty = inventoryDB[i].Item_Qty;
			temp.item_Type = (int) (inventoryDB[i].Item_Info.Base_Item_Type);

			//Convert Itemslot Type to string by using JsonMapper.ToJson
			string inputString = JsonMapper.ToJson(temp);

			//Write string to Json File.
			File.WriteAllText(Application.dataPath + "/StreamingAssets/InventoryDB.json",inputString);
		}
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Constructs the database.
	/// </summary>
	private void ConstructDatabase ()
	{
		//Construct data for equipment item
		inventoryJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/InventoryDB.json"));
		// loop through each item in equipmentData and generate info and add to list
		for (int i = 0; i < inventoryJson.Count; i++) 
		{
			inventoryDB.Add(new JMItemInfo((int)inventoryJson[i]["charIndex"],
										  (int)inventoryJson[i]["itemIndex"],
										  (int)inventoryJson[i]["quantity"],
										  (int)inventoryJson[i]["itemType"]));
		}
	}

	#endregion

	#region Helper Classes/Structs

	#endregion

}
