using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EquipmentIcon : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	EquipmentPart part;
	UnequipOption block;

	public EquipmentPart EquipType {
		get{ return  part; }
	}
	// Use this for initialization
	void Start () {
		block = FindObjectOfType<UnequipOption>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerClick(PointerEventData eventData){
		
		if(Input.GetMouseButtonUp(1))
		{
			Debug.Log(part.ToString());
			//transform.parent.GetComponent<Inventory>().rightClickItem = this.GetComponent<InventorySlot>();
			block.OpenOption(transform);
		}
	}
}
