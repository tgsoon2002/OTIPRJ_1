using UnityEngine;
using System.Collections;

public class PlayerGuardian : BasePlayerCharacter {




	// Use this for initialization
	protected void Start () {
		//base.UpperArmMesh = 
		base.rig = GetComponent<Rigidbody>();
		base.anim = GetComponent<Animator>();
	}
	 

	public override void MoveThisUnit(float direction){
//		base.rig.velocity = new Vector3(direction * base.statTable.mvSpeed,rig.velocity.y);
//		Debug.Log(direction* statTable.mvSpeed);
//		base.anim.SetFloat("speed",direction* base.statTable.mvSpeed) ;
//		Debug.Log(statTable.GetStats());
		base.MoveThisUnit(direction);
//		base.anim.SetFloat("speed",direction* base.statTable.mvSpeed) ;
	}







}
