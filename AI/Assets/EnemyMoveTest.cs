using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMoveTest : MonoBehaviour {
    float stamina;
    float staminaRegen;
    float staminaCap;
    bool detectedPlayer;
    float movespeed;
    float saveAmount;
    float staminaCost;
    float angleToEnemy;
    GameObject targetEnemy;
    Vector3 locationOfEnemy;
    string attackType;
    float maxDist;//max amount of distance enemy will move when there is no player character in sight.
    float middleX = 0.0f;//x value for middle of the map
    float middleY = 0.0f;//y value for middle of the map
public    List<GameObject> enemyList;
    // Use this for initialization
    void Start () {
        stamina = 200;
        staminaCap = 200;
        staminaRegen = 0.10f;
        saveAmount= 0.50f;
        enemyList = new List<GameObject>();
        staminaCost = 1.0f;
        attackType = "Melee";//adjust this portion later.
        movespeed = 1.0f;
        maxDist =18.0f;
    }
    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag=="Player")
        {
           enemyList.Add(coll.gameObject);
           Debug.Log(calculateDistance(coll.gameObject));
           //Debug.Log("I've got you in my sight!");
        }
        
    }
    void OnTriggerExit(Collider coll)
    {
        if(coll.tag == "Player")
        {
            enemyList.Remove(coll.gameObject);
        }
        Debug.Log("that weakling");
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        //print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
    }
    //list must contain at least one item.
    GameObject ChooseTarget(List<GameObject> listofEnemyInSight)
    {
        float shortest = 99999.0f;
        GameObject resultGameObject = gameObject;
        if (enemyList.Count>0)
        {
            for (int i = 0; i < enemyList.Count; i++)//max will be 3 anyways.
            {
                float distance = calculateDistance(enemyList[i]);
                if (distance < shortest)
                {
                    shortest = distance;
                    resultGameObject = enemyList[i];
                }
            }
        } 
        return resultGameObject;
    }
    //this function calculates diagonal distance between object and given object and returns diagonalSquared value.
    float calculateDistance(GameObject objectToCompare)
    {
        float diagonalSquared = 0.0f;
        float x0= objectToCompare.transform.position.x;
        float y0= objectToCompare.transform.position.y;
        float x1=gameObject.transform.position.x; 
        float y1=gameObject.transform.position.y;
        diagonalSquared = (y1 - y0)* (y1 - y0) + (x1 - x0)* (x1 - x0); // we can't use "^" for float values. 
        return diagonalSquared;
    }
    float calculateAngle(GameObject targetObject)
    {
        float value=0.0f;
        float distance = Mathf.Sqrt(calculateDistance(targetObject));

        float xDiff=targetObject.transform.position.x-gameObject.transform.position.x;
        float yDiff=targetObject.transform.position.y-gameObject.transform.position.y;
        value=Mathf.Asin(xDiff / distance);
        //Mathf.Acos(yDiff / distance);//In theory, Acos and Asin should give us same result.
        return value; // in degree
    }
    void MeleeEnemyAI()
    {
        //if enemy is in sight
        if (enemyList.Count > 0)
        {
            //ChooseTarget
            //ChooseSkill
        }
        else//if enemy is not in sight
        {


            if (stamina <= stamina * saveAmount / 100)// do not move and recover stamina.
            {
                stamina += staminaRegen;
            }
            else // if the object have enough stamina.
            {
                //Navmesh(point)-TODO, follow tutorial.
                Vector3 newLocation = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                if (gameObject.transform.position.x < middleX)// if locatetd on left side.
                {
                    float extraStamina=stamina - (stamina * saveAmount);//available stamina to spend;
                    float xAmount;
                    float yAmount;
                    xAmount=maxDist*Mathf.Sin(angleToEnemy);
                    yAmount=maxDist*Mathf.Cos(angleToEnemy);
                    newLocation.x+=xAmount;//need to think about how much to change.
                    newLocation.y+=yAmount;//need to think about how much to change.
                }
                else//if located on the right side
                {

                }
                gameObject.transform.position = newLocation;
            }
        }
    }
    // Update is called once per frame
    void Update ()
    {

        targetEnemy = ChooseTarget(enemyList); // it is dumb AI, it will always target the closest enemy regardless of HP status.
	}
}
