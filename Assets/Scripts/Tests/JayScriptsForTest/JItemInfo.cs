using UnityEngine;
using ItemInterface;
using System.Collections;

/// <summary>
/// ItemInfo class shall contain all information for
/// an Item in the game.
/// </summary>
public class JItemInfo : IContainable
{
	#region Data Members

	private BaseItem item;	   //Contains all basic Item information.
	private int itemQty;	   //Item quantity.
	private int characterID;   //Character ID in which this Item belongs to.
							   //Assign to -1 when an Item is outside of
						       //a Character.
	private int isOnQuickBar;  //Integer that determines if this item is 
							   //associated with a QuickBarSlot, as well w
							   //which Slot in the QuickBar it is located.

	#endregion

	#region Setters & Getters

	public BaseItem Item_Info
	{
		get { return item; }
		set { item = value; }
	}
		
	public int Item_ID
	{
		get { return item.Item_ID; }
		set { item.Item_ID = value; }
	}

	public int Owner_ID
	{
		get { return characterID; }
		set { characterID = value; }
	}

	public int Item_Quantity
	{
		get { return itemQty; }
		set { itemQty = value; }
	}

	public string Item_Name
	{
		get { return item.Item_Name; }
		set { item.Item_Name = value; }
	}

	public string Item_Description
	{
		get { return item.Item_Description; }
		set { item.Item_Description = value; }
	}

	public int Is_On_Quick_Bar
	{
		get { return isOnQuickBar; }
		set { isOnQuickBar = value; }
	}

	#endregion

	#region Built-in Unity Methods

	#endregion

	#region Public Methods

	/// <summary>
	/// Initializes a new instance of the <see cref="JItemInfo"/> class.
	/// Use this constructor when instantiating an Item on the Game World.
	/// Typically...
	/// </summary>
	/// <param name="_item">Item.</param>
	/// <param name="qty">Qty.</param>
	public JItemInfo(BaseItem _item, int qty)
	{
		item = _item;
		itemQty = qty;
		isOnQuickBar = -1;
		characterID = -1;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="JItemInfo"/> class.
	/// Use this constructor when initializing a Character's Inventory.
	/// </summary>
	/// <param name="cID">C I.</param>
	/// <param name="_item">Item.</param>
	/// <param name="qty">Qty.</param>
	public JItemInfo(int cID, BaseItem _item, int qty)
	{
		characterID = cID;
		item = _item;
		itemQty = qty;
		isOnQuickBar = -1;
	}

	/// <summary>
	/// Return the total weight of this
	/// item.
	/// </summary>
	/// <returns>The weight.</returns>
	public int TotalWeight()
	{
		return itemQty * item.Item_Weight;
	}
		
	#endregion

	#region Private Methods

	#endregion
}
