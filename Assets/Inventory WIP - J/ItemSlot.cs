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
	private int itemIndexInInventory;
	private string itemName;
	private int isSlotted;

	private Vector2 offsetFromMouseCursor;

	public Transform rootTransform;
	public Transform parentTransform;
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

	public int Is_On_Slot
	{
		get { return isSlotted; }
		set { isSlotted = value; }
	}


	#endregion

	#region Built-in Unity Methods

	//n/a

		
	#endregion

	#region Public Methods

	public void InitializeItemSlot(IStoreable item)
	{
		itemID = item.Item_ID;
		itemQuantity = item.Item_Quantity;
		itemName = item.Item_Name;
		isSlotted = item.Quickbar_Index;
		itemIndexInInventory = item.Inventory_Index;

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
		offsetFromMouseCursor = eventData.position - new Vector2(transform.position.x, transform.position.y);
		transform.SetParent(rootTransform);
		transform.position = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position - offsetFromMouseCursor;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//When the item slot is dragged onto an empty area.
		if(eventData.pointerEnter == null)
		{
			//TO DO:
			//Put logic here that will make an item drop to the
			//game world.
		}

		transform.SetParent(parentTransform);
		gameObject.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	#endregion

	#region Private Methods

	#endregion
}
