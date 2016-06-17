using UnityEngine;
using System.Collections;

public class TestGameManager : MonoBehaviour
{
	private bool isActive = false;

	// Use this for initialization
	void Start ()
	{
        InventoryMenu.Instance.StopListeningToEvent(SquadManager.Instance.FocusedUnit.gameObject);
        InventoryMenu.Instance.ListenToEvent(SquadManager.Instance.FocusedUnit.gameObject);
	}

	public void ExitGame()
	{
		Application.Quit();
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
                SquadManager.Instance.FocusedUnit.GetComponent<CharacterInventory>().InitializeMenu();
			}
			else
			{
				isActive = false;
            
				InventoryMenu.Instance.menuPrefab.SetActive(isActive);	
			}
		}
	}
}
