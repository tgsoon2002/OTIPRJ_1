using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSquadManager : MonoBehaviour {
	#region Data Members
	public List <GameObject> charList;
	public delegate void AICommands (int i);
	public static event AICommands commandEvents;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-In Unity Methods

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	#endregion

	#region Public Methods
	public static void TriggerCommand (int i)
	{
		Debug.Log (i);
		commandEvents (i);
		/*
		if (commandEvents != null) 
		{
			Debug.Log ("CAN'T STUMP THE TRUMP!");
			commandEvents (i);
		}
		*/
	}

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion



}
