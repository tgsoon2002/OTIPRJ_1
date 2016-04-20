using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterBlock : MonoBehaviour {

	#region Data Members

	public  List<Image> equipmentPartList;
	public Transform characterModel;
	public Transform characterEquipmentIcon;
	public Text characterStatsText;
	public Text characterAttributeText;
	private static CharacterBlock _instance;
	#endregion

	#region Setters & Getters
	public static CharacterBlock Instance {
		get{ return  _instance; }
		//set{ _instance = value; }
	}
	#endregion

	#region Built-in Unity Methods
	void Awake(){
		_instance = this;
	}
	// Use this for initialization
	void Start () {
		//pull information of all the bodypart to the list
		for (int i = 0; i < 8; i++) {
			equipmentPartList.Add( characterEquipmentIcon.GetChild(i).GetComponent<Image>());
		}
	}
		
	#endregion

	#region Public Methods
	/// <summary>
	/// Update character model mesh, missing code for update item icon also.
	/// Then update Stats text and Attribute text
	/// The other missing part is text to show stat of the unit.
	/// </summary>
	public void UpdateChar (){
		//update the mesh model first
		Transform tempChar =  SquadManager.Instance.focusedUnit.transform;
		for (int i = 1; i < 6; i++) 
		{
			characterModel.GetChild(i).GetComponent<SkinnedMeshRenderer>().sharedMesh = tempChar.GetChild(i).GetComponent<SkinnedMeshRenderer>().sharedMesh;
		}
		//update the Equipment icon 
		for (int i = 0; i < 8; i++) {
			if (tempChar.GetComponent<EquipmentSet>().listEquipment != null) {
				EquipmentItem[] tempList = tempChar.GetComponent<EquipmentSet>().listEquipment;
				if (tempList[i] != null) {
					equipmentPartList[i].sprite = tempList[i].Item_Sprite;
				}
			}

			//equipmentPartList[i].sprite = tempChar.GetComponent<EquipmentSet>()
		}


		//Update stat text
		characterStatsText.text= "";
		for (int i = 0; i <5; i++) {
			characterStatsText.text += SquadManager.Instance.focusedUnit.CharacterStats.StatsName(i).ToString() +" :"+ SquadManager.Instance.focusedUnit.CharacterStats.StatValue(i).ToString() + "\n";
		}
		
		//Update attribute text
		characterAttributeText.text= "";
		for (int i = 0; i <12; i++) {
			characterAttributeText.text += SquadManager.Instance.focusedUnit.CharacterStats.AttributeName(i).ToString() +" :"+ SquadManager.Instance.focusedUnit.CharacterStats.AttributeValue(i).ToString() + "\n";
		}

	}

	#endregion

	#region Private Methods

	#endregion

}
