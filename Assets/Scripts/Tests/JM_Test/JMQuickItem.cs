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

	#endregion

	#region Setters and Getters

	public JMItemInfo Item_Ref 
	{
		get { return itemRef; }
		set { itemRef = value; }
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
		//Declaring local variables
		bool itemExistsOnSlot;

		JMItemInfo tmp = eventData.pointerDrag.GetComponent<JMInventorySlot>().Inventory_Item;
		itemExistsOnSlot = quickBarMngr.GetComponent<JMQuickBarManager>().CheckIfItemOnBar(tmp.Item_Info.Item_ID);

		// Checking if the item is in the slot
		if (itemExistsOnSlot) 
		{
			quickBarMngr.GetComponent<JMQuickBarManager> ().CheckAndClearOtherSlots (tmp.Item_Info.Item_ID);
		} 
		else 
		{
			quickBarMngr.GetComponent<JMQuickBarManager> ().TriggerItemAssignedEvent (tmp);
		}
			
		slotIcon.sprite = tmp.Item_Info.Item_Sprite;  // Set icon Sprite
		qtyGUI.text = tmp.Item_Qty.ToString();	// Set quantity
		itemID = tmp.Item_Info.Item_ID;		// set itemID to keep track
	}

	// Update infomation of the quickItem, remove if item is empty
	public void UpdateInfo(JMItemInfo newItem)
	{
		//Declaring local variables
		/*
		int originalAmount = Int32.Parse(qtyGUI.text);

		qtyGUI.text = (originalAmount - amount).ToString();	
		*/

		itemRef = newItem;
		qtyGUI.text = itemRef.Item_Qty.ToString();
	}

	// remove the reference of quickItem to ItemSlots
	public void RemoveReference()
	{
		slotIcon.sprite = oriSlotIcon;
		qtyGUI.text = "0";
		itemID = null;
	}

	#endregion

}
