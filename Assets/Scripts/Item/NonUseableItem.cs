using UnityEngine;
using System.Collections;
using Items_And_Inventory;

public class NonUseableItem : BaseItem
{
	#region Data Members

	private bool isCraftable; // Determines whether or not a specific item is craftable

	#endregion

	#region Setters & Getters

	// Gets and sets the value of isCraftable
	public bool Is_Craftable 
    {
		get {return isCraftable;}
		set {isCraftable = value;}
	}

	#endregion

	#region Built-In Unity Methods

	#endregion

    #region Public Methods

    public NonUseableItem(bool craft, int id, string name, string description, Sprite icon, int price, BaseItemType type, bool isStack, int weight)
		: base(id, name, description,icon, price, type, isStack, weight)
    {
        isCraftable = craft;
    }


    #endregion
}
