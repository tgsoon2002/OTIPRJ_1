using System;
using UnityEngine;

namespace Items_And_Inventory
{
	#region Items Enumerations

	/// <summary>
	/// Enumerates different types of
	/// 'base' items in the game.
	/// </summary>
	public enum BaseItemType 
	{
		EQUIPMENT,
		CONSUMABLE,
		NON_CONSUMABLE
	};

	#endregion

	#region Inventory Interfaces

	/// <summary>
	/// Stores the most basic information that ItemInfo
	/// contains.
	/// </summary>
	public interface IStoreable
	{
		int Item_ID 
		{
			get;
		}

		string Item_Name 
		{
			get;
			set;
		}

		string Item_Description
		{
			get;
			set;
		}

		int Item_Type 
		{
			get;
			set;
		}

		int Item_Quantity 
		{
			get;
			set;
		}


		Sprite Item_Sprite
		{
			get;
		}
	}

	/// <summary>
	/// This interface is mostly used by the Item
	/// Quick Bar to read ItemInfo objects.
	/// </summary>
	public interface IUseable : IStoreable
	{
		int Quickbar_Index
		{
			get;
			set;
		}
	}

	/// <summary>
	/// Used by the Inventory Menu when
	/// handling Equipment items passed by
	/// the ItemInfo class.
	/// </summary>
	public interface IEquippable :IStoreable
	{
		bool Is_Equipped
		{
			get;
			set;
		}
	}
		
	#endregion

	#region Item Slots Interface

	/// <summary>
	/// Used by the Item Slot Game Object
	/// to decouple its component class.
	/// </summary>
	public interface ISlottable
	{
		int Item_Quantity
		{
			get;
			set;
		}

		int Item_ID
		{
			get;
			set;
		}

		void InitializeItemSlot(IStoreable item);
		void UpdateQuantity(int qty);
	}

	#endregion

	#region Loot Interfaces

	/// <summary>
	/// Interface for the Loot GameObject,
	/// since the only thing we need from it
	/// is the ItemInfo object it contains.
	/// </summary>
	public interface ILootable
	{
		ItemInfo Item_Info
		{
			get;
			set;
		}
	}

	#endregion
}

