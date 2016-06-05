using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathDefinition : MonoBehaviour {

	public Transform[] Points;
	bool setLine = false;

	public IEnumerator<Transform> GetPathsEnumerator(){

		if (Points == null || Points.Length < 1) 
			yield break;
	
		int direction = 1;
		int index = 0;
		while (true) 
		{
			yield return Points[index];
			if (Points.Length == 1) 
				continue;
			if(index <= 0)
				direction = 1;
			else if (index >= Points.Length -1) 
				direction = -1;
			
			index = index + direction;
		}
	}
		

	void OnDrawGizmosSelected()
	{
		if (Points == null || Points.Length<2) 
			return;

		for (int i = 0; i < Points.Length; i++) {
			//Debug.Log("Draw");
			//Gizmos.DrawLine(Points[i].position,Points[i].position);
		}
	}

	/// <summary>
	/// Appears the line. If path have not been setup, then create path.
	/// else enable the line renderer
	/// </summary>
	public void AppearLine() 
	{
		if (setLine) {
			GetComponent<LineRenderer>().enabled = true;
		}
		else{
			Debug.Log("draw line");
			GetComponent<LineRenderer>().enabled = true;
			Vector3[] newPath = new Vector3[Points.Length] ;
			for (int i = 0; i < Points.Length; i++) 
				newPath[i] = Points[i].position;
			GetComponent<LineRenderer>().SetPositions(newPath)	;
			setLine = true;
		}
	}

	/// <summary>
	/// Hides the line.Disable the line renderer
	/// </summary>
	public void HideLine() 
	{
		GetComponent<LineRenderer>().enabled = false;
	}
}
