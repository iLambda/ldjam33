
using System;
using UnityEngine;


public class GenericAgent: MonoBehaviour
{
	//Attributes
	public enum States { Idle, Move};
	public float speed;
	public States state;
	public GameObject target;
	public string targetTag;
	public GameObject obstacle;
	public bool blockedbyobstacle = false;
    public int healthPoints;
    public int humanityRate;
    public double cooldown;
    

    //temp attributes
	public Vector3 nextPos = Vector3.zero;


	public void Update(){
        if (cooldown >= -1)
        {
            cooldown -= UnityEngine.Time.deltaTime; // TODO decrement temporally
        }
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
			Debug.LogError ("Warning encountered state default in agent FSM");
			break;
		}

        LiveOrDie();
	}

	public void OnTriggerStay(Collider other){ //TODO create object collider
        //Debug.Log("I " + targetTag + "saw " + other.tag);
        if (other.gameObject.CompareTag(targetTag))
        {
            float distance = Vector3.Distance(transform.position, other.attachedRigidbody.position);
            GenericWeapon usedWeapon = Attack(distance);
            GenericAgent otherAgent = other.gameObject.GetComponent<GenericAgent>();
            //Debug.Log("generic agent is" + otherAgent.name);
            if ((usedWeapon != null) && (otherAgent != null))
            {
                if (usedWeapon.Contagion > 0)
                {
                    otherAgent.humanityRate -= usedWeapon.Contagion;
                    //Debug.Log("Humanity left" + otherAgent.humanityRate);
                }
                otherAgent.healthPoints -= usedWeapon.Damages;
                //Debug.Log("Lifepoints left" + otherAgent.healthPoints);
            }
        }
        else
        {
            //Debug.Log("I, "+ this.name +" am pacifist towards " + other.tag);
        }
	} 

    public virtual void Move()
    {
		if ((nextPos.Equals(Vector3.zero)) || Vector3.Distance(transform.position, nextPos) < 0.25f)
        {
            //TODO write random move function + obstacle avoidance
            int r = UnityEngine.Random.Range(1, 3);
            double theta = UnityEngine.Random.Range(0, 360) * Math.PI / 180.0;
            float a = (float)Math.Cos(theta) * r;
            float b = (float)Math.Sin(theta) * r;
			nextPos = new Vector3(transform.position.x + a, transform.position.y, transform.position.z + b);
			transform.LookAt(nextPos);
        }
        else
        {
			transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
        }

    }

    public virtual GenericWeapon Attack(float distance)
    {
        //does nothing
        return null;
    }

    public virtual void LiveOrDie()
    { //does nothing    
	}
}

