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
	public CharacterInventory charInv;
	//protected JobClass classType;
	protected int index;


	#endregion

	public void Init(int index){
		
		base.statTable = new UnitStats();

		UnitDataBase.Instance.SetUnitStat(base.statTable,index);
		UnitDataBase.Instance.SetUnitInfo(this,index);
		}

	#region MainMethod
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

	public virtual void BasicAttack(){}

	public virtual void Jump(){
		rig.velocity = new Vector2(5.0f,rig.velocity.y);
	}

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

}
