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

	#endregion
	#region Getters & Setters
	public BasePlayerCharacter FocusedUnit {
		get{ return  focusedUnit; }
		set{ focusedUnit = value; 
		FocusCharacterChanged();}
	}
	#endregion
	#region Built-in Method

	private static SquadManager _instance;
	public static SquadManager Instance
	{
		get { return _instance; }
	}

	public List<BasePlayerCharacter> Player_Char_List 
	{
		get { return playerCharacterList; }
	}

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
		mainCam = FindObjectOfType<GamePlayCamera>();
		SpawnUnit();
		SpawnUnit();
	}

	#endregion

	#region Main Method

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



	void FocusCharacterChanged(){
		mainCam.ChangeFocusUnit(focusedUnit.transform);
		Commands.Instance.focusedUnit = focusedUnit;
		if (Inventory.Instance.enabled) {
			focusedUnit.GetComponent<CharacterInventory>().RepopulateInventory();	
			CharacterBlock.Instance.UpdateChar();
		}
		else if (SkillGridManager.Instance.enabled) {
			SkillGridManager.Instance.LoadSkillMap();
		}


	}

	void SpawnUnit(){
		if (UnitDataBase.Instance.NumberOfUnit() == playerCharacterList.Count) {
			
			return;
		}
		GameObject tempchar =  Instantiate(playerCharacter,spawnPoint.position,spawnPoint.rotation) as GameObject;
		tempchar.GetComponent<BasePlayerCharacter>().Init(playerCharacterList.Count);
		tempchar.GetComponent<CharacterInventory>().charID = playerCharacterList.Count;
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
