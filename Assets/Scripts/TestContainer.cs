using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TestContainer : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("Ass n Tits!");
		eventData.pointerDrag.transform.SetParent(transform);
		eventData.pointerDrag.transform.position = gameObject.transform.position;
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
