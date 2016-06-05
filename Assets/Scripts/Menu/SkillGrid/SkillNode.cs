using UnityEngine;
using System.Collections.Generic;

using System.Collections;

public class SkillNode : MonoBehaviour {

	#region Data Members
	public string skillName;
	public List<SkillNode> requirementSkill;
	public List<PathDefinition> pathList;
	[SerializeField]
	private bool unlocked ;
	//Component halo;
	[SerializeField]
	int skillCost = 1;
	#endregion

	#region Setters & Getters
	public int SkillCostToUnlock {
		get{ return  skillCost; }
	}
	public bool UnlockedState {
		get{ return  unlocked; }
		set{ if (!value) 
				ResetSkill();
			else
				Unlocked();
			}
	}
	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () {
		if (name == "Core") {
			(GetComponent("Halo") as Behaviour).enabled = true;
			unlocked = true;
		}else{
			(GetComponent("Halo") as Behaviour).enabled = false;
			unlocked = false;

		}
	}

	// Update is called once per frame
	void Update () {
		
	}
	#endregion

	#region Public Methods
	/// <summary>
	/// Starts the unlock. set the path for indicator to follow and create animation of unlock.
	/// </summary>
	/// <param name="indiPrefab">Indi prefab.</param>
	public void StartUnlock(Indicator[] indiPrefab) 
	{
		for (int i = 0; i < pathList.Count; i++) {
			indiPrefab[i].SetNewPath(pathList[i],this);
		}
	}
	/// <summary>
	/// Unlocked this instance. Called when unlock animation finish. When load character skill, this will also be called..
	/// -Show the halo(this will replace with somehting better
	/// -Turn on the path.
	/// </summary>
	void Unlocked (){
		Debug.Log("Unlock : " + skillName);
		unlocked = true;
		(GetComponent("Halo") as Behaviour).enabled = true;
		foreach (var path in pathList) {
			path.AppearLine();
		}
	}

	/// <summary>
	/// Resets the skillNode. 
	/// - Turn off halo.
	/// - set unlocked back to false.
	/// - turn off unlock path.
	/// </summary>
	void ResetSkill() 
	{
		Debug.Log("set back to lock state : " + skillName);
		unlocked = false;
		(GetComponent("Halo") as Behaviour).enabled = false;
		foreach (var path in pathList) {
			path.HideLine();
		}
	}
	#endregion

	#region Private Methods

	#endregion



}
