using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CharStat : BaseStat {

	#region Data Members

	protected List<float> listOfBonuses;
	protected float calculatedStatValue;

	#endregion

	#region Setters & Getters

	public float StatValue 
	{
		get{return calculatedStatValue;}
		set{calculatedStatValue = value;}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Class constructor. We need this constructor because this class not 
	/// inheriting from MonoBehaviour
	/// </summary>
	/// <param name="newName">New stat name.</param>
	/// <param name="newValue">New stat value.</param>
	public CharStat(string newName, float newValue): base(newName,newValue){
		calculatedStatValue = newValue;
		listOfBonuses = new List<float>();
	
	}

	#endregion

	#region Protected Methods

	/// <summary>
	/// Calculates the actual stat.
	/// </summary>
	protected virtual void CalculateActualStat(){
		calculatedStatValue = base.baseValue;
		for (int i = 0; i < listOfBonuses.Count; i++) {
			calculatedStatValue += listOfBonuses[i];	
		}
	}

	#endregion

	#region Public Methods

	public void AddBonus(float newValue){
		listOfBonuses.Add(newValue);
		CalculateActualStat();
	}

	public void RemoveBonus(float newValue){
		listOfBonuses.Remove(newValue);
		CalculateActualStat();
	}

	#endregion


}
