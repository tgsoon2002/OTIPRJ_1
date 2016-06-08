using UnityEngine;
using System.Collections;
using Player_Info;

public class TestPlayer : MonoBehaviour, ICarryable, IControllable
{
	private int maxWeight = 100;
	private bool isSelected = true;

	public int Player_Max_Weight
	{
		get { return maxWeight; }
		set { maxWeight = value; }
	}

	public bool Character_Is_Selected
	{
		get { return isSelected; }
		set { isSelected = value; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
