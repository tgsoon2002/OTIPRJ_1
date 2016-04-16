using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;

namespace CommandsNameSpace
{
	public abstract class Command
	{
		#region Data Members

		protected string commandName;
		protected CharacterInputs cmdType;

		#endregion

		#region Setters & Getters

		public string Command_Name
		{
			get { return commandName; }
			set { commandName = value; }
		}

		public CharacterInputs Commmand_Type 
		{
			get { return cmdType; }
			set { cmdType = value; }
		}

		#endregion

		#region Built-in Unity Methods

		//None

		#endregion

		#region Public Methods

		public Command()
		{
			commandName = string.Empty;
			cmdType = 0;
		}

		public virtual void Execute(CharacterInputs input, Stack<CharacterInputs> prevInput, GameObject obj)
		{
			//Override this later
		}

		#endregion

		#region Private Methods

		#endregion
	}

	public class MoveLeft : Command
	{
		#region Data Members

		#endregion

		#region Setters & Getters

		#endregion

		#region Built-In Unity Methods

		#endregion

		#region Public Methods

		public override void Execute (CharacterInputs input, Stack<CharacterInputs> prevInput, GameObject obj) 
		{
			if (base.cmdType == input) 
			{
				float tmp = obj.GetComponent<BasePlayerCharacter> ().CharacterStats.AttributeValue (6);

				if (prevInput.Peek () == CharacterInputs.Character_Move_Left) {
					// TBD Code for dashing left
				} 
				else if (/*TBD*/true)
				{

				}

				// This line TBD
				//obj.transform.position += new Vector3()

			}
		}

		#endregion

		#region Private Methods

		#endregion

		#region Helper Classes/Structs

		#endregion
	}

	public class MoveRight : Command
	{
		#region Data Members

		#endregion

		#region Setters & Getters

		#endregion

		#region Built-In Unity Methods

		#endregion

		#region Public Methods

		public override void Execute (CharacterInputs input, Stack<CharacterInputs> prevInput, GameObject obj) {
			if (base.cmdType == input) 
			{
				float tmp = obj.GetComponent<BasePlayerCharacter> ().CharacterStats.AttributeValue (6);

				if (prevInput.Peek () == CharacterInputs.Character_Move_Right) 
				{
					// TBD Code for dashing right
				}

				// This line TBD
				//obj.transform.position += new Vector3()

			}
		}

		#endregion

		#region Private Methods

		#endregion

		#region Helper Classes/Structs

		#endregion
	}
}