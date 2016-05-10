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

		IContainable itemRef = eventData.pointerDrag.GetComponent<JMInventorySlot>().Inventory_Item;
		itemExistsOnSlot = quickBarMngr.GetComponent<JMQuickBarManager>().CheckIfItemOnBar(itemRef.Item_Info.Item_ID);

		if (itemExistsOnSlot) 
		{
			quickBarMngr.GetComponent<JMQuickBarManager> ().CheckAndClearOtherSlots (itemRef.Item_Info.Item_ID);
		} 
		else 
		{
			quickBarMngr.GetComponent<JMQuickBarManager> ().TriggerItemAssignedEvent (itemRef.Item_Info.Item_ID);
		}
			
		slotIcon.sprite = itemRef.Item_Info.Item_Sprite;  // Set icon Sprite
		qtyGUI.text = itemRef.Item_Qty.ToString();	// Set quantity
		itemID = itemRef.Item_Info.Item_ID;		// set itemID to keep track
	}

	// Update infomation of the quickItem, remove if item is empty
	public void UpdateInfo(int amount)
	{
		//Declaring local variables
		int originalAmount = Int32.Parse(qtyGUI.text);

		qtyGUI.text = (originalAmount - amount).ToString();
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
