using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class UnitDataBase : MonoBehaviour {

	#region Data Members
	[SerializeField]
	List<CharacterType> charList = new List<CharacterType>();
	private JsonData unitDatabase;
	private JsonData modifierDatabase;
	List<string> classTypeIndex ;
	public static UnitDataBase _instance;

	#endregion

	#region Getters/Setters

	public static UnitDataBase Instance
	{
		get {return _instance;}
	}

	#endregion

	#region UnitBuiltInMethod

	/// <summary>
	/// Awake this instance.
	/// Get data from JSON file, initial some temp Data member and construct database
	/// Create classTypeIndex to help set modifier for attribute later base on class.
	/// </summary>
	void Awake(){
		_instance = this;
		classTypeIndex = new List<string>();
		classTypeIndex.Add("Swordsman");
		classTypeIndex.Add("Guardian");
		classTypeIndex.Add("Alchemist");
		classTypeIndex.Add("Magician");
		classTypeIndex.Add("Rouge");
		classTypeIndex.Add("Archer");


		ConstructCharacterDatabase();
	}

	#endregion

	#region Helper Methods

	/// <summary>
	/// Constructs the character database.
	/// Go through for loop, creat temp stat and attribuet. then set modifier for attribute before create temp CharacterType 
	/// After got temp Character type, add to the charList.
	/// </summary>
	void ConstructCharacterDatabase(){
		unitDatabase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/CharacterDB.json"));
		modifierDatabase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Modifier.json"));
		for (int i = 0; i < unitDatabase.Count; i++) {
			List<CharStat> tempCharStat = new List<CharStat>();
			List<CharAttribute> tempCharAttri = new List<CharAttribute>();
			// set temporary stat value for unit from database
			//0: strength,1:agility,2:intelligent, 3:vitality,4:luck
			tempCharStat.Add(new CharStat("strength",(float)((double)unitDatabase[i]["strength"])));		//0 str
			tempCharStat.Add(new CharStat("agility",(float)((double)unitDatabase[i]["agility"])));			//1 agi
			tempCharStat.Add(new CharStat("intelligent",(float)((double)unitDatabase[i]["intelligent"])));	//2 int
			tempCharStat.Add(new CharStat("vitality",(float)((double)unitDatabase[i]["vitality"])));		//3 vit
			tempCharStat.Add(new CharStat("luck",(float)((double)unitDatabase[i]["luck"])));				//4 luk
			// set temporary stat attribute for unit from database
			//0:maxHP, 1:maxMP, 2:maxSP, 3:pAttack, 4:mAttack, 5:dodgRate, 
			//6:mvSpeed, 7:critChance, 8:regSp, 9:regMp, 10:pDef, 11:mDef, 
			//12:currentHP, 13:currentMP, 14:currentSP
			tempCharAttri.Add(new CharAttribute("MaxHP",(float)((double)unitDatabase[i]["maxHealhtPoint"]))); 			//0MaxHP
			tempCharAttri.Add(new CharAttribute("MaxMP",(float)((double)unitDatabase[i]["maxMagicPoint"])));			//1MaxMp
			tempCharAttri.Add(new CharAttribute("MaxSP",(float)((double)unitDatabase[i]["maxStaminaPoint"])));		//2MaxSp
			tempCharAttri.Add(new CharAttribute("PhyAtk",(float)((double)unitDatabase[i]["PAttack"])));						//3PAttack
			tempCharAttri.Add(new CharAttribute("MagAtk",(float)((double)unitDatabase[i]["MAttack"])));						//4MAtt
			tempCharAttri.Add(new CharAttribute("DodRate",(float)((double)unitDatabase[i]["dodgeRate"])));					//5DogRate
			tempCharAttri.Add(new CharAttribute("MvSpeed",(float)((double)unitDatabase[i]["mvSpeed"])));						//6MvSpd
			tempCharAttri.Add(new CharAttribute("CritCha",(float)((double)unitDatabase[i]["CritChance"])));					//7CritChance
			tempCharAttri.Add(new CharAttribute("SPReg",(float)((double)unitDatabase[i]["SPReg"])));							//8SpReg
			tempCharAttri.Add(new CharAttribute("MPReg",(float)((double)unitDatabase[i]["MPReg"])));							//9MpReg
			tempCharAttri.Add(new CharAttribute("PDef",(float)((double)unitDatabase[i]["PDef"])));								//10Pdef
			tempCharAttri.Add(new CharAttribute("MDef",(float)((double)unitDatabase[i]["MDef"])));								//11MDef
			tempCharAttri.Add(new CharAttribute("currentHealthPoint",(float)((double)unitDatabase[i]["currentHealthPoint"])));	//12CurrentHp
			tempCharAttri.Add(new CharAttribute("currentMagicPoint",(float)((double)unitDatabase[i]["currentMagicPoint"])));	//13CurrentMp
			tempCharAttri.Add(new CharAttribute("currentStaminaPoint",(float)((double)unitDatabase[i]["currentStaminaPoint"])));//14CurrentSp
			//check which class this unit is. Then call the right index for modifierinfo.
			int classIndex = classTypeIndex.IndexOf(unitDatabase[i]["charClass"].ToString());
			// set modifier for character attribute
			tempCharAttri[0].AddModifier((float)((double)modifierDatabase[classIndex]["StrHpMod"]),tempCharStat[0]);	//MaxHP
			tempCharAttri[0].AddModifier((float)((double)modifierDatabase[classIndex]["VitHpMod"]),tempCharStat[3]);	
			tempCharAttri[1].AddModifier((float)((double)modifierDatabase[classIndex]["IntMpMod"]),tempCharStat[2]);	//MaxMp
			tempCharAttri[2].AddModifier((float)((double)modifierDatabase[classIndex]["VitSpMod"]),tempCharStat[3]);	//MaxSp
			tempCharAttri[3].AddModifier((float)((double)modifierDatabase[classIndex]["StrAtkMod"]),tempCharStat[0]);	//PAtt
			tempCharAttri[3].AddModifier((float)((double)modifierDatabase[classIndex]["VitAtkMod"]),tempCharStat[3]);	
			tempCharAttri[3].AddModifier((float)((double)modifierDatabase[classIndex]["AgiAtkMod"]),tempCharStat[1]);	
			tempCharAttri[3].AddModifier((float)((double)modifierDatabase[classIndex]["LukAtkMod"]),tempCharStat[4]);	
			tempCharAttri[4].AddModifier((float)((double)modifierDatabase[classIndex]["IntMAtkMod"]),tempCharStat[2]);	//MAtk
			tempCharAttri[4].AddModifier((float)((double)modifierDatabase[classIndex]["LukMAtkMod"]),tempCharStat[4]);
			tempCharAttri[5].AddModifier((float)((double)modifierDatabase[classIndex]["AgiDodMod"]),tempCharStat[1]);	//DogRate
			tempCharAttri[5].AddModifier((float)((double)modifierDatabase[classIndex]["LukDodMod"]),tempCharStat[4]);
			tempCharAttri[6].AddModifier((float)((double)modifierDatabase[classIndex]["AgiSpdMod"]),tempCharStat[1]);	//MvSpd
			tempCharAttri[7].AddModifier((float)((double)modifierDatabase[classIndex]["AgiCritMod"]),tempCharStat[1]);	//CrtChance
			tempCharAttri[7].AddModifier((float)((double)modifierDatabase[classIndex]["LukCritMod"]),tempCharStat[4]);	
			tempCharAttri[8].AddModifier((float)((double)modifierDatabase[classIndex]["StrSpRegMod"]),tempCharStat[0]);	//SpReg
			tempCharAttri[8].AddModifier((float)((double)modifierDatabase[classIndex]["VitSpRegMod"]),tempCharStat[3]);	
			tempCharAttri[9].AddModifier((float)((double)modifierDatabase[classIndex]["IntMpRegMod"]),tempCharStat[2]);	//MpReg
			tempCharAttri[9].AddModifier((float)((double)modifierDatabase[classIndex]["LukMpRegMod"]),tempCharStat[4]);	
			tempCharAttri[10].AddModifier((float)((double)modifierDatabase[classIndex]["StrPdefMod"]),tempCharStat[0]);	//PDef
			tempCharAttri[10].AddModifier((float)((double)modifierDatabase[classIndex]["VitPdefMod"]),tempCharStat[3]);	
			tempCharAttri[11].AddModifier((float)((double)modifierDatabase[classIndex]["IntMdefMod"]),tempCharStat[2]);	//MDef
			tempCharAttri[11].AddModifier((float)((double)modifierDatabase[classIndex]["VitMdefMod"]),tempCharStat[3]);	
			// Recalculate the value of Attribute after link modifier
			for (int y = 0; y < 11; y++) 
			{
				tempCharAttri[y].AddBonus(0.0f);
			}
			//Create temporary character to with tempChar and tempAttri to add to list.
			CharacterType tempChar = 
				new CharacterType((int)unitDatabase[i]["charID"],
					unitDatabase[i]["charName"].ToString(),
								  unitDatabase[i]["charClass"].ToString(),
								  (int)unitDatabase[i]["charLevel"] , 
								  tempCharStat, 
								  tempCharAttri);
			// add tempChar to charList.
			charList.Add(tempChar);
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Public function return number of template Unit saved in database.
	/// </summary>
	/// <returns>The of unit.</returns>
	public int NumberOfUnit()
	{
		return charList.Count;
	}
	/// <summary>
	/// Sets the unit stat for the unit sent in. with the name
	/// </summary>
	/// <param name="currentUnit">Current unit.</param>
	/// <param name="index">Index.</param>
	public void SetUnitStat(UnitStats currentUnit,int index){
		currentUnit.ListStat = charList[index].unitStat;
		currentUnit.ListAttribute = charList[index].unitAttri;
	}
	/// <summary>
	/// Sets the unit info. set name, level of the unit. in future. we can set to spawn point, or saved location.
	/// </summary>
	/// <param name="currentUnit">Current unit.</param>
	/// <param name="index">Index.</param>
	public void SetUnitInfo(BasePlayerCharacter currentUnit,int index){
		currentUnit.charID = charList[index].charID;
		currentUnit.CharacterName = charList[index].charName;
		currentUnit.charLevel = charList[index].charLevel;
	}

	#endregion
}

public struct CharacterType
{
	public string charName;
	public string charClass;
	public int charLevel;
	public int charID;
	public List<CharStat> unitStat;
	public List<CharAttribute> unitAttri;
	public CharacterType(int newCharID, string name, string cClass, int level,List<CharStat> stat,List<CharAttribute> attri)
	{
		charID = newCharID;
		charClass = cClass;
		charName = name;
		charLevel = level;
		unitStat = stat;
		unitAttri = attri;
	}
}