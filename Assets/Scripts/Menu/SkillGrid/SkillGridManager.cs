using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillGridManager : MonoBehaviour {

	#region Data Members
	private Ray ray;
	private RaycastHit hit;
	public Camera skillGridCamera;
	public Text skillPointText;
	public GameObject skillGridObject;
	int skillPointLeft = 10;
	public Indicator[] indicatorArray;
	public SkillNode[] skillNodeArray;

	public static SkillGridManager _instance;
	#endregion

	#region Setters & Getters

	public static SkillGridManager Instance {
		get{ return  _instance; }

	}

	public bool GridObject {
		get{ return  skillGridObject.activeSelf; }
		set{ skillGridObject.SetActive (value) ;
			//LoadSkillMap();
		}
	}
	#endregion

	#region Built-in Unity Methods
	void Awake(){
		skillGridObject.SetActive(false);
	}

	void Start () {
		_instance = this;
		UpdateSkillPointText ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			SelectNode();	
		}
	}
	#endregion

	#region Public Methods

	public void ResetGridMap() 
	{
		for (int i = 0; i < skillNodeArray.Length; i++) {
			skillNodeArray[i].UnlockedState = false;
		}
	}

	public void SaveSkillMap(){
		SquadManager.Instance.FocusedUnit.GetComponent<CharacterSkillSet>().SetCharSkillMap(skillNodeArray);
	}

	public void LoadSkillMap(){
		bool[] tempmap = SquadManager.Instance.FocusedUnit.GetComponent<CharacterSkillSet>().GetCharSkillMap();
		for (int i = 0; i < skillNodeArray.Length; i++) {
			skillNodeArray[i].UnlockedState =	tempmap[i];
		}
	}

	#endregion

	#region Private Methods

	private void SelectNode()
	{
		ray = skillGridCamera.ScreenPointToRay(Input.mousePosition);
		//Debug.Log("Raycasting ");
		Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
		if(Physics.Raycast(ray, out hit, 100.0f))
		{
			if(hit.collider.gameObject.tag == "SkillNode")
			{
				if (CheckSkillPoint (hit.collider.gameObject.GetComponent<SkillNode>())) {
					hit.collider.gameObject.GetComponent<SkillNode>().StartUnlock(indicatorArray);
				} else {
					Debug.Log("Not Enough");
				}	
			}
		}


	}

	private bool CheckSkillPoint (SkillNode skill)
	{
		bool canUnlock = true;
		foreach (var node in skill.requirementSkill) {
			if(!node.UnlockedState)
				canUnlock = false;
		}
		if (skillPointLeft >= skill.SkillCostToUnlock && canUnlock) {
			skillPointLeft -= skill.SkillCostToUnlock;
			UpdateSkillPointText();
			return true;
		} else {
			return false;
		}
	}

	private void UpdateSkillPointText ()
	{
		skillPointText.text = "Skill Point : " + skillPointLeft.ToString();
	}
	#endregion






}
