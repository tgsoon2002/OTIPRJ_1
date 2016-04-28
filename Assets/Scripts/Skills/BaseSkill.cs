using UnityEngine;
using System.Collections;

public class BaseSkill : MonoBehaviour 
{

	#region Data Members
	protected string skillName;
	protected int skillID;
	protected string skillDescription;
	protected int skillLevel;

	#endregion

	#region Setters & Getters
	public string Skill_Name 
	{
		get {return skillName;}
		set {skillName = value;}
	}

	public int Skill_ID
	{
		get {return skillID;}
		set {skillID = value;}
	}

	public string Skill_Description 
	{
		get {return skillDescription;}
		set {skillDescription = value;}
	}

	public int Skill_Level 
	{
		get {return skillLevel;}
		set {skillLevel = value;}
	}

	#endregion

	#region Built-In Unity Methods
	protected void Start()
	{
		skillName = "";
		skillID = 0;
		skillDescription = "";
		skillLevel = 0;
	}

	// Update is called once per frame
	protected void Update () 
	{

	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion
}
