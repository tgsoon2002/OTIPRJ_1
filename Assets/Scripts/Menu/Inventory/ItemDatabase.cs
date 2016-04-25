using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public class ItemDatabase : MonoBehaviour {

	#region Data Members
	//private List<Item> database = new List<Item>();
	private List<EquipmentItem> equipmentDB = new List<EquipmentItem>();
	private List<ConsumableItem> consumableDB = new List<ConsumableItem>();
	private List<NonUseableItem> nonConsumDB = new List<NonUseableItem>();
	private JsonData equipmentData;
	private JsonData consumableData;
	private JsonData nonUsableData;
	private JsonData equiStatJson;
	private Transform armorSetModel;
	static ItemDatabase _instance;
	public Texture2D texture ;

	private List<EquipmentStat> equipStatDB;
	List<float> tempStatList;
	List<float> tempAttriList;
	//	public int itemCount; // Use this to debug number of item in the list
	#endregion

	#region Setters & Getters
	public static ItemDatabase Instance
	{
		get {return _instance;}
	}
	#endregion

	#region Built-in Unity Methods
	void Awake(){
		_instance = this;
		equipStatDB = new List<EquipmentStat>();

		ConstructEquipmentStats();
		ConstructItemDatabase()	;

	}
	#endregion

	#region Public Methods
	/// <summary>
	/// Gets the item.0:equipment,1:consumable,2:noncomsumalbe
	/// </summary>
	/// <returns>The item.</returns>
	/// <param name="index">Index.</param>
	/// <param name="type">Type.</param>
	public BaseItem GetItem(int index, int type)
	{
		BaseItem temp = equipmentDB.Find(x => x.Item_ID == index);
		switch(type)	{
		case 2:
			return nonConsumDB.Find(x => x.Item_ID == index);
		
		case 1:
			return consumableDB.Find(x => x.Item_ID == index);
		
		default:
			break;
		}
		return temp;
	}
	public EquipmentStat GetEquipStat (int index){
		return equipStatDB.Find(o=>o.equipmentID == index);
	}

//	public BaseItem PeekItem(int index, int type)
//	{
//		BaseItem temp = new ;
//		if(equipmentDB.Exists(x => x.Item_ID == index))
//			return equipmentDB.Find(x => x.Item_ID == index);
//		if(nonConsumDB.Exists(x => x.Item_ID == index))
//			return nonConsumDB.Find(x => x.Item_ID == index);
//		if(consumableDB.Exists(x => x.Item_ID == index))
//			return consumableDB.Find(x => x.Item_ID == index);
//	}

	#endregion

	#region Private Methods
	void  ConstructItemDatabase()
    {
		//Construct data for equipment item
		equipmentData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/EquipmentItem.json"));

		for (int i = 0; i < equipmentData.Count; i++)
        {
			equipmentDB.Add(
				new EquipmentItem(GetMesh(equipmentData[i]["modelSet"].ToString(),(EquipmentPart)(int)equipmentData[i]["BodyPart"]),
					(EquipmentPart)(int)equipmentData[i]["BodyPart"],
					(int)equipmentData[i]["index"],
					equipmentData[i]["name"].ToString(),
					equipmentData[i]["desc"].ToString(),
					GetSprite("equipmentSheet",(int)equipmentData[i]["index"]),
					(int)equipmentData[i]["price"],
					BaseItemType.EQUIPMENT,
					(bool)equipmentData[i]["stackable"],
                    (int)equipmentData[i]["weight"]
				));
			
			equipmentDB[i].Equipment_Stats = equipStatDB.Find(o => o.equipmentID == (int)equipmentData[i]["index"]);
//			Debug.Log("First Value of First equipment" + equipmentDB[i].Equipment_Stats.stats[0]);
			}

		// construct data for Consumable item
		consumableData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/ConsumableItem.json"));
		//ItemType _type, float range, bool craft, int id, string name, string description, int price, BaseItemType type, bool isStack
		for (int i = 0; i < consumableData.Count; i++) {
			consumableDB.Add(
				new ConsumableItem(  (ItemType)(int)consumableData[i]["consumType"],
					0.6f,
					(bool)consumableData[i]["craftable"],
					(int)consumableData[i]["index"],
					consumableData[i]["name"].ToString(),
					consumableData[i]["desc"].ToString(),
					GetSprite("consumableSheet",(int)consumableData[i]["index"]),
					(int)consumableData[i]["price"],
					BaseItemType.CONSUMABLE,
					(bool)consumableData[i]["stackable"],
                    (int)consumableData[i]["weight"]
				));
			}


		// construct data for NonConsumable item
		nonUsableData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/NonconsumItem.json"));
		//bool craft, int id, string name, string description, int price, BaseItemType type, bool isStack
		for (int i = 0; i < nonUsableData.Count; i++) {
			nonConsumDB.Add(
				new NonUseableItem(  (bool)nonUsableData[i]["craftable"],
					
					(int)nonUsableData[i]["index"],
					nonUsableData[i]["name"].ToString(),
					nonUsableData[i]["desc"].ToString(),
					GetSprite("nonUsableSheet",(int)nonUsableData[i]["index"]),
					(int)nonUsableData[i]["price"],
					BaseItemType.NON_CONSUMABLE,
					(bool)nonUsableData[i]["stackable"],
                    (int)nonUsableData[i]["weight"]
				));
		}



	}

	private void ConstructEquipmentStats (){
		equiStatJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/EquipmentStats.json"));
		// loop through each item in equipmentData and generate info and add to list
		for (int i = 0; i < equiStatJson.Count; i++) {
			equipStatDB.Add(new EquipmentStat( (int)equiStatJson[i]["equipIndex"]));

			tempStatList = new List<float>();
			for (int j = 0; j < 5; j++) 
			{
				tempStatList.Add((float)((double)equiStatJson[i]["stat"][j]));
			}
			equipStatDB[i].UpdateStats(tempStatList);

			tempAttriList= new List<float>();
			for (int j = 0; j < 12; j++) 
			{
				tempAttriList.Add((float)((double)equiStatJson[i]["attribute"][j]));
			}
			equipStatDB[i].UpdateAttr(tempAttriList);
		}
	}

	/// <summary>
	/// Gets the mesh data from prefab.
	/// </summary>
	/// <returns>The mesh.</returns>
	/// <param name="setName">Set name.</param>
	/// <param name="part">Part.</param>
	Mesh GetMesh(string setName,EquipmentPart part){
		Mesh tempMesh = new Mesh();
		armorSetModel = Resources.Load<GameObject>(setName).transform;
		tempMesh = armorSetModel.FindChild(part.ToString()).GetComponent<SkinnedMeshRenderer>().sharedMesh;
		return (tempMesh);
	}

	Sprite GetSprite(string sheetName, int spriteID){
		Sprite[] spriteSheet = Resources.LoadAll<Sprite>("Sprites/" + sheetName);
		return spriteSheet[spriteID];
	}
	#endregion
}