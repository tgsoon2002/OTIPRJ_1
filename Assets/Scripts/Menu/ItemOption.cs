using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemOption : MonoBehaviour 
{
	#region Data Members

    private GameObject itmSlotRef;

    public Transform amtDropOptionDisplay;
	public GameObject amountPanel;
	public List<Transform> button;

	#endregion

	#region Setters & Getters

    public GameObject Item_Slot_Reference
    {
        get { return itmSlotRef; }
        set { itmSlotRef = value; }
    }

	#endregion

	#region Built-in Unity Methods
	void Awake(){
		for (int i = 0; i < 4; i++) {
			button.Add(transform.GetChild(i));	
		}
	}

	// Use this for initialization
	void Start () {
		ClosePanel();
	}

	#endregion

	#region Public Methods

	public void _CallDropPanel()
    {
        amountPanel.SetActive(true);
        amountPanel.transform.position = transform.position;
        amountPanel.GetComponent<DropOptionMenu>().DropItemCall(true, itmSlotRef);

		//ammontPanel.SetActive(true);

        //this.gameObject.SetActive(false);
		//ClosePanel();
	}

	public void ClosePanel()
	{
		gameObject.SetActive(false);
	}
	/// <summary>
	/// Populates the option for item base on type of item.
	/// </summary>
	/// <param name="slot">Slot in as gameobject containt data.</param>
	public void PopulateOption (GameObject slot){
		transform.position = slot.transform.position;
        switch ((int)slot.GetComponent<ItemInfo>().Item_Type) {
		case 0:  // Equipment
			button[0].gameObject.SetActive(true);
			button[3].gameObject.SetActive(false);
			break;
		case 1:  // consumable
			button[0].gameObject.SetActive(false);
			button[3].gameObject.SetActive(true);
			break;
		default:  // non - consumeable
			button[0].gameObject.SetActive(false);
			button[3].gameObject.SetActive(false);


			break;
		} 
	}
	#endregion

	#region Private Methods

	#endregion

}
