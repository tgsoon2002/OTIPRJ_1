using System;

namespace ItemInterface
{
	public interface IContainable
	{
		int Item_ID
		{
			get;
			set;
		}

		int Owner_ID
		{
			get;
			set;
		}

		int Is_On_Quick_Bar
		{
			get;
			set;
		}

		int Item_Quantity
		{
			get;
			set;
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
	}

	public interface ISlottable
	{
		IContainable Item_Info
		{
			get;
			set;
		}

		void Initialize_Slot(IContainable info);
	}
}

