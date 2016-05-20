using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class JMInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler 
{
	#region Data Members

	private JMItemInfo itemInfo;
	private Image spriteGUI;
	private GameObject highlighter;
	private Vector2 offset;
	private int quantity;
	private bool equipped;
	private GameObject tempClone;

	public Text itemQuantityText;
	public Transform originalParent;
	public int equipmentPart;

	#endregion

	#region Setters & Getters

	public JMItemInfo Inventory_Item
	{
		get { return itemInfo; }
		set { itemInfo = value; }
	}

	public int ItemQuantity
	{
		get { return quantity;}

		set 
		{ 
			quantity = value; 
			itemQuantityText.text = quantity.ToString();
		}
	}

	public Image Sprite_GUI
	{
		get { return spriteGUI; }
		set { spriteGUI = value; }
	}

	public bool Is_Equipped 
	{
		get{ return  equipped; }
		set{ equipped = value; }
	}

	#endregion

	#region Built-In Unity Methods

	//Use this for initialization
	void Awake () 
	{
		//highlighter = transform.GetChild(1).gameObject;
		itemQuantityText = gameObject.transform.GetChild(0).GetComponent<Text>();
		spriteGUI = gameObject.GetComponent<Image>();
	}

	// Use this for initialization
	void Start () 
	{
		originalParent = transform.parent;

		if (true) 
		{
			spriteGUI.color = new Color(1f,1f,1f,0.6f);
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Initializes the item slot.
	/// </summary>
	/// <param name="item">Item.</param>
	public void InitItemSlot(JMItemInfo _item)
	{
		itemInfo = _item;
		spriteGUI.sprite = itemInfo.Item_Info.Item_Sprite;
		itemQuantityText.text = itemInfo.Item_Qty.ToString();
	}

	/// <summary>
	/// Deactivate the highliter child.
	/// </summary>
	public void Deselected()
	{
		highlighter.SetActive(false);
	}

	/// <summary>
	/// Activate highlighter child and call inventory to update the item selecting by left click.
	/// </summary>
	public void Selecting()
	{
		// Highlights the selected item.
		transform.parent.GetComponent<JMInventoryMenu>().UpdateSelectingItem(gameObject);
		highlighter.SetActive(true);

		// Deactivates the Inventory panel.
		transform.parent.GetComponent<JMInventoryMenu>().itemOptionPanel.gameObject.SetActive(false);
	}

	/// <summary>
	/// Call inventory to call fucntion itemOptionWindow with slot is this gameObject
	/// </summary>
	public void ItemOption()
	{
		// 
		transform.parent.GetComponent<JMInventoryMenu>().ItemOptionWindow(gameObject);
	}

	/// <summary>
	/// When slot was click by mouse, 
	/// If left click, then call selecting fucntion.
	/// Else If Right click, call ItemOption fucntion
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick(PointerEventData eventData)
	{
		if (Input.GetMouseButtonUp(0)) 
		{
			Selecting();
		}
		else if(Input.GetMouseButtonUp(1))
		{
			if (!itemInfo.Is_Equipped) 
			{
				transform.parent.GetComponent<JMInventoryMenu>().rightClickItem = this.GetComponent<JMInventorySlot>();
				ItemOption();
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
		originalParent = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent.parent.parent);
		this.transform.position = eventData.position;

	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position - offset;
		//tempClone.transform.position = eventData.position - offset;
		//tempClone.GetComponent<CanvasGroup>().blocksRaycasts= false;

		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnEndDrag(PointerEventData eventData)
	{


//		if (eventData.pointerEnter == null && eventData.pointerEnter.gameObject.GetComponent<JMInventorySlot> ().Is_Equipped) 
//		{
//			originalParent.GetComponent<JMInventoryMenu> ().rightClickItem = this;
//			originalParent.GetComponent<JMInventoryMenu> ().amountOptionPanel.SetActive (true);
//			originalParent.GetComponent<JMInventoryMenu> ().amountOptionPanel.GetComponent<AmountDropOption> ()._CalledToDrop (true); 
//		} 
		this.transform.SetParent (originalParent);
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion

}
