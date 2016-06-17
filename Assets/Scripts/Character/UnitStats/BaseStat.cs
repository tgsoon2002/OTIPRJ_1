using UnityEngine;
using System.Collections;

public abstract class BaseStat  {
	#region Data Members

	protected string name;
	protected float baseValue;
	protected float bonusValue;

	#endregion

	#region Setters & Getters

	public string StatName {get{return name;}set{name = value;}}

//	public float BaseValue {get{return baseValue;}set{baseValue = value;}}

	#endregion

	#region Constructors
	/// <summary>
	/// Class constructor. We need this constructor because this class not 
	/// inheriting from MonoBehaviour
	/// </summary>
	/// <param name="newName">New stat name.</param>
	/// <param name="newValue">New stat value.</param>
	public BaseStat(string  newName, float newValue){
		name = newName;
		baseValue = newValue;
		bonusValue = 0;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Mutator for baseValue.
	/// </summary>
	/// <param name="value">Value.</param>
	public void BaseValueChange(float value){
		baseValue += value;
	}

	/// <summary>
	/// Mutator for bonusValue
	/// </summary>
	/// <param name="value">Value.</param>
	public void BonusValueChange(float value){
		bonusValue +=value;
	}

	#endregion



}
