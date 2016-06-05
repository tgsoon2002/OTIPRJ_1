using UnityEngine;
using System.Collections;

public class UnequipOption : MonoBehaviour {

	EquipmentPart currentPart;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenOption(Transform slot){
		currentPart = slot.GetComponent<EquipmentIcon>().EquipType;
		gameObject.SetActive(true) ;
		transform.position = slot.transform.position;
	}

	public void _Confirm(){
		
		Debug.Log((int)currentPart);

		Inventory.Instance.RetriveItem((int)currentPart);
		gameObject.SetActive(false) ;
	}

	public void _Cancel() 
	{
		gameObject.SetActive(false) ;
	}
}
