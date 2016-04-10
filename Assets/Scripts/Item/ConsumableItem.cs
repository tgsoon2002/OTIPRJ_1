using UnityEngine;
using System.Collections;

public enum ItemType
{
    HEAL,
    BUFF,
    DEBUFF,
    DAMAGE
}

public class ConsumableItem : UseableItem, IConsumableItem 
{

	#region Data Members

	private Sprite itemSprite;	// Contains the sprite for a specific item
	private float itemRange;		// Describes the range of the item's effects
    private ItemType itemType;

	#endregion

	#region Setters & Getters

    public Sprite Item_IconSprite
    {
        get { return itemSprite; }
        set { itemSprite = value; }
    }

    public float Item_Range
    {
        get { return itemRange; }
        set { itemRange = value; }
    }

	#endregion

	#region Built-In Unity Methods

    //// Use this for initialization
    //protected override void Start () {

    //}

    //// Update is called once per frame
    //protected override void Update () {

	//}

	#endregion

	#region Public Methods

	public ConsumableItem(ItemType _type, float range, bool craft, int id, string name, string description, Sprite icon, int price,  BaseItemType type, bool isStack, int weight)
		: base(id, name, description, icon, price, type, isStack, weight)
    {
        itemSprite = new Sprite();
        itemRange = range;
        itemType = _type;
    }


	// Activates a specific item in the inventory
	public void ActivateItem() 
    {
		//TBD
        switch(itemType)
        {
            case ItemType.BUFF:
            break;

            case ItemType.DEBUFF:
            break;

            case ItemType.DAMAGE:
            break;

            default:
            break;
        }
	}

	#endregion

	#region Private Methods

	#endregion

}
