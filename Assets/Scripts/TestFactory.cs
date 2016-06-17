using UnityEngine;
using System.Collections;

public class TestFactory : MonoBehaviour 
{
	public Sprite aIcon;
	public Sprite bIcon;
	public Sprite cIcon;
	public Sprite dIcon;
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
		BaseItem bItem = new BaseItem(02, "C", "CCC", bIcon, 1, Items_And_Inventory.BaseItemType.NON_CONSUMABLE, true, 12);
		ItemInfo itemB = new ItemInfo(bItem, 1);
		pRef.GetComponent<CharacterInventory>().AddItem(itemB);
	}

	public void MakeItemC()
	{
		BaseItem cItem = new BaseItem(03, "A", "AAA", cIcon, 1, Items_And_Inventory.BaseItemType.EQUIPMENT, false, 20);
		ItemInfo itemC = new ItemInfo(cItem, 1);
		pRef.GetComponent<CharacterInventory>().AddItem(itemC);
	}

	public void MakeItemD()
	{
		BaseItem dItem = new BaseItem(03, "Z", "AAAZ", dIcon, 1, Items_And_Inventory.BaseItemType.EQUIPMENT, false, 0);
		ItemInfo itemD = new ItemInfo(dItem, 1);
		pRef.GetComponent<CharacterInventory>().AddItem(itemD);

	}
}
