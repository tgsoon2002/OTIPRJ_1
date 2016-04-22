using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	#region Data Members
	private List<GameObject> enemyList;
	public Sprite enemyMarker;
	private static EnemyManager instance;
	#endregion

	#region Setters & Getters
	public static EnemyManager Instance {
		get { return instance; }
	}

	#endregion

	#region Built-In Unity Methods

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion

	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		enemyList = new List<GameObject> ();
		/*TestEnemy[] testEnemies = FindObjectOfType (typeof(TestEnemy))as TestEnemy[];

		foreach (TestEnemy tmp in testEnemies) 
		{
			enemyList.Add (tmp);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
