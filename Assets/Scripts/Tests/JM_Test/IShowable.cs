using UnityEngine;
using System.Collections;

public interface IShowable
{
	BaseItem Item_Contents
	{
		get;
		set;
	}

	int Item_Quantity
	{
		get;
		set;
	}
}
