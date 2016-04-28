using UnityEngine;
using System.Collections;

public class SkillInputTest : MonoBehaviour {
	public GameObject skill;
	public GameObject cuck;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
		{
			skill.GetComponent<ActiveSkill> ().SkillReference().ActivateSkill();	
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			cuck.GetComponent<ActiveSkill> ().SkillReference().ActivateSkill();	
		}
	}
}
