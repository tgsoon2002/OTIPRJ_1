using System;

namespace GameInputNameSpace
{
	public enum CharacterInputs
	{
		// These should be assigned numbers.
		Character_Move_Left = 0,  	//done
		Character_Move_Right = 1,	//done
		Character_Crouch ,       	//done
		Character_Primary_Weapon,	//done
		Character_Secondary_Weapon,	//done
		Character_Jump ,			//done
		Character_Lock_On,
		Character_Context_Action, 	//setup Command
		Character_Non_2D_Movement,
		Character_Switch_Left, 		// Done
		Character_Switch_Right, 	//Done
		Open_Character_Inventory,	//done
		Open_Settings,				//Setup command
		Open_Map,					//done
		Open_Skill_Grid,			//Setup command
		Open_Squad_Manager,			//Setup command
		Open_Journal,				//Setup command
		Character_MAX
	}

}