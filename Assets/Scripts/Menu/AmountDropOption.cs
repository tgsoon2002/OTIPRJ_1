using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmountDropOption : MonoBehaviour {

	#region Data Members
	public JMInventoryMenu invRef;
	public InputField ammountText;
	int ammount;
	bool dropOption;

	#endregion

	#region Built-in Unity Methods
	// Use this for initialization
	void Start () {
		ammountText.onValueChanged.AddListener(delegate {
			ValueChangeUpdate();
		});
		gameObject.SetActive(false);
	}
	#endregion

	#region Public Methods

	/// <summary>
	/// Called by ItemOptionPanel.
	/// Move this ammount panel to item select by right click.
	/// set ammount to 1 and ammount text to 1. 
	/// </summary>
	/// <param name="drop">If set to <c>true</c> drop.</param>
	public void _CalledToDrop (bool drop){
		transform.position = invRef.rightClickItem.transform.position;
		dropOption = drop;
		ammountText.text = "1";
		ammount = 1;
	}



	/// <summary>
	/// When change value by increse/decrease button.
	/// If the ammount is more than item being select quantity. Then set ammount = slot quantity 
	/// If ammount + new value is small than item quan tity or large than 0. Then increate ammount by new value
	/// Set ammount text = ammount
	/// </summary>
	/// <param name="newVal">New value.</param>
	public void ChangeValueButton (int newVal){
		if (ammount > invRef.rightClickItem.ItemQuantity) {
			ammount = invRef.rightClickItem.ItemQuantity;
		}
		if ((ammount + newVal) > 0 && (ammount + newVal) < invRef.rightClickItem.ItemQuantity) {
			ammount += newVal;
		}
		ammountText.text = ammount.ToString();	
	}

	/// <summary>
	/// Confirms the button.
	/// Call inventory to drop itemSelect by right click base on dropOption and ammount.
	/// Then close ammount drop panel
	/// </summary>
	public void _Confirm_Button (){
		invRef.DropSelectedItem(dropOption,ammount);
		gameObject.SetActive(false);
	}

	/// <summary>
	/// Just close ammount drop panel.
	/// </summary>
	public void _Cancel_Button (){
		gameObject.SetActive(false);
	}
	#endregion

	#region Private Methods
	private void ValueChangeUpdate()
	{
		ammount = int.Parse(ammountText.text);
	}
	#endregion









}
