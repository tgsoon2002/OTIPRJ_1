using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {

	#region Member
	public Animator anim;
	public Rigidbody rig;
	protected UnitStats statTable;
	[SerializeField]
	protected string characterName;
	protected float sightRange;

	#endregion

	#region UnitBuiltInMethod

		
	#endregion

	public virtual UnitStats CharacterStats {
		get{return statTable;}
		set{statTable = value;}
	}

	#region MainMethod
	public virtual void MoveThisUnit(float direction){
		rig.velocity = new Vector3(direction * statTable.AttributeValue(6),rig.velocity.y);
		anim.SetFloat("speed",direction* statTable.AttributeValue(6)) ;
	}

	public virtual float PercentHealth() 
	{
		return statTable.AttributeValue(12) / statTable.AttributeValue(0);//(statTable.healthPoint / statTable.maxHealthPoint);
	}

	public virtual float PercentMana()  
	{
		return statTable.AttributeValue(13) / statTable.AttributeValue(1);
	}

	public virtual float PercentStamina()  
	{
		return statTable.AttributeValue(14) / statTable.AttributeValue(2);
	}


	public virtual bool IsDead()
	{
		if (statTable.AttributeValue(12) <= statTable.AttributeValue(0)) {
			return true;
		}
		return false;
	}

	public	virtual void TakeDamage(float damageValue){
		statTable.AttributeChange(statTable.AttributeValue(12)- damageValue,12) ;
	}

	public virtual void Kill()
	{
		statTable.AttributeChange(0.0f,0);
	}


#endregion



}