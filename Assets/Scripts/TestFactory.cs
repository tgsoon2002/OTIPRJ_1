using UnityEngine;
using System.Collections;

public class TestFactory : MonoBehaviour 
{
	public Sprite aIcon;
	public Sprite bIcon;

	public GameObject pRef;

	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	public void MakeItemA()
	{
		BaseItem bItem = new BaseItem(01, "D", "DDD", aIcon, 1, Items_And_Inventory.BaseItemType.CONSUMABLE, true, 3);
		ItemInfo itemA = new ItemInfo(bItem, 1);

		pRef.GetComponent<CharacterInventory>().AddItem(itemA);
	
	}

	public void MakeItemB()
	{
		BaseItem bItem = new BaseItem(02, "C", "CCC", bIcon, 1, Items_And_Inventory.BaseItemType.NON_CONSUMABLE, false, 12);
		ItemInfo itemB = new ItemInfo(bItem, 1);
		pRef.GetComponent<CharacterInventory>().AddItem(itemB);
	}
}

