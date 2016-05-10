using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickBar : MonoBehaviour
{
    #region Data Members

    private List<QuickItem> quickSlots;
    public GameObject slotPrefab;

    #endregion

    #region Setters & Getters

    public List<QuickItem> Slot_Manager
    {
        get { return quickSlots; }
    }

    #endregion

    #region Built-in Unity Methods

    // Use this for initialization
	void Start ()
    {
		
	    quickSlots = new List<QuickItem>();
        for(int i = 0; i < 4; i++)
        {
            GameObject tmp = Instantiate(slotPrefab);
            tmp.transform.SetParent(gameObject.transform);
			tmp.GetComponent<QuickItem>().quickBarMngr = gameObject.GetComponent<QuickBar>();
            quickSlots.Add(tmp.GetComponent<QuickItem>());
        }
	}


    #endregion

    #region Public Methods

    public bool CheckIfItemOnBar(int itemID)
    {
		Debug.Log(quickSlots.Exists(o => o.itemID == itemID));
		return quickSlots.Exists(o => o.itemID == itemID);
    }

	public void UpdateQuickItemSlot(int itemID)
    {
		if (quickSlots.Exists(o=>o.itemID==itemID)) 
		{
			quickSlots[quickSlots.FindIndex(o => o.itemID == itemID)].GetComponent<QuickItem>().UpdateInfo();	
		}
    }

    public void CheckAndClearOtherSlots(int itemID)
    {
        if(quickSlots.Exists(o => o.itemID == itemID))
        {
			quickSlots[quickSlots.FindIndex(o => o.itemID == itemID)].GetComponent<QuickItem>().RemoveReference();
        }
    }

    #endregion

    #region Private Methods

    #endregion
}
