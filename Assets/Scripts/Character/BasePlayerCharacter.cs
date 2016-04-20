using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasePlayerCharacter : BaseCharacter {

	public struct CharacterInventory
	{
		public List<ItemInfo> listItem;
		public List<ItemInfo> quickItem;

	}

	#region Member
	//public List<BaseSkill> activeSkill;
	//public SkillTree skillTree;
	public List <int> activeItem;
	public List<BaseItem> inventory;
	public int charLevel;
	public int charID;
	bool isCrounching = false;
	public CharacterInventory charInv;
	//protected JobClass classType;
	protected int index;
	FeetCollider bottomCollider;

	#endregion

	public void Init(int index){
		
		base.statTable = new UnitStats();

		UnitDataBase.Instance.SetUnitStat(base.statTable,index);
		UnitDataBase.Instance.SetUnitInfo(this,index);
		bottomCollider = GetComponentInChildren<FeetCollider>();
		}

	#region MainMethod
	#region Get Value
	public string CharacterName {
		get{ return  base.characterName; }
		set{ base.characterName = value; }
	}

	public override UnitStats CharacterStats {
		get{return base.statTable;}
		set{base.statTable = value;}
	}

	public virtual float PercentageStamina(){
		return 0.0f;//(statTable.staminaPoint/statTable.maxStaminaPoint);
	}
	#endregion
	#region Perform Action


	/// <summary>
	/// Called to player character perform Primary attack action
	/// </summary>
	public void PrimaryAttack(){
		anim.SetTrigger("attackPrimary");
	}

	public void SecondaryAttack(){
		anim.SetTrigger("attackSecondary");
	}

	/// <summary>

	/// Moves the this unit if they are on the ground and is not crounching
	/// 
	/// </summary>
	/// <param name="direction">Direction.</param>
	public override void MoveThisUnit(float direction){
		if (bottomCollider.IsTouchGround && !isCrounching ) {
			base.MoveThisUnit(direction);
		}
	}



	/// <summary>
	/// Called to player perform Jump action
	/// </summary>
	public virtual void Jump(){
		//base.rig.velocity = new Vector2(5.0f,base.rig.velocity.y); 
		float jumpPw = 1;
		if (bottomCollider.IsTouchGround ) {
			bottomCollider.IsTouchGround = false;
			if (isCrounching) {
				jumpPw *= 1.7f;
			}
			base.rig.AddForce((Vector3.up * 10.0f *jumpPw ),ForceMode.Impulse);
		}
	}

	public void Crounch(bool val){
		anim.SetBool("crounching",val);
		isCrounching = val;
	}

	/// <summary>
	/// Call to player character perform dash action if character on ground and is not crounching
	/// </summary>
	public virtual void DashThisUnit(float direction){
		if (bottomCollider.IsTouchGround && !isCrounching) {
		base.rig.AddForce((Vector3.right * 10.0f * direction),ForceMode.Impulse);
		}
	}

	#endregion
	#region Update Status

	/// <summary>
	/// Add the Stat and Attribute to character statTable base on new equipment
	/// </summary>
	/// <param name="newEquipment">New equipment.</param>
	public void GearOn(EquipmentStat newEquipment) 
	{
		for (int i = 0; i < 5; i++) {
			statTable.StatChange(newEquipment.stats[i],i);
		}
		for (int i = 0; i < 12; i++) {
			statTable.AttributeChange(newEquipment.attributes[i],i);
		}
	}

	/// <summary>
	/// Remove Stat and attribute to character base on oldEquipment
	/// </summary>
	/// <param name="newEquipment">New equipment.</param>
	public void GearOff(EquipmentStat newEquipment) 
	{
		for (int i = 0; i < 5; i++) {
			statTable.StatChange(newEquipment.stats[i] * -1.0f,i);
		}
		for (int i = 0; i < 12; i++) {
			statTable.AttributeChange(newEquipment.attributes[i]* -1.0f,i);
		}
	}
	#endregion

	public void ResetTriggerAttack(int typeOfAttack){
		switch (typeOfAttack) {
		case 0:
			anim.ResetTrigger("attackPrimary");
			break;
			case 1:
			anim.ResetTrigger("attackSecondary");
			break;
		default:
			break;
		}

	}
	#endregion

	#region Collider Methods


	#endregion

}
