using UnityEngine;
using System.Collections;
using Items_And_Inventory;

/// <summary>
/// This class contains all the information related to the item.
/// </summary>
public class ItemInfo : IEquippable, IUseable
{
	#region Data Members
	private int itemQuantity;	// The amount of a specific item
	private int ownerID;		// The ID of the character that the item belongs to
	private int quickbarIndex;	// Whether or not the item is on the Quickbar
	private BaseItem item;		// The basic information on a specific item
	private bool isEquipped;	// Whether or not the item is equipped

	#endregion

	#region Setters & Getters

	// Gets and sets the basic item information.
	public BaseItem Item_Info
	{
		get { return item; }
		set { item = value; }
	}

	// Gets and sets the name of the item.
	public string Item_Name
	{
		get { return item.Item_Name; }
		set { item.Item_Name = value; }
	}

	public int Item_ID
	{
		get { return item.Item_ID; }
	}

	// Gets and sets the item's description.
	public string Item_Description
	{
		get { return item.Item_Description; }
		set { item.Item_Description = value; }
	}

	// Gets and sets the item's type.
	public int Item_Type
	{
		get { return (int)item.Base_Item_Type; }
		set { item.Base_Item_Type = (BaseItemType)value; }
	}

	// Gets the item's icon.
	public Sprite Item_Sprite
	{
		get { return item.Item_Sprite; }
	}

	// Gets and sets the quantity of the item.
	public int Item_Quantity
	{
		get { return itemQuantity; }
		set { itemQuantity = value; }
	}

	// Gets and sets the item owner's ID.
	public int Owner_ID
	{
		get { return ownerID; }
		set { ownerID = value; }
	}

	// Gets and sets the Quickbar slot number of the item.
	public int Quickbar_Index
	{
		get { return quickbarIndex; }
		set { quickbarIndex = value; }
	}

	// Gets and sets whether or not the item is eqipped.
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
	/// Initializes a new instance of the <see cref="JItemInfo"/> class.
	/// Use this constructor when instantiating an Item on the Game World.
	/// Typically...
	/// </summary>
	/// <param name="_item">Item.</param>
	/// <param name="qty">Qty.</param>
	public ItemInfo(BaseItem _item, int qty)
	{
		item = _item;
		itemQuantity = qty;
		quickbarIndex = -1;
		ownerID = -1;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="JItemInfo"/> class.
	/// Use this constructor when initializing a Character's Inventory.
	/// </summary>
	/// <param name="cID">C I.</param>
	/// <param name="_item">Item.</param>
	/// <param name="qty">Qty.</param>
	public ItemInfo(int cID, BaseItem _item, int qty)
	{
		ownerID = cID;
		item = _item;
		itemQuantity = qty;
		quickbarIndex = -1;
	}

	/// <summary>
	/// Return the total weight of this
	/// item.
	/// </summary>
	/// <returns>The weight.</returns>
	public int TotalWeight()
	{
		return itemQuantity * item.Item_Weight;
	}

	#endregion

	#region Private Methods

	#endregion
}