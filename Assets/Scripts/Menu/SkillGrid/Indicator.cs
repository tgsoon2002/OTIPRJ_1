using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Indicator : MonoBehaviour {

	public enum FollowType
	{
		MoveTowards,
		Lerp
	}

	public FollowType type = FollowType.MoveTowards;
	public SkillNode nodeWillBeUnlock;
	public PathDefinition path;

	public float  speed = 1;
	public float maxDistanceToGoal = .1f;
	bool Moving = false;

	private IEnumerator<Transform> _currentPoint;

	void Start(){
		if (path== null) {
			Debug.LogError("Path cannot be null", gameObject);
			return ;
		}

//		_currentPoint = path.GetPathsEnumerator();
//		_currentPoint.MoveNext();
//		if (_currentPoint.Current == null) 
//			return;
//
//		transform.position = _currentPoint.Current.position;
	}

	void Update() {
		if (_currentPoint == null || _currentPoint.Current == null) 
		{
			return ;
		}

		if (Moving) {
			transform.GetComponent<TrailRenderer>().enabled = true;
			if (type == FollowType.MoveTowards) 
				transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime*speed);
			else if(type == FollowType.Lerp)
				transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime*speed);

			// check if reach the new node.
			var distanceSquare = (transform.position - _currentPoint.Current.position).sqrMagnitude;
			if (distanceSquare < maxDistanceToGoal* maxDistanceToGoal) {
				if (_currentPoint.Current.gameObject.name== "End") {
					// stop moving
					Moving = false;
					// reset trail
					transform.GetComponent<TrailRenderer>().Clear();
					transform.GetComponent<TrailRenderer>().enabled = false;
					nodeWillBeUnlock.UnlockedState = true;
				}	
				_currentPoint.MoveNext();
			}
		}
	}

	public void SetNewPath(PathDefinition newPath, SkillNode unlockNode){

		nodeWillBeUnlock = unlockNode;

		path = newPath;
		_currentPoint = path.GetPathsEnumerator();
		_currentPoint.MoveNext();
		if (_currentPoint.Current == null) 
			return;
		transform.position = _currentPoint.Current.position;

		Moving = true;
	}
}
