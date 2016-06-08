using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Items_And_Inventory;

public class TestDriver : MonoBehaviour, IPointerClickHandler
{
	public GameObject subMenu;

	public delegate void Bloop(int i);
	public static event Bloop Bleep;


	public void OnPointerClick(PointerEventData eventData)
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(eventData.pointerCurrentRaycast.gameObject.GetComponent<ISlottable>() != null)
			{
				Debug.Log("Boop!");
				//gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

				Debug.Log(Bleep);
				Bleep(11);
				//subMenu.SetActive(true);
			}
			else
			{
				Debug.Log("Squeeze");
			}
		}
	}

	// Use this for initialization
	void Start () {
		Bleep += Privates;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Privates(int i)
	{
		Debug.Log("Eek: " + i);
	}
}
