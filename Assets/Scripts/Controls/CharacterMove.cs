using UnityEngine;
using System.Collections;

public class CharacterMove : Command
{
	#region Data Members

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	#endregion

	#region Public Methods

	public CharacterMove() : base()
	{
		base.commandName = "Move Left";
		base.defaultKeyBind.Add(KeyCode.A);
		base.defaultKeyBind.Add(KeyCode.D);
		PlayerPrefs.SetInt("MoveCharLeft", (int)KeyCode.A);
		PlayerPrefs.SetInt("MoveCharRight", (int)KeyCode.D);
	}

	public void SetCustomMove(KeyCode key, int dir)
	{
		if(dir == 0)
		{
			PlayerPrefs.SetInt("MoveCharLeft", (int)key);
		}
		else
		{
			PlayerPrefs.SetInt("MovecharRight", (int)key);
		}

		base.customKeyBind.Add(key);
	}

	public override void Execute(GameObject actor)
	{
		//
	}
		
	#endregion

	#region Private Methods

	#endregion
}
