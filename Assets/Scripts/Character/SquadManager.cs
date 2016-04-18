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
	public GameObject playerCharacter;
	public GameObject commandContainer;

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
	}

	// Update is called once per frame
	void Update () 
	{
//		if (Input.GetKeyUp(KeyCode.Tab)) 
//		{
//			SwitchFocusCharacter();		
//		}
//		if (focusedUnit != null) 
//		{
//			focusedUnit.MoveThisUnit(Input.GetAxis("MoveHorizontal"));
//		}
//		if (Input.GetKeyUp(KeyCode.P)) 
//		{
//			SpawnUnit();
//		}
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
		focusedUnit.GetComponent<CharacterInventory>().RepopulateInventory();
	}
		
	public void SpawnUnit()
	{
		if (UnitDataBase.Instance.NumberOfUnit() == playerCharacterList.Count) 
		{
			
			return;
		}

		GameObject tempchar =  Instantiate(playerCharacter,spawnPoint.position,spawnPoint.rotation) as GameObject;
		tempchar.GetComponent<BasePlayerCharacter>().Init(playerCharacterList.Count);
		tempchar.GetComponent<CharacterInventory>().charID = playerCharacterList.Count;
		playerCharacterList.Add(tempchar.GetComponent<BasePlayerCharacter>());

		//FocusCharacterChanged();
		tempchar.GetComponent<BasePlayerCharacter>().GearOn(((EquipmentItem)ItemDatabase.Instance.GetItem(0,0)).Equipment_Stats);
		SwitchFocusCharacter();
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
