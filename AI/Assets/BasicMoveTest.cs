using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BasicMoveTest : MonoBehaviour
{ 
    float distance = 17;
    public float stamina;
    bool dragging;
    float staminaRegen;
    float staminaCap;
    Vector3 temp=new Vector3();
    float staminaCost;
    // Use this for initialization
    void Start () {
        stamina = 190f;
        dragging = false;
        staminaRegen = 0.1f;
        staminaCap = 200f;
        staminaCost = 1.0f;
    }


    void OnMouseDown()
    {
        temp.x = gameObject.transform.position.x;
        temp.y = gameObject.transform.position.y;
    }
    void OnMouseDrag()//need to figure out why it does not work sometimes. for the worst case, need to use raycast and make my own mouse drag. :(
    {

        dragging = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (stamina >0)
        {
            //Debug.Log(temp.x==gameObject.transform.position.x);
            
            if (temp.x != gameObject.transform.position.x);//need to fix drag and not move case.
            {
                stamina -= staminaCost;
            }
            transform.position = objPosition;
            
            //Debug.Log(Stamina);
        }
        else
        {
            Debug.Log("not enough stamina.");
        }
        
        
    }
    void OnMouseUp()
    {
        dragging = false;
        
    }
	// Update is called once per frame
	void Update ()
    {
        if (dragging == false && stamina < staminaCap)
        {
            stamina += staminaRegen;
            //Debug.Log(Stamina);
        }
	
	}
}
