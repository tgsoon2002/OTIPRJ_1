using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour
{
    #region Data Members

    private BaseItem item;
    private int quantity;
    private Transform originalParent;
    private Vector2 offset;
  
    #endregion

    #region Setters & Getters

	public BaseItem Item_Object
    {
        get { return item; }
        set { item = value; }
    }

    public int Item_Qty
    {
        get { return quantity; }
        set { quantity = value; }
    }

    #endregion

    #region Built-in Unity Methods

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.Item_Sprite;
    }
	#endregion

   
   

  

}
