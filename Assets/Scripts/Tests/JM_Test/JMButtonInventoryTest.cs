using UnityEngine;
using System.Collections;

public class JMButtonInventoryTest : MonoBehaviour 
{
	public GameObject temp;
	public GameObject inventory;
	private JMInventoryMenu invRef;

	private JMItemInfo test;
	private int tempID = 1;
	private int tempType = 0;
	bool blah = false;

	// Use this for initialization
	void Start () 
	{
		invRef = inventory.GetComponent<JMInventoryMenu>();
		//invRef.Max_Weight = 10;
	}


	public void AddStuff()
	{

		//BaseItem newItem = ItemDatabase.Instance.GetItem(tempID, tempType);
		test = new JMItemInfo(0, tempID, 1, tempType);

		Debug.Log (test.Item_Info.Item_Name);
		Debug.Log (test.Item_Info.Base_Item_Type);
		Debug.Log (test.Item_Info.Item_Weight);
		Debug.Log (test.Item_Info.Item_Sprite);

		//GameObject newGameObject = Instantiate(temp);
		//newGameObject.transform.localScale = Vector3.one;
		//newGameObject.GetComponent<JMItemInfo>().Item_Info = newItem;
		//newGameObject.GetComponent<JMItemInfo>().Item_Qty = 1;


		//BaseItem newItem = new BaseItem(1, "Potion", "This sucks", 22, BaseItemType.CONSUMABLE, true, 1);

		if(!blah)
		{
			tempID+=6;
			//tempType++;
			blah = true;
		}
		else
		{
			tempID-=6;
			//tempType--;
			blah = false;
		}

		invRef.AddItem (test);
	}


}
