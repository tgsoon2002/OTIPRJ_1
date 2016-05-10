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

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start ()
	{
		quickSlots = new List<GameObject>();
		for(int i = 0; i < 4; i++)
		{
			GameObject tmp = Instantiate(slotPrefab);

			tmp.transform.SetParent(gameObject.transform);
			tmp.GetComponent<JMQuickItem>().quickBarMngr = 
				gameObject.GetComponent<JMQuickBarManager>();
			quickSlots.Add(tmp.GetComponent<JMQuickItem>());
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

	public void ClearQuickBarSlot (int index)
	{
		quickSlots [quickSlots.FindIndex (o => o.GetComponent<JMQuickItem> ().itemID ==
		index)].GetComponent<JMQuickItem> ().RemoveReference ();		
	}

	public void UpdateQuickBarSlot (int amount, int index)
	{
		quickSlots [quickSlots.FindIndex (o => o.GetComponent<JMQuickItem> ().itemID ==
			index)].GetComponent<JMQuickItem> ().UpdateInfo (amount);
	}

	public void TriggerItemAssignedEvent (int index)
	{
		itemAssigned (index);
	}

	#endregion

	#region Private Methods

	#endregion
}
