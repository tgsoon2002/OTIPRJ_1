using UnityEngine;
using System.Collections;
using GameInputNameSpace;

public class Commands : MonoBehaviour
{
	#region Data Members

	delegate void CommandMethods(CharacterInputs cmd, int type);
	CommandMethods commands;
	static Commands instance;
	GameObject objToControl;

	#endregion

	#region Setters & Getters

	public static Commands Instance
	{
		get { return instance; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		commands += MoveLeft;
		commands += MoveRight;
		commands += Jump;
		commands += Crouch;
		commands += PrimaryWeapon;
		commands += SecondaryWeapon;
	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	#region GUICommands

	public void GetInput(CharacterInputs cmd)
	{
		
	}

	public void GUIAccept(CharacterInputs cmd)
	{
		
	}

	public void GUIMoveUp(CharacterInputs cmd)
	{
		
	}

	public void GUIMoveDown(CharacterInputs cmd)
	{
		
	}
		
	public void GUIMoveLeft(CharacterInputs cmd)
	{
		
	}

	public void GUIMoveRight(CharacterInputs cmd)
	{

	}

	#endregion

	#region Character Commands

	public void SetGameObjectToControl(GameObject obj)
	{
		objToControl = obj;
	}

	public void ReceiveInput(CharacterInputs cmd, int type)
	{
		commands(cmd, type);
	}

	public void MoveLeft(CharacterInputs cmd, int type)
	{
		
	}

	public void MoveRight(CharacterInputs cmd, int type)
	{
		
	}

	public void Jump(CharacterInputs cmd, int type)
	{
		
	}

	public void Crouch(CharacterInputs cmd, int type)
	{
		
	}

	public void PrimaryWeapon(CharacterInputs cmd, int type)
	{
		
	}

	public void SecondaryWeapon(CharacterInputs cmd, int type)
	{

	}

	#endregion

	#endregion

	#region Private Methods

	#endregion

}
