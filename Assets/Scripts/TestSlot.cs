using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using Items_And_Inventory;

public class TestSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public SpriteRenderer icon;
	public Sprite stuff;
	public Image image;
	public Transform rootTransform;
	public Transform parentTransform;

	string id = "Penis";
	int qty = 100;

	public string Item_ID
	{
		get { return id; }
		set { id = value; }
	}

	public int Item_Quantity
	{
		get { return qty; }
		set { qty = value; }
	}

	public int Is_On_Slot
	{
		get { return 99; }
		set { qty = value; }
	}

	public void UpdateQuantity(int q)
	{
		qty = q;
	}

	public void InitializeItemSlot(IStoreable itm)
	{
		
	}

	public void Clicked(int i)
	{
		image.sprite = stuff;
	}
		
	public void OnBeginDrag(PointerEventData eventData)
	{
		transform.SetParent(rootTransform);
		transform.position = eventData.position;
	}
		
	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(eventData.pointerEnter == null)
		{
			gameObject.transform.SetParent(parentTransform);
			gameObject.transform.position = parentTransform.position;
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
	}

	void OnEnable()
	{
		//TestDriver.Bleep += Clicked;
	}

	// Use this for initialization
	void Start () 
	{
		parentTransform = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
