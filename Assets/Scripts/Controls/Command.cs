using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameInputNameSpace;

public class Command : IControllable
{
    #region Data Members
	protected string commandName;
    protected CharacterInputs charMovement;   

    #endregion

    #region Setters & Getters

    public CharacterInputs Command_Type
    {
        get { return charMovement; }
        set { charMovement = value; }
    }

    public string Command_Name
    {
        get { return commandName; }
        set { commandName = value; }
    }

    #endregion

    #region Built-in Unity Methods

    #endregion

    #region Public Methods
    public Command (string _commandName,  CharacterInputs _charMovement)
    {
        commandName = _commandName;
        charMovement = _charMovement;
    }

    public void Execute (GameObject obj)
    {

    }

    #endregion

    #region Private Methods

    #endregion
}

