using UnityEngine;
using System.Collections;

public class JMItemInfo
{
	#region Data Members

	public BaseItem item;
	public int itemQuantity;
	public int charID;
	public bool isEquipped;

	#endregion

	/// <summary>
	/// Constructor with the item ID, item quantity, type of item.
	/// </summary>
	/// <param name="itemID">Item ID.</param>
	/// <param name="quan">Item Quantity.</param>
	/// <param name="type">Item Type.</param> 
	public JMItemInfo(int char_ID, int itemID, int itemQty, int itemType)
	{
		item = ItemDatabase.Instance.GetItem (itemID, itemType);
		itemQty = itemQty;
		charID = char_ID;
		isEquipped = false;
	}
}
