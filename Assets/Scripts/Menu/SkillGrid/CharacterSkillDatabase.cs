using UnityEngine;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class CharacterSkillDatabase : MonoBehaviour {

	#region Data Members
	private JsonData charSkillJson;
	private List<TempSkillSet> skillSetDB;
	int tempIndex = -1;
	#endregion

	#region Setters & Getters (empty)

	#endregion

	#region Built-in Unity Methods (Start)
	// Use this for initialization
	void Start () {
		skillSetDB = new List<TempSkillSet>();
		LoadFromDB();
	}

	// Update is called once per frame
	void Update () {

	}
	#endregion

	#region Public Methods
	public void SaveCharSkill(CharacterSkillSet newChar) 
	{
		Debug.Log(newChar.CharacterID);
		// Check if character already in the buffer.
		 tempIndex = skillSetDB.FindIndex(o => o.charID == newChar.CharacterID) ;
		if (tempIndex > -1){
			skillSetDB[tempIndex].skillMap = newChar.unlocked;
			skillSetDB[tempIndex].skillPointLeft = newChar.SkillPointAvalible;
		}else{
			TempSkillSet tempChar = new TempSkillSet();
			tempChar.charID = newChar.CharacterID;
			tempChar.skillMap = newChar.unlocked;
			tempChar.skillPointLeft = newChar.SkillPointAvalible;
			skillSetDB.Add(tempChar);
		}

	}


	public bool LoadCharSkill(CharacterSkillSet newChar) 
	{
		for (int i = 0; i < skillSetDB.Count; i++) {
			Debug.Log("ID in the list : " + skillSetDB[i].charID);
		}
		tempIndex = skillSetDB.FindIndex(o => o.charID == newChar.CharacterID) ;
		if (tempIndex > -1){
			newChar.unlocked = skillSetDB[tempIndex].skillMap ;
			newChar.SkillPointAvalible = skillSetDB[tempIndex].skillPointLeft;
			return true;
		}
		return false;
	}
	#endregion

	#region Private Methods
	public void SaveToDB (){
		StringBuilder sb = new StringBuilder();
		JsonWriter writer = new JsonWriter(sb);
		writer.WriteArrayStart();
		for (int i = 0; i < skillSetDB.Count; i++){
			writer.WriteObjectStart();
			writer.WritePropertyName("skillMap");
			writer.WriteArrayStart();
			for (int j = 0; j < GlobalVar.TOTALSKILL; j++) 
			{
				writer.Write(skillSetDB[i].skillMap[j]);
			}
			writer.WriteArrayEnd();
			writer.WritePropertyName("charID");
			writer.Write(skillSetDB[i].charID);
			writer.WritePropertyName("skillPointLeft");
			writer.Write(skillSetDB[i].skillPointLeft);
			writer.WriteObjectEnd();
		}
		writer.WriteArrayEnd();
		File.WriteAllText(Application.dataPath + "/StreamingAssets/SkillSetDB.json",sb.ToString());

	}

	void LoadFromDB(){
		charSkillJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/SkillSetDB.json"));
		Debug.Log(charSkillJson.Count);
		for (int i = 0; i < charSkillJson.Count; i++) {
			bool[] tempArray = new bool[GlobalVar.TOTALSKILL];
			for (int j = 0; j < GlobalVar.TOTALSKILL ; j++) 
			{
				tempArray[j] = (bool)charSkillJson[i]["skillMap"][j];
				//Debug.Log(charSkillData[i]["skillMap"]);
			} 

			skillSetDB.Add(	new TempSkillSet(
					tempArray,
				(int)charSkillJson[i]["charID"],
				(int)charSkillJson[i]["skillPointLeft"]
				));
		}

	}

	#endregion
}
class TempSkillSet
{
	public bool[] skillMap;
	public int charID;
	public int skillPointLeft;
	public TempSkillSet (){	}
	public TempSkillSet (bool[] newMap, int newCharID, int newPoint){	
		skillMap = newMap;
		charID = newCharID;
		skillPointLeft = newPoint;
	}
//	public TempSkillSet ( int charID, int newPoint){	
//		//skillMap = newMap;
//		charID = charID;
//		skillPointLeft = newPoint;
//	}

}


