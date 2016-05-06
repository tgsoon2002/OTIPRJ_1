using UnityEngine;
using System.Collections;

public class JMMenuManager : MonoBehaviour 
{
	#region Data Members
	private static JMMenuManager _instance;
	public GameObject inventoryReference;

	#endregion

	#region Setters & Getters
	public static JMMenuManager Instance
	{
		get 
		{ 
			if(!_instance)
			{
				_instance = FindObjectOfType(typeof (JMMenuManager)) as JMMenuManager;

				if(!_instance)
				{
					Debug.LogError("No JMMenuManager GameObject detected!");
				}
			}

			return _instance;
		}
	}

	#endregion

	#region Built-In Unity Methods

	void Awake ()
	{
		
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	public JMInventoryMenu GetInventory ()
	{

		return inventoryReference.GetComponent<JMInventoryMenu> ();
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion

}
