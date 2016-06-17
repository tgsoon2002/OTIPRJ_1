using UnityEngine;
using System.Collections;

public class FixedStatBonus : BaseStat {

	#region Constructors

	/// <summary>
	/// Class constructor. We need this constructor because this class not 
	/// inheriting from MonoBehaviour
	/// </summary>
	/// <param name="newName">New stat name.</param>
	/// <param name="newValue">New stat value.</param>
	public FixedStatBonus( float newValue):base("", newValue){	}

	#endregion
}
