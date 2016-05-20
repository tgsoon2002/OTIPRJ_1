using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using UnityEngine.EventSystems;

public class JMQuickItem : MonoBehaviour, IDropHandler
{
	#region Data Members

	public GameObject quickBarMngr;
	public int? itemID;
	private Image slotIcon;
	private Sprite oriSlotIcon;
	private Text qtyGUI;
	private JMItemInfo itemRef;
	private int slotIndex;

	#endregion

	#region Setters and Getters

	public JMItemInfo Item_Ref 
	{
		get { return itemRef; }
		set { itemRef = value; }
	}

	public int Slot_Index 
	{
		set { slotIndex = value; }
	}

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start()
	{
		oriSlotIcon = gameObject.GetComponent<Image>().sprite;
		slotIcon = gameObject.GetComponent<Image>();
		qtyGUI = gameObject.transform.GetChild(0).GetComponent<Text>();
		gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

	#endregion

	#region Public Methods

	//Checking when some object Drop on the quickItem(itemSlot)
	public void OnDrop(PointerEventData eventData)
	{
		quickBarMngr.GetComponent<JMQuickBarManager> ().TriggerItemAssignedEvent (slotIndex);
	}

	// Update infomation of the quickItem, remove if item is empty
	public void UpdateInfo(int amount)
	{
		//Declaring local variables
		/*
		int originalAmount = Int32.Parse(qtyGUI.text);

		qtyGUI.text = (originalAmount - amount).ToString();	
		*/

		if (amount < 0) {
			itemRef.Item_Qty -= amount;
		} 
		else 
		{
			itemRef.Item_Qty += amount;
		}
			
		qtyGUI.text = itemRef.Item_Qty.ToString();
	}

	// remove the reference of quickItem to ItemSlots
	public void RemoveReference()
	{
		slotIcon.sprite = oriSlotIcon;
		qtyGUI.text = "0";
		itemID = null;
		itemRef = null;
	}

	#endregion

}
