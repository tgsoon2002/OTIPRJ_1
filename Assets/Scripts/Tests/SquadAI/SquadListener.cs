using UnityEngine;
using System.Collections;

public class SquadListener : MonoBehaviour {
	#region Data Members
	public GameObject captain;
	private int commandType = -1;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-In Unity Methods
	void OnEnable ()
	{
		TestSquadManager.commandEvents += ReceiveCommand;
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		switch (commandType) 
		{
		case 0: 
			Vector3 posA = gameObject.transform.position;
			Vector3 posB = captain.transform.position;
			gameObject.transform.position = Vector3.Lerp (posA, posB, Time.deltaTime * 1.5f);
				break;
			case 1: 
			
				break;		

		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods
	void ReceiveCommand(int i)
	{
		commandType = i;
	}

	#endregion

	#region Helper Classes/Structs

	#endregion
}
