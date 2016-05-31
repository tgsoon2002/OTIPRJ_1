using UnityEngine;
using System.Collections;

public class SquadManager : MonoBehaviour
{
	public GameObject character;
	private static SquadManager _instance;

	public static SquadManager Instance
	{
		get { return _instance; }
	}

	public GameObject Current_Character
	{
		get { return character; }
		set { character = value; }
	}

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
