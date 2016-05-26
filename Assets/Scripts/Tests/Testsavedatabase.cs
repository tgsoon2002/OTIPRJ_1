using UnityEngine;
using System.Collections;

public class Testsavedatabase : MonoBehaviour {

	public CharacterSkillDatabase db;
	public CharacterSkillSet character;
	public CharacterSkillSet character2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveData(){
		db.SaveCharSkill(character);
		db.SaveCharSkill(character2);
		Debug.Log("Done");
	}
	public void LoadData(){
		Debug.Log("in the db :" + db.LoadCharSkill(character));
		Debug.Log("in the db :" + db.LoadCharSkill(character2));
	}
}
