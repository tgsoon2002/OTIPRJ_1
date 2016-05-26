using UnityEngine;
using System.Collections;

public class CharacterSkillSet : MonoBehaviour  {


#region Data Members

	public int charID;
	public bool[] unlocked;
	public int SkillPointAvalible;
	// Use this for initialization
#endregion

#region Setters & Getters
	public int CharacterID {
		get{ return  charID; }
	}
#endregion

#region Built-in Unity Methods (empty)
	void Start () {
		if (unlocked.Length <= 0) {
			unlocked = new bool[GlobalVar.TOTALSKILL];	
		}

	}
//
//	// Update is called once per frame
//	void Update () {
//	}
#endregion

#region Public Methods
	public void SetCharSkillMap(SkillNode[] currentMap) 
	{
		for (int i = 0; i < currentMap.Length; i++) {
			unlocked[i] = currentMap[i].UnlockedState;
		}
	}

	public bool[] GetCharSkillMap() 
	{
		return unlocked;
	}
#endregion

#region Private Methods

#endregion




}
