using UnityEngine;
using System.Collections;

public class TestGameManager : MonoBehaviour
{
	private bool isActive = false;

	// Use this for initialization
	void Start ()
	{
		InventoryMenu.Instance.StopListeningToEvent(SquadManager.Instance.Current_Character);
		InventoryMenu.Instance.ListenToEvent(SquadManager.Instance.Current_Character);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			if(!isActive)
			{
				isActive = true;
				InventoryMenu.Instance.menuPrefab.SetActive(isActive);
				SquadManager.Instance.Current_Character.GetComponent<CharacterInventory>().InitializeMenu();
			}
			else
			{
				isActive = false;
				SquadManager.Instance.Current_Character.GetComponent<CharacterInventory>().UpdateInventory();
				InventoryMenu.Instance.menuPrefab.SetActive(isActive);	
			}

		}
	}
}
