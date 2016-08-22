using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentScript : MonoBehaviour {
    public float stamina;
    public float staminaRegen;
    public float staminaCap;
    private bool detectedPlayer;
    private float minstaminaSaveAmount;//amount to save considering attack. i.e 0.5 for 50%.
    private float staminaSaveAmount;//amount to save for moving.
    private float staminaCost;//stamina cost per update when moving.
    private float angleToEnemy;
    public GameObject targetEnemy; //made it public for taunt.
    private Vector3 locationOfEnemy;
    public string attackType; //range char might need to changed to melee depends on the design.
    public float maxDist;//max amount of distance enemy will move when there is no player character in sight. //depends on CC, it may change
    public float targetX;//x value for targetLocation of the map
    public float targetY;//y value for targetLocation of the map. we will most likely not use Y. 
    public float targetZ; //z value for targetLocation of the map
    private bool outOfStamina;
    private NavMeshAgent agent;
    private Vector3 curpos;
    public List<GameObject> enemyList;
    public float destinationOffset;
    // Use this for initialization
    void Start()
    {
        
        stamina = 200f;
        staminaRegen = 8.5f;
        staminaCap = 300f;
        detectedPlayer = false;
        minstaminaSaveAmount = 0.1f;
        staminaSaveAmount = 0.8f;
        staminaCost = 2.50f;
        angleToEnemy = 0f;
        //targetEnemy;//initially none.
        //attackType = "Melee";//might want to read it from data. depends on enemy. for testing, Melee was used.
        maxDist = 40f;
        targetX = 0.0f;
        targetY = 0.0f;
        targetZ = -10.8f;
        locationOfEnemy = new Vector3(targetX, targetY, targetZ); //initailly given target location
        outOfStamina = false;
        agent = GetComponent<NavMeshAgent>();
        curpos = agent.transform.position;
        enemyList = new List<GameObject>();
        if (attackType == "Melee")//we can combine this if and else if we have attack range information on db, just read it from there.
        {
            destinationOffset = 2.0f;
        }
        else //case of ranged enemy
        {
            destinationOffset = 6.0f;//need to update it based on the attack range.
        }
        //Debug.Log(locationOfEnemy);
        agent.SetDestination(locationOfEnemy); //move to enemy location. in start, it will be 0.0,0.0,-10.8


    }

    float calculateDistance(Vector3 location)
    {
        float result;
        float x0 = location.x;
        float z0 = location.z;
        float x1 = gameObject.transform.position.x;
        float z1 = gameObject.transform.position.z;
        result = (z1 - z0) * (z1 - z0) + (x1 - x0) * (x1 - x0);
        result = Mathf.Sqrt(result);
        return result;
    }

    GameObject ChooseTarget()//choosing a target with in sight, determining with distance.
    {
        float shortest = 99999.0f;
        GameObject resultGameObject = gameObject;
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)//max will be 3 anyways.
            {
                float distance = calculateDistance(enemyList[i].transform.position);
                if (distance < shortest)
                {
                    shortest = distance;
                    resultGameObject = enemyList[i];
                }
            }
        }
        return resultGameObject;
    }
    void SetTarget()
    {
        if (targetEnemy.tag != "Enemy")
        {
            locationOfEnemy = targetEnemy.transform.position;
            agent.SetDestination(locationOfEnemy);
            targetX = locationOfEnemy.x;
            targetY = locationOfEnemy.y;
            targetZ = locationOfEnemy.z;
        }
        else // if there is no target enemy,set it to the default one.
        {
            agent.SetDestination(locationOfEnemy);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("onTriggerEnter Triggered");
        if (coll.tag == "Player")
        {
            enemyList.Add(coll.gameObject);
            detectedPlayer = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            enemyList.Remove(coll.gameObject);
        }
        //Debug.Log("that weakling");
    }
    bool isMoving()
    {
        //Debug.Log(curpos != agent.transform.position);
        return (curpos != agent.transform.position);
    }
    bool isOutofStamina()
    {
        if (stamina < staminaCap * minstaminaSaveAmount)
        {
            outOfStamina = true;
        }
        else if (stamina > staminaCap * staminaSaveAmount)
        {
            outOfStamina = false;
        }
        return outOfStamina;
    }
    // Update is called once per frame
    bool enoughDistance()
    {
        return (calculateDistance(locationOfEnemy) <= destinationOffset);
    }
    void Update ()
    {
        Debug.Log(stamina);
        #region Checking for target and setting the target
        targetEnemy = ChooseTarget();
        SetTarget();
        #endregion
        #region controls movement of agent.
        if (isOutofStamina() == true)
        {
            agent.Stop();
        }
        else if (enoughDistance() ==false && isOutofStamina() == false)
        {
            agent.Resume();
        }
        else if (enoughDistance()==true)
        {
            agent.Stop();
        }
        #endregion
        #region handles movement and stamina calc.
        if (isMoving() == true)
        {
            
            curpos = agent.transform.position;
            stamina -= staminaCost;
        }
        else
        {
            if (stamina < staminaCap - staminaRegen)
            {
                stamina += staminaRegen;
            }
        }
        #endregion
        #region debugging tool
        /*if(Input.GetMouseButtonDown(0)) // handling movement stuff. remove it later. only for debug.
         {
             agent.Resume();
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast(ray,out hit))//raycast stuff.
             { 
                 if (hit.collider.tag == "Ground")
                 {

                     agent.SetDestination(hit.point);
                     targetX = hit.point.x;
                     targetZ = hit.point.y;
                     Debug.Log(hit.point);
                 }
             }

         }*///end of debug stuff.
        #endregion
    }
}
