using UnityEngine;
using System.Collections;

public class JMItemInfo
{
	#region Data Members

	private BaseItem item;
	private int itemQuantity;
	private int charID;
	private bool isEquipped;
	private int isInQuickbar;

	#endregion

	#region Setters & Getters

	public BaseItem Item_Info
	{
		get { return item; }
		set { item = value; }
	}

	public int Is_In_Quickbar 
	{
		get { return isInQuickbar; }
		set { isInQuickbar = value; }
	}

	public int Item_Qty
	{
		get { return itemQuantity; }
		set { itemQuantity = value; }
	}

	public int Char_ID 
	{
		get { return charID; }
		set { charID = value; }
	}

	public bool Is_Equipped
	{
		get { return isEquipped; }
		set { isEquipped = value; }
	}

	#endregion

	#region Built-In Unity Methods

	#endregion

	#region Public Methods

	/// <summary>
	/// Constructor with the item ID, item quantity, type of item.
	/// </summary>
	/// <param name="itemID">Item ID.</param>
	/// <param name="quan">Item Quantity.</param>
	/// <param name="type">Item Type.</param> 
	public JMItemInfo(int char_ID, int itemID, int _itemQty, int itemType)
	{
		item = ItemDatabase.Instance.GetItem (itemID, itemType);
		itemQuantity = _itemQty;
		charID = char_ID;
		isEquipped = false;
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion



}
