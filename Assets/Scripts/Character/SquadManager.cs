﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SquadManager : MonoBehaviour {

	#region Member
	[SerializeField]
	List<BasePlayerCharacter> playerCharacterList;
	public BasePlayerCharacter focusedUnit;
	GamePlayCamera mainCam;
	public Transform spawnPoint;
	public GameObject playerCharacter;

	#endregion

	#region Built-in Method

	private static SquadManager _instance;
	public static SquadManager Instance
	{
		get { return _instance; }
	}

	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		
		mainCam = FindObjectOfType<GamePlayCamera>();
		SpawnUnit();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.Tab)) 
		{
			SwitchFocusCharacter();		
		}
		if (focusedUnit != null) 
		{
			focusedUnit.MoveThisUnit(Input.GetAxis("MoveHorizontal"));
		}
		if (Input.GetKeyUp(KeyCode.P)) 
		{
			SpawnUnit();
		}
	}

	#endregion

	#region Main Method
	void SwitchFocusCharacter(){
		int i = playerCharacterList.IndexOf (focusedUnit);
		if (i == playerCharacterList.Count-1) {
			focusedUnit = playerCharacterList[0];
		}
		else {
			focusedUnit = playerCharacterList[++i];
		}
		FocusCharacterChanged();
	}

	void FocusCharacterChanged(){
		mainCam.ChangeFocusUnit(focusedUnit.transform);
	}

	void SpawnUnit(){
		if (UnitDataBase.Instance.NumberOfUnit() == playerCharacterList.Count) {
			Debug.Log("You got max unit");
			return;
		}
		GameObject tempchar =  Instantiate(playerCharacter,spawnPoint.position,spawnPoint.rotation) as GameObject;
		tempchar.GetComponent<BasePlayerCharacter>().Init(playerCharacterList.Count);
		playerCharacterList.Add(tempchar.GetComponent<BasePlayerCharacter>());
		SwitchFocusCharacter();
		FocusCharacterChanged();
		tempchar.GetComponent<BasePlayerCharacter>().GearOn(((EquipmentItem)ItemDatabase.Instance.GetItem(0,0)).Equipment_Stats);
	}
	#endregion
}
