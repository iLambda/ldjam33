
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
    public float distanceToClosestSound;
    // The boundaries
    public Vector3 worldBoundsMin = new Vector3(-250,-1,-250);
    public Vector3 worldBoundsMax = new Vector3(250, -1, 250);

    //temp attributes
	public Vector3 nextPos = Vector3.zero;
	private static Collider worldCollider = null;

	public void Start() {
		if (GenericAgent.worldCollider == null) 
		{
			GameObject world = GameObject.Find("GameArea");
			if (world != null) 
			{
				GenericAgent.worldCollider = world.GetComponent<Collider>();
			}
		}
	}

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
        if (other.gameObject.CompareTag(targetTag))
        {
            float distance = Vector3.Distance(transform.position, other.attachedRigidbody.position);
            GenericWeapon usedWeapon = Attack(distance);
            GenericAgent otherAgent = other.gameObject.GetComponent<GenericAgent>();
            if ((usedWeapon != null) && (otherAgent != null))
            {
                if (usedWeapon.Contagion > 0)
                {
                    otherAgent.humanityRate -= usedWeapon.Contagion;
                }
                otherAgent.healthPoints -= usedWeapon.Damages;
            }
        }
	}

	public void SetDestination(Vector3 destination) 
	{
		nextPos = destination;
		float tempX = transform.rotation.eulerAngles.x;
		transform.LookAt(nextPos);
		// compensating mesh flaw, it's awful, but it's works
		transform.rotation = Quaternion.Euler(tempX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}

	public virtual void Move()
    {
		if ((nextPos.Equals(Vector3.zero)) || Vector3.Distance(transform.position, nextPos) < 0.25f)
        {
            //TODO write random move function + obstacle avoidance
            int r = UnityEngine.Random.Range(4, 10);
            double theta = UnityEngine.Random.Range(0, 360) * Math.PI / 180.0;
            float a = (float)Math.Cos(theta) * r;
            float b = (float)Math.Sin(theta) * r;
			SetDestination(new Vector3(transform.position.x + a, transform.position.y, transform.position.z + b));
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
        }

        EnforceBounds();

    }

    public virtual GenericWeapon Attack(float distance)
    {
        //does nothing
        return null;
    }

    public virtual void LiveOrDie()
    { //does nothing    
	}

    public Vector3 ClampVector3(Vector3 value, Vector3 mins, Vector3 maxs)
    {
        value.x = Mathf.Clamp(value.x, mins.x, maxs.x);
        value.y = Mathf.Clamp(value.y, mins.y, maxs.y);
        value.z = Mathf.Clamp(value.z, mins.z, maxs.z);

        return value;
    }

    public virtual void SetDistanceToClosestSound(float distance)
    {
        //does nothing
    }

    private void EnforceBounds()
    {
        Vector3 newPosition = transform.position;

        float xMax = worldCollider.bounds.max.x;
		float xMin = worldCollider.bounds.min.x;
		float zMax = worldCollider.bounds.max.z;
		float zMin = worldCollider.bounds.min.z;

        if (newPosition.x < xMin )
        {
            newPosition.x = xMax - (xMin - newPosition.x);
            nextPos.x = xMax - (xMin - nextPos.x);
        }
        if( newPosition.x > xMax)
        {
            newPosition.x = xMin + (newPosition.x - xMax);
            nextPos.x = xMin + (nextPos.x - xMax);
        }
        // TODO vertical bounds
        if (newPosition.z < zMin)
        {
            newPosition.z = zMax - (zMin - newPosition.z);
            nextPos.z = zMax - (zMin - nextPos.z);
        }
        if (newPosition.z > zMax)
        {
            newPosition.z = zMin + (newPosition.z - zMax);
            nextPos.z = zMin + (nextPos.z - zMax);
        }
        transform.position = newPosition;
    }
}

