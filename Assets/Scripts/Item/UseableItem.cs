using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items_And_Inventory;

public class UseableItem : BaseItem {

	#region Data Members

	private Dictionary<string, int> statsModifier;  // Contains the name of the stat and its modifier

	#endregion

	#region Setters & Getters

	// Gets and sets the stat and its modifier
	public Dictionary<string,int> Item_Bonus_Stat_Modifier
	{
		get{ return statsModifier; }
		set{ statsModifier = value; }
	}

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
    //protected override void Start() 
    //{
    //    base.Start ();
    //    
    //}

	// Update is called once per frame
    //protected override void Update ()
    //{

    //}

	#endregion

	#region Public Methods

	public UseableItem(int id, string name, string description, Sprite icon, int price, BaseItemType type, bool isStack, int weight) :
        base(id, name, description, icon, price, type, isStack, weight)
    {
        statsModifier = new Dictionary<string, int>();
    }

	#endregion

	#region Private Methods

	#endregion
}
