using UnityEngine;
using System.Collections;

public class BaseSkill : MonoBehaviour {

	#region Member
	public string skillName;
	public string skillDescription;
	//public SkillType skillType;
	public int skillLevel;
	#endregion

	#region MainMethod
	public void LevelUpSkill()
	{
		skillLevel++;
	}
	#endregion
}
