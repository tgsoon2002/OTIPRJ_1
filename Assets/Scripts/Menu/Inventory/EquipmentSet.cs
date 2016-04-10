using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentSet :MonoBehaviour {

	#region Data Members
	public List<Transform> listMesh;
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

	}

	#region Public Methods

	/// <summary>
	/// Equips the armor. 
	/// 0:head,1;arm,2:hand,3:torso,4:leg,5:feet
	/// </summary>
	/// <param name="newItem">New item.</param>
	public void EquipArmor(EquipmentItem newItem)
	{

		switch ((int)newItem.Equipment_Type) {
		case 0:
			// remove stats in player base on old item
			if (head != null) {
				playerCharacter.GearOff(head.Equipment_Stats);	
			}
			listMesh[2].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
			playerCharacter.GearOn(newItem.Equipment_Stats);
			head = newItem;
			break;
		case 1:
			if (arm != null) {
				playerCharacter.GearOff(arm.Equipment_Stats);	
			}
			listMesh[3].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
			playerCharacter.GearOn(newItem.Equipment_Stats);
			arm = newItem;
			//Debug.Log("Equiping item for Arm" + newItem.Equipment_Stats.attributes[0]);
			break;
		case 2:
			if (hand != null) {
				playerCharacter.GearOff(hand.Equipment_Stats);	
			}
			listMesh[4].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
			playerCharacter.GearOn(newItem.Equipment_Stats);
			hand = newItem;
			break;
		case 3:
			if (torso != null) {
				playerCharacter.GearOff(torso.Equipment_Stats);	
			}
			listMesh[5].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
			playerCharacter.GearOn(newItem.Equipment_Stats);
			torso = newItem;
			break;
		case 4:
			if (leg != null) {
				playerCharacter.GearOff(leg.Equipment_Stats);	
			}
			listMesh[6].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
			playerCharacter.GearOn(newItem.Equipment_Stats);
			leg = newItem;
			break;
		case 5:
			if (feet != null) {
				playerCharacter.GearOff(feet.Equipment_Stats);	
			}
			listMesh[7].GetComponent<SkinnedMeshRenderer>().sharedMesh = newItem.Get_Item_Geo;
			// add stats to player base on new item.
			playerCharacter.GearOn(newItem.Equipment_Stats);
			feet = newItem;
			break;
		default:
			Debug.Log("This is wrong type of equipment type");
			break;
		}
		charBlock.GetComponent<MenuManager>().UpdateCharacterBlock();
	}

	public void EquipWeapon (EquipmentItem newItem){
		
	}
	#endregion

	#region Private Methods

	#endregion

}
