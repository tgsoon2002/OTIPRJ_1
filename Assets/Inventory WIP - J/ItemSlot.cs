using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Items_And_Inventory;

public class ItemSlot : MonoBehaviour, ISlottable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region Data Members

	private int itemID;
	private int itemQuantity;
	private string itemName;

	public Text text;
	public Image icon;

	#endregion

	#region Setters & Getters

	public int Item_ID
	{
		get { return itemID; }
		set { itemID = value; }
	}

	public int Item_Quantity
	{
		get { return itemQuantity; }
		set { itemQuantity = value; }
	}

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}
		
	#endregion

	#region Public Methods

	public void InitializeItemSlot(IStoreable item)
	{
		itemID = item.Item_ID;
		itemQuantity = item.Item_Quantity;
		itemName = item.Item_Name;

		//Initialize the quantity text
		text.text = itemQuantity.ToString();

		//Initialize the icon
		icon.sprite = item.Item_Sprite;	//Set the quick slot icon with the item sprite
		icon.color = new Vector4(255f, 255f, 255f, 255f); //Make sure the alpha value is turned 
														  //all the way up
	}

	/// <summary>
	/// Called by the menu whenever the item 
	/// quantity is updated.
	/// </summary>
	/// <param name="qty">Qty.</param>
	public void UpdateQuantity(int qty)
	{
		itemQuantity = qty;
		text.text = qty.ToString();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		
	}

	public void OnDrag(PointerEventData eventData)
	{

	}

	public void OnEndDrag(PointerEventData eventData)
	{

	}

	#endregion

	#region Private Methods

	#endregion
}
