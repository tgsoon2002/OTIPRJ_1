using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {

	public ItemDatabase itemDB;
	public SquadManager manger;
	public Transform model;
	public Transform upperArm;
	public  Mesh Witharmor;
	public  Mesh WithOutArmor;
	public Material steelMat;
	public Material skinMat;
	public  bool armed;

	public CharacterBlock charBlock;


	// Use this for initialization
	void Start () {
		armed = false;
		GetReference();
//		model = Resources.Load<GameObject>("Nakedgrunt").transform;
//		WithOutArmor = model.FindChild("UpperArm").GetComponent<SkinnedMeshRenderer>().sharedMesh;
//		model = Resources.Load<GameObject>("BasicSet").transform;
//		Witharmor = model.FindChild("UpperArm").GetComponent<SkinnedMeshRenderer>().sharedMesh;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.S)){
			Debug.Log("switching");
			if (upperArm == null) {
				upperArm = manger.focusedUnit.transform.FindChild("ARMS");
			}
			if (armed) {
				
				//upperArm.GetComponent<SkinnedMeshRenderer>(	).sharedMesh = WithOutArmor;


				SquadManager.Instance.focusedUnit.GetComponent<EquipmentSet>().EquipArmor((EquipmentItem)itemDB.GetItem(0,0));
				//charBlock.UpdateChar();
				upperArm.GetComponent<SkinnedMeshRenderer>(	).material = skinMat;
				armed  = false;
			} else {
				//EquipmentItem temp = (EquipmentItem)ItemDatabase.Instance.GetItem(1,0);
				//upperArm.GetComponent<SkinnedMeshRenderer>(	).sharedMesh = Witharmor;
				SquadManager.Instance.focusedUnit.GetComponent<EquipmentSet>().EquipArmor((EquipmentItem)itemDB.GetItem(1,0));
				//charBlock.UpdateChar();
				upperArm.GetComponent<SkinnedMeshRenderer>(	).material = steelMat;
				armed = true;
			}
		}
	}

	void GetReference(){
		EquipmentItem temp = (EquipmentItem)itemDB.GetItem(0,0);
		WithOutArmor = temp.Get_Item_Geo;
		temp = (EquipmentItem)itemDB.GetItem(1,0);
		Witharmor  = temp.Get_Item_Geo;
	}

}
