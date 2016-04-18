using UnityEngine;
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

<<<<<<< HEAD
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

=======
>>>>>>> 9b8f50be6b5b13f0d4cf57d65bee39fda6b2aa76
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
//		if (playerCharacterList.Count > 1) {
//			Debug.Log("repopulate inventory for charID :" + playerCharacterList.IndexOf (focusedUnit).ToString());
//
//		}
	}

	void FocusCharacterChanged(){
		mainCam.ChangeFocusUnit(focusedUnit.transform);
		focusedUnit.GetComponent<CharacterInventory>().RepopulateInventory();
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
		if (Commands.Instance.focusUnit == null) {
			Commands.Instance.focusUnit = tempchar.GetComponent<BasePlayerCharacter>();
		}
	}
	#endregion
}
