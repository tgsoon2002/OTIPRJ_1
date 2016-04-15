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
		protected List<CharacterInputs> cmdType;

		#endregion

		#region Setters & Getters

		public string Command_Name
		{
			get { return commandName; }
			set { commandName = value; }
		}


		#endregion

		#region Built-in Unity Methods

		//None

		#endregion

		#region Public Methods

		public Command()
		{
			commandName = string.Empty;
			cmdType = new List<CharacterInputs>();
		}

		public virtual void Execute(CharacterInputs input, Stack<CharacterInputs> prevInput, GameObject obj)
		{
			//Override this later
		}

		#endregion

		#region Private Methods

		#endregion
	}

	public class MoveCommand : Command
	{
		#region Data Members

		private float direction;

		#endregion

		#region Setters & Getters

		#endregion

		#region Built-in Unity Methods

		#endregion

		#region Public Methods

		public MoveCommand() : base()
		{
			cmdType.Add(CharacterInputs.Character_Move_Left);
			cmdType.Add(CharacterInputs.Character_Move_Right);
			direction = 0.0f;
		}

		public override void Execute(CharacterInputs input, Stack<CharacterInputs> prevInput, GameObject obj)
		{
			base.Execute(input, prevInput, obj);

			if(input == CharacterInputs.Character_Move_Left)
			{
				direction = -1.0f;
			}
	
			if(input == CharacterInputs.Character_Move_Right)
			{
				direction = 1.0f;
			}
				
			if(prevInput.Count > 0 && prevInput.Peek() == CharacterInputs.Character_Lock_On)
			{
				
			}
			else
			{
				
			}
		}

		#endregion

		#region Private Methods

		private void TurnLeft()
		{
			
		}

		private void TurnRight()
		{
			
		}

		#endregion
	}
}

