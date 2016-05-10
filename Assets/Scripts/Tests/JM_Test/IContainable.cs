using UnityEngine;
using System.Collections;

public interface IContainable
{
	BaseItem Item_Info 
	{
		get ;
		set ;
	}

	bool Is_In_Quickbar 
	{
		get;
		set;
	}

	int Item_Qty
	{
		get;
		set;
	}
}
