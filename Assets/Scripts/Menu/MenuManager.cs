using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	#region Data Members
	public Transform characterModel;
	public GameObject menu;
	public CharacterBlock charModelManager;
	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods
	// Use this for initialization
	void Start () {
		//menu = transform.FindChild("Menu");
		menu.SetActive (false) ;
		characterModel.gameObject.SetActive (false) ;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (menu.activeSelf) {
				
				MenuVisibility (false);
			} else {
				
				MenuVisibility (true);
			}
		}
	}
	#endregion

	#region Public Methods
	public void UpdateCharacterBlock(){
		charModelManager.GetComponent<CharacterBlock>().UpdateChar();
	}
	#endregion

	#region Private Methods
	/// <summary>
	/// this used to turn the model on and off.
	/// </summary>
	/// <param name="visible">If set to <c>true</c> visible.</param>
	public void MenuVisibility (bool visible){
		menu.SetActive (visible) ;
		characterModel.gameObject.SetActive (visible) ;
		charModelManager.UpdateChar();


	}
	#endregion


}
