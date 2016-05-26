using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	void Start(){
		gameObject.SetActive(false);
	}

	public void ShowMap(){
		gameObject.SetActive(true);
	}

	public void HideMap(){
		gameObject.SetActive(false);
	}

}
