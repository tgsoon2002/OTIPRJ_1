using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailBlock : MonoBehaviour 
{

	#region Data Members
	public Image itemIcon;
	public Text infoText;
	public Text descText;

	#endregion

	#region Public Methods

	/// <summary>
	/// Updates the detail of detailBlock, base on item being select with left click.
	/// </summary>
	/// <param name="itemIndex">Item index.</param>
	/// <param name="Quantiy">Quantiy.</param>
	/// <param name="itemtype">Itemtype.</param>
	public void UpdateDetail (int itemIndex, int Quantiy, BaseItemType itemtype)
	{
		BaseItem tempItem =  ItemDatabase.Instance.GetItem(itemIndex,(int)itemtype);
		itemIcon.sprite = tempItem.Item_Sprite;
		descText.text = "Description : " + tempItem.Item_Description;
		infoText.text = "Type : " + tempItem.Base_Item_Type + 
			"\nPrice : " + tempItem.Item_Price ;
	}

	/// <summary>
	/// Updates the detail of detailBlock, base on item being select with left click.
	/// </summary>
	/// <param name="itemIndex">Item index.</param>
	/// <param name="Quantiy">Quantiy.</param>
	/// <param name="itemtype">Itemtype.</param>
	public void UpdateItemDetail (JMItemInfo item)
	{
		
		itemIcon.sprite = item.Item_Info.Item_Sprite;
		descText.text = "Description : " + item.Item_Info.Item_Description;
		infoText.text = "Type : " + item.Item_Info.Base_Item_Type + 
			"\nPrice : " + item.Item_Info.Item_Price ;
	}

	/// <summary>
	/// This function called to close the detail block
	/// </summary>
	public void CloseDetailWindow()
	{
		gameObject.SetActive(false)	;
	}

	#endregion
}
