using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ItemInterface;

public class JInventoryMenu : MonoBehaviour
{
	#region Data Members

	private List<GameObject> itemSlots;

	public Text weightText;
	public GameObject lootPrefab;
	public GameObject itemSlotPrefab;
	public GameObject rightClickedItemSlot;
	public GameObject itemOptionPanel;
	public GameObject amountOptionDropBox;

	private static JInventoryMenu _instance;

	#region Test Data Members

	public NinjaMan ninjaPlayer;

	#endregion

	#endregion

	#region Setters & Getters

	public static JInventoryMenu Instance
	{
		get { return _instance; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		itemSlots = new List<GameObject>();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Creates an Item Slot in the menu. Called by
	/// CharacterInventory whenever an item is added
	/// via menu operations, or the menu is being 
	/// populated.
	/// </summary>
	/// <param name="item">Item.</param>
	public void CreateSlot(IContainable item)
	{
		//This prefab contains two GameObjects:
		//1 - The 'Item Slot' GameObject
		//2 - The 'Item' GameObject
		GameObject slot = Instantiate(itemSlotPrefab);

		//Get the Component of the 'Item' to initialize its data.
		slot.transform.GetComponentInChildren<ISlottable>().Initialize_Slot(item);

		//Set transform of the 'Item Slot' to the Menu Canvas.
		slot.transform.SetParent(slot.transform);

		//Append new item to the list.
		itemSlots.Add(slot);
	}

	/// <summary>
	/// Removes the slot.
	/// </summary>
	/// <param name="toRemove">To remove.</param>
	public void RemoveSlot(IContainable toRemove)
	{
		
	}

	#endregion

	#region Private Methods

	#endregion
}
