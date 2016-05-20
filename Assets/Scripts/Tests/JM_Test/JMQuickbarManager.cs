using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JMQuickBarManager : MonoBehaviour
{
	#region Data Members

	private List<GameObject> quickSlots;
	public GameObject slotPrefab;
	public delegate void InventorySlotEvent (int index);
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
			tmp.GetComponent<JMQuickItem> ().Slot_Index = i;
			quickSlots.Add(tmp);
		}
	}

	#endregion

	#region Public Methods

	public bool CheckIfItemOnBar(int itemID)
	{
		return quickSlots.Exists(o => o.GetComponent<JMQuickItem>().itemID == itemID);
	}

	public void CheckAndClearOtherSlots(int index, int amount)
	{
		quickSlots [index].GetComponent<JMQuickItem> ().UpdateInfo (amount);
	}

	public void ClearQuickBarSlot (JMItemInfo item)
	{
		quickSlots [quickSlots.FindIndex (o => o.GetComponent<JMQuickItem> ().itemID ==
			item.Item_Info.Item_ID)].GetComponent<JMQuickItem> ().RemoveReference ();		
	}

	public void UpdateQuickBarSlot (int index, int amount)
	{
		quickSlots [index].GetComponent<JMQuickItem> ().Item_Ref.Item_Qty += amount;
	}

	public void TriggerItemAssignedEvent (int slotIndex)
	{
		itemAssigned (slotIndex);
	}

	#endregion

	#region Private Methods

	#endregion
}
