//using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats  {
	#region Data Members

	List<CharStat> listStats;
	List<CharAttribute> listAttribute;


	#endregion

	#region Setters & Getters
	/// <summary>
	/// Sets the list stat.
	/// </summary>
	/// <value>The list stat.</value>
	public List<CharStat> ListStat{
		set{ listStats = value;}
	}
	/// <summary>
	/// Gets or sets the list attribute.
	/// </summary>
	/// <value>The list attribute.</value>
	public List<CharAttribute> ListAttribute {
		set{ listAttribute = value;}
		get{ return listAttribute;}
	}
	#endregion

	#region Builtin Method

	void Start () {
		
		listStats = new List<CharStat>();
		listAttribute = new List<CharAttribute>();
	}
		
	#endregion

	#region MainMethod
	/// <summary>
	/// Stats the change. 0: strength,1:agility,2:intelligent, 3:vitality,4:luck
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="type">Type.</param>
	public void StatChange(float value, int type){
		listStats[type].StatValue += value ;

	}

	public void BonusStatChange(float value, int type){
		listStats[type].BonusValueChange (value);
	}

	/// <summary>
	/// Return stat value, type base on index 
	/// 0: strength,1:agility,2:intelligent, 3:vitality,4:luck
	/// </summary>
	/// <returns>The value.</returns>
	/// <param name="type">Type.</param>
	public float StatValue(int type){
		return listStats[type].StatValue;
	}

	public string StatsName(int type){
		return listStats[type].StatName;
	}

	/// <summary>
	/// Attributes the change. 
	/// 0:maxHP, 1:maxMP, 2:maxSP, 3:pAttack, 4:mAttack, 5:dodgRate, 
	/// 6:mvSpeed, 7:critChance, 8:regSp, 9:regMp, 10:pDef, 11:mDef, 
	/// 12:currentHP, 13:currentMP, 14:currentSP
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="type">Type.</param>
	public void AttributeChange(float value, int type){
		listAttribute[type].BaseValueChange (value);
	}

	public void BonusAttributeChange(float value, int type){
		listAttribute[type].BonusValueChange (value);
	}





	/// <summary>
	/// Return attribute, type base on index.
	/// 0:maxHP, 1:maxMP, 2:maxSP, 3:pAttack, 4:mAttack, 5:dodgRate, 
	/// 6:mvSpeed, 7:critChance, 8:regSp, 9:regMp, 10:pDef, 11:mDef, 
	/// 12:currentHP, 13:currentMP, 14:currentSP
	/// </summary>
	/// <returns>The value.</returns>
	/// <param name="type">Type.</param>
	public float AttributeValue(int type){
		return listAttribute[type].StatValue;
	}
		
	public string AttributeName(int type){
		return listAttribute[type].StatName;
	}
	#endregion

}
