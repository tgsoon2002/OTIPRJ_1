using UnityEngine;
using System.Collections;

public class ButtonInventoryTest : MonoBehaviour 
{
    public GameObject temp;
    public GameObject inventory;
    private Inventory invRef;

    private int tempID = 1;
    private int tempType = 0;
    bool blah = false;

	// Use this for initialization
	void Start () 
    {
	    invRef = inventory.GetComponent<Inventory>();
        //invRef.Max_Weight = 10;
	}


    public void AddStuff()
    {

		BaseItem newItem = ItemDatabase.Instance.GetItem(tempID, tempType);
		GameObject newGameObject = Instantiate(temp);
		newGameObject.transform.localScale = Vector3.one;
		newGameObject.GetComponent<ItemInfo>().Item_Object = newItem;
        newGameObject.GetComponent<ItemInfo>().Item_Qty = 1;
        Debug.Log(newGameObject);

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

        if(!invRef.AddItem(newGameObject.GetComponent<ItemInfo>()))
        {
            Debug.Log("Ey yo, HOL UP!");
        }
    }
	

}
