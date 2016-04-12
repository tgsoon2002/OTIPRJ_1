using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Command : IControllable
{
    #region Data Members

	protected bool isController;
	protected List<KeyCode> defaultKeyBind;
	protected List<KeyCode> customKeyBind;
	protected string commandName;
	protected int maxKeyBindSize;

    #endregion

    #region Setters & Getters
    
	public bool Is_Controller
	{
		get { return isController; }
		set { isController = value; }
	}
		
	public string Command_Name
	{
		get { return commandName; }
		set { commandName = value; }
	}

	protected int Max_Key_Bind_Size
	{
		set { maxKeyBindSize = value; }
	}

    #endregion

    #region Built-in Unity Methods
    
	//None

    #endregion

    #region Public Methods
    
	public Command()
	{
		isController = false;
		defaultKeyBind = new List<KeyCode>();
		customKeyBind = new List<KeyCode>();
	}
		
	public virtual void Execute(GameObject obj)
	{
		
	}
		
    #endregion

    #region Private Methods
    		
    #endregion
}