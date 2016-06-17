using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentSet :MonoBehaviour {

	#region Data Members
	public List<Transform> listMesh;
	public EquipmentItem[] listEquipment;
	private EquipmentItem head;
	private EquipmentItem arm;
	private EquipmentItem hand;
	private EquipmentItem torso;
	private EquipmentItem leg ;
	private EquipmentItem feet;
	private EquipmentItem leftHand;
	private EquipmentItem rightHand;
	private BasePlayerCharacter playerCharacter;
	public MenuManager charBlock;

	#endregion

	#region Setters & Getters

	#endregion

	void Start(){
		playerCharacter = (BasePlayerCharacter)this.GetComponent<PlayerGuardian>();
		for (int i = 0; i < 6; i++) {
			listMesh.Add(transform.GetChild(i+1));
		}
		charBlock = FindObjectOfType<MenuManager>();
		listEquipment = new EquipmentItem[8];
	
	}

	#region Public Methods

	/// <summary>
	/// Equips the armor. 
	/// 0:head,1;arm,2:hand,3:torso,4:leg,5:feet
	/// </summary>
	/// <param name="newItem">New item.</param>
	public bool EquipArmor(EquipmentItem newItem)
	{
		bool result = false;
		int equipmentType = (int)newItem.Equipment_Type;
	
		//Debug.Log(listEquipment[equipmentType]);
		// remove stats in player base on old item, And retrive the equipment if already equip.
		if (listEquipment[equipmentType] != null) {
			result = true;
			playerCharacter.GearOff(listEquipment[equipmentType].Equipment_Stats);	
		}
		//Update mesh of the model, need to add more later to update the material also.
		listMesh[3].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
		// replace the equipment in the list with new one.
		listEquipment[equipmentType] = newItem;
		playerCharacter.GearOn(newItem.Equipment_Stats);
		charBlock.GetComponent<MenuManager>().UpdateCharacterBlock();

		return result;
	}

	public void EquipWeapon (EquipmentItem newItem){
		
	}
	#endregion

	#region Private Methods

	#endregion

}
