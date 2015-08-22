using UnityEngine;
using System.Collections;

public enum MoveBehaviorType
{
    Idle,
    Attraction,
    Repulsion
}

public class MoveBehavior : MonoBehaviour {

    public MoveBehaviorType Type;
    public Vector3 Point;
    public float Speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var direction = Point - transform.position;
        direction.Normalize();
        switch(Type)
        {
            case MoveBehaviorType.Idle:
                break;
            case MoveBehaviorType.Attraction:
                transform.position += new Vector3(direction.x, 0, direction.z) * (Speed * Time.deltaTime);
                break;
            case MoveBehaviorType.Repulsion:
                transform.position -= new Vector3(direction.x, 0, direction.z) * (Speed * Time.deltaTime);
                break;
        }
        transform.LookAt(new Vector3(Point.x, 1, Point.z));
	}
}
