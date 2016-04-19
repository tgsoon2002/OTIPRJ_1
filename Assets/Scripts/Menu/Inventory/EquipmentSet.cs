using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentSet :MonoBehaviour {

	#region Data Members
	public List<Transform> listMesh;
	public List<EquipmentItem> listEquipment;
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
		listEquipment = new List<EquipmentItem>();
		listEquipment.Capacity = 8;
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
		switch ((int)newItem.Equipment_Type) {
		case 0:
			// remove stats in player base on old item, And retrive the equipment if already equip.
			if (head != null) {
				result = true;
				playerCharacter.GearOff(head.Equipment_Stats);	
			}

			listMesh[2].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;


			head = newItem;
			break;
		case 1:
			if (arm != null) {
				result = true;
				playerCharacter.GearOff(arm.Equipment_Stats);	
			}
			listMesh[3].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
		
			arm = newItem;
			//Debug.Log("Equiping item for Arm" + newItem.Equipment_Stats.attributes[0]);
			break;
		case 2:
			if (hand != null) {
				result = true;
				playerCharacter.GearOff(hand.Equipment_Stats);	
			}
			listMesh[4].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;

			hand = newItem;
			break;
		case 3:
			if (torso != null) {
				result = true;
				playerCharacter.GearOff(torso.Equipment_Stats);	
			}
			listMesh[5].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;

			torso = newItem;
			break;
		case 4:
			if (leg != null) {
				result = true;
				playerCharacter.GearOff(leg.Equipment_Stats);	
			}
			listMesh[6].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
		
			leg = newItem;
			break;
		case 5:
			if (feet != null) {
				result = true;
				playerCharacter.GearOff(feet.Equipment_Stats);	
			}
			listMesh[7].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
	
			feet = newItem;
			break;
		default:
			Debug.Log("This is wrong type of equipment type");
			break;
		}
		// add stats to player base on new item.
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
