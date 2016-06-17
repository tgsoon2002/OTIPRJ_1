using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharAttribute : CharStat {

	#region Data Members

	List< float> mods;
	List<CharStat> dependents;

	#endregion

	#region Setters & Getters


	#endregion

	#region Constructors

	/// <summary>
	/// Class constructor. We need this constructor because this class not 
	/// inheriting from MonoBehaviour
	/// </summary>
	/// <param name="newName">New stat name.</param>
	/// <param name="newValue">New stat value.</param>
	public CharAttribute(string newName, float newValue): base(newName,newValue){
		mods = new List<float>();
		dependents = new List<CharStat>();

	}

	#endregion


	#region Protected Methods

	protected override void CalculateActualStat ()
	{
		base.CalculateActualStat();
		for(int i = 0; i < mods.Count; i++)
		{
			base.calculatedStatValue += mods[0] * dependents[0].StatValue ;		
		}
	}

	#endregion

//	CharAttribute(float baseValue, List<CharStat> dLisdept){
//		initialValue = baseValue;
//		dependent = dList;
//		CalculateAttribute();
//	}
//	void CalculateAttribute(){
//		baseValue = initialValue;
//		for (int i = 0; i < modifiers.Count; i++) {
//			baseValue +=  (float)dependent[i].StatsValue*modifiers[i];
//		}
//	}
	#region Public Methods

	public void AddModifier(float newModifier, CharStat statName){
		mods.Add( newModifier);
		dependents.Add(statName);
	}
		
	#endregion



}
