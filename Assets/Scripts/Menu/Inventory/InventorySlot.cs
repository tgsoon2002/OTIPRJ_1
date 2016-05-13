using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    #region Data Members

    //public GameObject itemObj;
	public Text itemQuantityText;
    private ItemInfo itemInfo;
    private Image spriteGUI;
	private GameObject highlighter;
	public Transform originalParent;
	private GameObject tempClone;

    private Vector2 offset;
	private int quantity;
	public int equipmentPart;

    #endregion

    #region Setters & Getters

    public ItemInfo Inventory_Item
    {
        get { return itemInfo; }
        set { itemInfo = value; }
    }

	public int ItemQuantity
	{
		get { return quantity;}
		set { quantity = value; 
			itemQuantityText.text = quantity.ToString();}
	}


    public Image Sprite_GUI
    {
        get { return spriteGUI; }
        set { spriteGUI = value; }
    }

	public bool IsEquip {
		get{ return  itemInfo._isEquiped; }
		set{ itemInfo._isEquiped = value;
			if (value) {
				spriteGUI.color = new Color(1f,1f,1f,0.6f);
			}else{
				spriteGUI.color = new Color(1f,1f,1f,1.6f);
			}	}
	}

    #endregion

    #region Built-in Unity Methods

    //Use this for initialization
	void Awake () 
    {
		highlighter = transform.GetChild(1).gameObject;
        spriteGUI = gameObject.GetComponent<Image>();

	}

	void Start(){
		originalParent = transform.parent;

//		if (itemInfo._isEquiped) {
//			spriteGUI.color = new Color(1f,1f,1f,0.6f);
//		}
	}

    #endregion

    #region Public Methods
	/// <summary>
	/// Deactivate the highliter child.
	/// </summary>
	public void Deselected(){
		highlighter.SetActive(false);
	}

	/// <summary>
	/// Activate highlighter child and call inventory to update the item selecting by left click.
	/// </summary>
	public void Selecting(){
		transform.parent.GetComponent<Inventory>().UpdateSelectingItem(gameObject);
		highlighter.SetActive(true);
		transform.parent.GetComponent<Inventory>().itemOptionPanel.gameObject.SetActive(false);
	}

	/// <summary>
	/// Call inventory to call fucntion itemOptionWindow with slot is this gameObject
	/// </summary>
	public void ItemOption(){
		
		transform.parent.GetComponent<Inventory>().ItemOptionWindow(gameObject);
	}
		
	/// <summary>
	/// When slot was click by mouse, 
	/// If left click, then call selecting fucntion.
	/// Else If Right click, call ItemOption fucntion
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick(PointerEventData eventData){
		if (Input.GetMouseButtonUp(0)) {
			Selecting();
		}
		else if(Input.GetMouseButtonUp(1))
		{
			if (!itemInfo._isEquiped) {
				transform.parent.GetComponent<Inventory>().rightClickItem = this.GetComponent<InventorySlot>();
				ItemOption();
			}
		}

	}

    public void OnBeginDrag(PointerEventData eventData)
    {
		/*
        offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
       // originalParent = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent.parent.parent);
        this.transform.position = eventData.position;
        */

		tempClone = Instantiate (gameObject);
		tempClone.GetComponent<InventorySlot> ().itemInfo = itemInfo;
		tempClone.GetComponent<Image> ().sprite = itemInfo.Item_Object.Item_Sprite;
		Debug.Log (tempClone.GetComponent<InventorySlot> ().itemInfo);
		tempClone.transform.SetParent (this.transform.parent.parent.parent.parent);
		tempClone.transform.position = eventData.position;

		offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
		// originalParent = this.transform.parent;
		/*
		this.transform.SetParent(this.transform.parent.parent.parent.parent);
		this.transform.position = eventData.position;
		*/
    }

    public void OnDrag(PointerEventData eventData)
    {
		/*
        this.transform.position = eventData.position - offset;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        */

		//this.transform.position = eventData.position - offset;
		tempClone.transform.position = eventData.position - offset;
		tempClone.GetComponent<CanvasGroup>().blocksRaycasts= false;

		//gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		if (eventData.pointerEnter == null && !itemInfo._isEquiped) {
			originalParent.GetComponent<Inventory>().rightClickItem =this;
			originalParent.GetComponent<Inventory>().ammountOptionPanel.gameObject.SetActive(true);
			originalParent.GetComponent<Inventory>().ammountOptionPanel._CalledToDrop(true);

		} 
		this.transform.SetParent(originalParent);
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    #endregion

}
