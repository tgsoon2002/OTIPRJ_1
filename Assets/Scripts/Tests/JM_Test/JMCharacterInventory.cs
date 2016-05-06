using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JMCharacterInventory : MonoBehaviour 
{
	#region Data Members

	List<JMItemInfo> listOfItem;
	int maxWeight = 50;				// This value is hard coded. Character stats should
									// determine the weight.
	public int charID;

	#endregion

	#region Setters & Getters

	public int CharacterMaxWeight 
	{
		get{ return  maxWeight; }
		set{ maxWeight = value; }
	}

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
	void Start () 
	{
		//listOfItem = JMInventoryDB.Instance.GetInventoryItemForCharacter(charID);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Ask Kien WTF this does later.  May move this function to JMInventory.
	/// </summary>
	public void RepopulateInventory ()
	{
		// Get list of item from database if the list is empty
		if (listOfItem == null) 
		{
			listOfItem = JMInventoryDB.Instance.GetInventoryItemForCharacter(charID);
		}

		//update inventory block. 
		//Inventory.Instance.ClearInventory(maxWeight);

		foreach (var item in listOfItem) 
		{
			//to do: 
			//Implement JMInventory later.
			//Inventory.Instance.PopulateInventoryFromCharacter();
		}
		//Update character Block
	}

	public void AddItem (JMItemInfo item)
	{
		if (item.item.Is_Stackable)
		{
			int temp = listOfItem.FindIndex(o => o.item.Item_ID == item.item.Item_ID && 
											o.item.Base_Item_Type == item.item.Base_Item_Type);

			if (temp > -1) 
			{
				listOfItem[temp].itemQuantity += item.itemQuantity;
			} 
			else 
			{
				listOfItem.Add(new JMItemInfo(charID, item.item.Item_ID, item.itemQuantity, 
							  (int)item.item.Base_Item_Type));
			}
		} 
		else 
		{
			listOfItem.Add(new JMItemInfo(charID, item.item.Item_ID, item.itemQuantity, 
						  (int)item.item.Base_Item_Type));
		}
	}

	public void RemoveItem(JMItemInfo item)
	{
		int temp = listOfItem.FindIndex(o => o.item.Item_ID == item.item.Item_ID && 
									    o.item.Base_Item_Type == item.item.Base_Item_Type);

		if (temp > -1) 
		{
			listOfItem.RemoveAt(temp);
		}

	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion

}
