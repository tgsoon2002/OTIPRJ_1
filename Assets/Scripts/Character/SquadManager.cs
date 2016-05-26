using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SquadManager : MonoBehaviour {

	#region Member
	[SerializeField]
	List<BasePlayerCharacter> playerCharacterList;
	BasePlayerCharacter focusedUnit;
	GamePlayCamera mainCam;
	public Transform spawnPoint;
	public GameObject playerCharacter;
	public CharacterSkillDatabase skillDB;
	private static SquadManager _instance;
	#endregion
	#region Getters & Setters
	public BasePlayerCharacter FocusedUnit {
		get{ return  focusedUnit; }
		set{ focusedUnit = value; 
		FocusCharacterChanged();}
	}

	public List<BasePlayerCharacter> Player_Char_List 
	{
		get { return playerCharacterList; }
	}


	public static SquadManager Instance
	{
		get { return _instance; }
	}

	#endregion
	#region Built-in Method




	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
		mainCam = FindObjectOfType<GamePlayCamera>();

	}

	#endregion
	#region Public Method
	public void SwitchFocusCharacter(){
		int i = playerCharacterList.IndexOf (focusedUnit);
		if (i == playerCharacterList.Count-1) {
			focusedUnit = playerCharacterList[0];
		}
		else {
			focusedUnit = playerCharacterList[++i];
		}
		FocusCharacterChanged();
	}
	public void SwitchCurrent(BasePlayerCharacter newFocused)
	{
		focusedUnit = newFocused;
		FocusCharacterChanged();
	}

	public void _SaveSkillSet(){
		foreach (var item in playerCharacterList) {
			skillDB.SaveCharSkill(item.GetComponent<CharacterSkillSet>());
		}
	}

	public void _LoadSkillSet(){
		foreach (var item in playerCharacterList) {
			skillDB.LoadCharSkill(item.GetComponent<CharacterSkillSet>());
		}
	}

	public void _SpawnUnit() 
	{
		SpawnUnit();	
	}
	#endregion

	#region Private Methods




	void FocusCharacterChanged(){
		mainCam.ChangeFocusUnit(focusedUnit.transform);
		Commands.Instance.focusedUnit = focusedUnit;
		if (MenuManager.Instance.CurrentMenu == 0) {
			focusedUnit.GetComponent<CharacterInventory>().RepopulateInventory();	
			CharacterBlock.Instance.UpdateChar();
		}
		else if (MenuManager.Instance.CurrentMenu == 2) {
			SkillGridManager.Instance.LoadSkillMap();
		}


	}

	void SpawnUnit(){
		//check the number of unit in squad.
		if (UnitDataBase.Instance.NumberOfUnit() == playerCharacterList.Count) {
			return;
		}
		//create from prefab if allow.
		GameObject tempchar =  Instantiate(playerCharacter,spawnPoint.position,spawnPoint.rotation) as GameObject;
		// set value :BasePlayerCharacter,Inventory, (later: skill map)
		tempchar.GetComponent<BasePlayerCharacter>().Init(playerCharacterList.Count);
		tempchar.GetComponent<CharacterInventory>().charID = tempchar.GetComponent<BasePlayerCharacter>().charID;
		tempchar.GetComponent<CharacterSkillSet>().charID = tempchar.GetComponent<BasePlayerCharacter>().charID;
		skillDB.LoadCharSkill(tempchar.GetComponent<CharacterSkillSet>());
		Debug.Log("First skill unlocked state is : "+ tempchar.GetComponent<CharacterSkillSet>().unlocked[0]);
		// add character to the list
		playerCharacterList.Add(tempchar.GetComponent<BasePlayerCharacter>());

		//FocusCharacterChanged();
		tempchar.GetComponent<BasePlayerCharacter>().GearOn(((EquipmentItem)ItemDatabase.Instance.GetItem(0,0)).Equipment_Stats);
		SwitchFocusCharacter();
		if (Commands.Instance.focusedUnit == null) {
			Commands.Instance.focusedUnit = tempchar.GetComponent<BasePlayerCharacter>();
		}
	}
	#endregion
}
