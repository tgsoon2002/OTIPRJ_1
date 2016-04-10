using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	#region Data Members
	public Transform characterModel;
	private Transform menu;
	public CharacterBlock charModelManager;
	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods
	// Use this for initialization
	void Start () {
		menu = transform.FindChild("Menu");
		menu.gameObject.SetActive (false) ;
		characterModel.gameObject.SetActive (false) ;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (Time.timeScale == 0) {
				Time.timeScale =1.0f;
				MenuVisibility (false);
			} else {
				Time.timeScale =0.0f;
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
		characterModel.gameObject.SetActive (visible) ;
		charModelManager.UpdateChar();
		menu.gameObject.SetActive (visible) ;

	}
	#endregion


}
