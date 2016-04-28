using UnityEngine;
using System.Collections;

public class CuckSkill : ActiveSkill, IExecutable {

	#region Data Members
	int test;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
	void Start () {
		base.Start ();
		test = 9999;
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#endregion

	#region Public Methods
	public void ActivateSkill ()
	{
		Debug.Log (test);
	}

	public override IExecutable SkillReference ()
	{
		base.SkillReference ();
		IExecutable tmp;
		tmp = this;

		return tmp;
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion
}
