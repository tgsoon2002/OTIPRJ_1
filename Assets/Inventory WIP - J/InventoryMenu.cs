using UnityEngine;
using System.Collections;
using Items_And_Inventory;

public class InventoryMenu : MonoBehaviour 
{
	#region Data Members

	private static InventoryMenu _instance;

	#endregion

	#region Setters & Getters

	public static InventoryMenu Instance
	{
		get { return _instance; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		_instance = this;
	}

	#endregion

	#region Public Methods



	#endregion

	#region Private Methods

	private void AddSlot(IStoreable item)
	{

	}

	#endregion
}
