using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class QuickItem : MonoBehaviour, IDropHandler
{
    #region Data Members
		 
	public QuickBar quickBarMngr;
	public int? itemID;
    private ItemInfo itemRef;
    private Image slotIcon;
	private Sprite oriSlotIcon;
    private Text qtyGUI;
	private InventorySlot slotRef;

	#endregion

    #region Built-in Unity Methods

    // Use this for initialization
    void Start()
    {
		oriSlotIcon = gameObject.GetComponent<Image>().sprite;
        slotIcon = gameObject.GetComponent<Image>();
        qtyGUI = gameObject.transform.GetChild(0).GetComponent<Text>();
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    #endregion

    #region Public Methods

	//Checking when some object Drop on the quickItem(itemSlot)
    public void OnDrop(PointerEventData eventData)
    {
        //Declaring local variables
        ItemInfo temp;
        bool itemExistsOnSlot;
		slotRef = eventData.pointerDrag.GetComponent<InventorySlot>();
		temp = eventData.pointerDrag.GetComponent<InventorySlot>().Inventory_Item;
		itemExistsOnSlot = quickBarMngr.CheckIfItemOnBar(temp.Item_Object.Item_ID);

        if(itemExistsOnSlot)
        {
			quickBarMngr.CheckAndClearOtherSlots(temp.Item_Object.Item_ID);
        }
    
        itemRef = temp;
		slotIcon.sprite = itemRef.Item_Object.Item_Sprite;  // Set icon Sprite
        qtyGUI.text = itemRef.Item_Qty.ToString();	// Set quantity
		itemID = itemRef.Item_Object.Item_ID;		// set itemID to keep track
    }
	// Update infomation of the quickItem, remove if item is empty
    public void UpdateInfo()
    {
		Debug.Log(itemRef);
        //Declaring local variables
         if(itemRef.Item_Qty <= 0)
        {
			RemoveReference();
        }
        else
        {
            qtyGUI.text = itemRef.Item_Qty.ToString();
        }
    }
	// remove the reference of quickItem to ItemSlots
    public void RemoveReference()
    {
		slotIcon.sprite = oriSlotIcon;
		qtyGUI.text = "";
		itemRef = null;
		itemID = null;
    }

	public void UsingItem(int quantity){
		Debug.Log("this part of the code will active to use item and update item in the inventory");
		//slotRef.Inventory_Item.Item_Qty -= quantity;
		// this will update in character inventory.

	}
    #endregion

}
