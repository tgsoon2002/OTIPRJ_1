using UnityEngine;
using System.Collections;

public class ActiveSkill : BaseSkill 
{
	#region Data Members
	protected int mpCost;
	protected int staminaCost;

	#endregion

	#region Setters & Getters
	public int Mp_Cost 
	{
		get { return mpCost; }
		set { mpCost = value; }
	}

	public int Stamina_Cost 
	{
		get { return staminaCost; }
		set { staminaCost = value; }
	}

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
	protected void Start () 
	{
		base.Start ();
		mpCost = 0;
		staminaCost = 0;
	}

	// Update is called once per frame
	protected void Update () 
	{

	}

	#endregion

	#region Public Methods
	public virtual IExecutable SkillReference ()
	{
		IExecutable tmp = null;

		return tmp;
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion
}
