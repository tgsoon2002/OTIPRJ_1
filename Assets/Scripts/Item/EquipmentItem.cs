using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This enumeration describes all the body parts that any
// appropriate items can be used on.
public enum EquipmentPart 
{
	HEAD,
	ARMS,
	HANDS,
	TORSO,
	LEGS,
	FEET,
	MACE,
	SHIELD,
	SWORD,
	BOW,
	STAFF,
	DAGGER
};

public class EquipmentItem : UseableItem
{
	
	#region Data MBodyPartembers

	private Mesh itemGeo;								// The geometry mesh of the item.
	private Dictionary<string, int> statsRequirement;	// The stats requirements of the item.
	//public string model;								// The model for the item.
	private EquipmentPart equipmentPart;				// The body part the item belongs to.
	private EquipmentStat equipmentStats;					// Hold stat of item affect on theplayer.

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets or sets the equipment stats.
	/// This will affect on player either as benefit or a curse.
	/// </summary>
	/// <value>The equipment stats.</value>
	public EquipmentStat Equipment_Stats {
		get{ return  equipmentStats; }
		set{ equipmentStats = value; }
	}

	// Gets the geometry mesh of the item.
	public Mesh Get_Item_Geo
	{
		get { return itemGeo; }
	}

	// Gets and sets the stats requirements.
	public Dictionary<string, int> Item_Stats_Requirement
	{
		get { return statsRequirement; }
		set { statsRequirement = value; }
	}

	/// <summary>
	/// Get/Set equipment type ( head,torso, arm, hand, leg, mace, ....)
	/// </summary>
	/// <value>The type of the equipment.</value>
	public EquipmentPart Equipment_Type 
    {
		get {return equipmentPart;}
		set {equipmentPart = value;}
	}

	#endregion

	#region Built-In Unity Methods

    //// Use this for initialization
    //protected override void Start () 
    //{
    //    base.Start ();
    //    itemGeo = new Mesh();
    //}

    //// Update is called once per frame
    //protected override void Update ()
    //{

    //}

	#endregion

	#region Public Methods

	public EquipmentItem( Mesh mesh, EquipmentPart part, int id, string name, string description, Sprite icon, int price, BaseItemType type, bool isStack, int weight)
		: base(id, name, description,icon, price, type, isStack, weight)
    {  
		itemGeo = mesh;
      //  model = _model;
        equipmentPart = part;
        statsRequirement = new Dictionary<string,int>();
    }

	#endregion

	#region Private Methods

	#endregion
}
