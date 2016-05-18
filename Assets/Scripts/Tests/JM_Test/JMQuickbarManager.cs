using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JMQuickBarManager : MonoBehaviour
{
	#region Data Members

	private List<GameObject> quickSlots;
	public GameObject slotPrefab;
	public delegate void InventorySlotEvent (JMItemInfo item);
	public static event InventorySlotEvent itemAssigned;
	private static JMQuickBarManager _instance;

	#endregion

	#region Setters & Getters

	public static JMQuickBarManager Instance 
	{
		get { return _instance; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		quickSlots = new List<GameObject>();
		for(int i = 0; i < 4; i++)
		{
			GameObject tmp = Instantiate(slotPrefab);

			tmp.transform.SetParent(gameObject.transform);
			tmp.GetComponent<JMQuickItem>().quickBarMngr = gameObject;
			quickSlots.Add(tmp);
		}
	}

	#endregion

	#region Public Methods

	public bool CheckIfItemOnBar(int itemID)
	{
		return quickSlots.Exists(o => o.GetComponent<JMQuickItem>().itemID == itemID);
	}

	public void CheckAndClearOtherSlots(int itemID)
	{
		if(quickSlots.Exists(o => o.GetComponent<JMQuickItem>().itemID == itemID))
		{
			quickSlots[quickSlots.FindIndex(o => o.GetComponent<JMQuickItem>().itemID == 
				itemID)].GetComponent<JMQuickItem>().RemoveReference();
		}
	}

	public void ClearQuickBarSlot (JMItemInfo item)
	{
		quickSlots [quickSlots.FindIndex (o => o.GetComponent<JMQuickItem> ().itemID ==
			item.Item_Info.Item_ID)].GetComponent<JMQuickItem> ().RemoveReference ();		
	}

	public void UpdateQuickBarSlot (JMItemInfo newItem)
	{
		quickSlots [quickSlots.FindIndex (o => o.GetComponent<JMQuickItem> ().itemID ==
			newItem.Item_Info.Item_ID)].GetComponent<JMQuickItem> ().UpdateInfo (newItem);
	}

	public void TriggerItemAssignedEvent (JMItemInfo item)
	{
		itemAssigned (item);
	}

	#endregion

	#region Private Methods

	#endregion
}
