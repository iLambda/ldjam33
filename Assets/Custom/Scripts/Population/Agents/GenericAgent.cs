//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34209
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;


public class GenericAgent: MonoBehaviour
{
	//Attributes
	public enum States { Idle, Move};
	public float speed;
	public GenericWeapon weapon;
	public States state;
	public GameObject target;
	public string targetTag;
	public GameObject obstacle;
	public bool blockedbyobstacle = false;
	public Vector3 nextpos;
    public int healthPoints;

	public void Update(){
		switch (state)
		{
		case States.Idle:
			state = States.Move;
			break;
				
		case States.Move:
			Move();
			state = States.Move;
			break;

		default:
			state = States.Idle;
			Console.Write ("Warning encountered state default in agent FSM");
			break;
		}

        if (healthPoints < 0)
        {
            Die();
        }
	}
	public void OnTriggerEnter(Collider other){ //TODO create object collider
		if(other.gameObject.CompareTag ( targetTag )){
			Attack ();
		}
	}

    public virtual void Move()
    {
        //does nothing
    }

    public virtual void Attack()
    {
        //does nothing
    }

    public virtual void Die()
    { //does nothing    
	}
}

