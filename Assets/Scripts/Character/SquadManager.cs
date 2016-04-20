using UnityEngine;
using System;
using GameInputNameSpace;
using System.Collections;
using System.Collections.Generic;
public class SquadManager : MonoBehaviour 
{
	#region Member
	[SerializeField]
	List<BasePlayerCharacter> playerCharacterList;
	public BasePlayerCharacter focusedUnit;
	GamePlayCamera mainCam;
	public Transform spawnPoint;
	public GameObject playerPrefab;
	private Commands commands;

	#endregion

	#region Built-in Method

	private static SquadManager _instance;
	public static SquadManager Instance
	{
		get { return _instance; }
	}

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{		
		commands = gameObject.GetComponent<Commands>();
		mainCam = FindObjectOfType<GamePlayCamera>();
		SpawnUnit();
		SpawnUnit();
	}

	#endregion

	#region Main Method

	public void SwitchFocusCharacter()
	{

		
		int i = playerCharacterList.IndexOf (focusedUnit);

		if (i == playerCharacterList.Count-1)
		{
			focusedUnit = playerCharacterList[0];
		}
		else 
		{
			focusedUnit = playerCharacterList[++i];
		}
		FocusCharacterChanged();

//		if (playerCharacterList.Count > 1) {
//			Debug.Log("repopulate inventory for charID :" + playerCharacterList.IndexOf (focusedUnit).ToString());
//
//		}
	}

	void FocusCharacterChanged()
	{
		mainCam.ChangeFocusUnit(focusedUnit.transform);
		Commands.Instance.focusUnit = focusedUnit;
		focusedUnit.GetComponent<CharacterInventory>().RepopulateInventory();
		CharacterBlock.Instance.UpdateChar();
	}
		
	public void SpawnUnit()
	{
		if (UnitDataBase.Instance.NumberOfUnit() == playerCharacterList.Count) 
		{
			
			return;
		}

		GameObject tempchar =  Instantiate(playerPrefab,spawnPoint.position,spawnPoint.rotation) as GameObject;
		tempchar.GetComponent<BasePlayerCharacter>().Init(playerCharacterList.Count);
		tempchar.GetComponent<CharacterInventory>().charID = playerCharacterList.Count;
		playerCharacterList.Add(tempchar.GetComponent<BasePlayerCharacter>());

		//FocusCharacterChanged();
		tempchar.GetComponent<BasePlayerCharacter>().GearOn(((EquipmentItem)ItemDatabase.Instance.GetItem(0,0)).Equipment_Stats);
		SwitchFocusCharacter();
		if (Commands.Instance.focusUnit == null) {
			Commands.Instance.focusUnit = tempchar.GetComponent<BasePlayerCharacter>();
		}
	}

	#endregion

	#region Private Methods

	private void CharacterSwitch(int direction)
	{
		
	}

	private void OpenInventory()
	{
		
	}

	private void OpenSettings()
	{
		
	}

	#endregion
}
