using UnityEngine;
using System.Collections;
using System.Linq;

public enum MoveBehaviorType
{
    Idle,
    Attraction,
    Repulsion
}

//TODO : handle more than one attractor/repulsor
// must apply the sum of forces

public class MoveBehavior : MonoBehaviour {

    public MoveBehaviorType Type;
    public float Speed = 1f;

    //private TriggerField currentField;

	// Use this for initialization
	void Start () {
        //StartCoroutine("GetField");
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (Type != MoveBehaviorType.Idle)
        {
            var direction = currentField.transform.position - transform.position;
            direction.Normalize();
            switch (currentField.Type)
            {
                case MoveBehaviorType.Attraction:
                    transform.position += new Vector3(direction.x, 0, direction.z) * (Speed * Time.deltaTime);
                    break;
                case MoveBehaviorType.Repulsion:
                    transform.position -= new Vector3(direction.x, 0, direction.z) * (Speed * Time.deltaTime);
                    break;
            }
            transform.LookAt(new Vector3(currentField.transform.position.x, 1, currentField.transform.position.z));
        }*/
	}

    /*public void SetMoveRoutine(TriggerField field)
    {
        currentField = field;
        Type = field.Type;
    }
    */
    /*IEnumerator GetField()
    {
        for(; ;)
        {
            //if (Type == MoveBehaviorType.Idle)
            //{
                var triggers = GameObject.FindGameObjectsWithTag("Trigger");
                var triggerDist = triggers.Min(t => Vector3.Distance(t.transform.position, transform.position));
                var trigger = triggers.First(t => Vector3.Distance(t.transform.position, transform.position) <= triggerDist).GetComponent<TriggerField>();
                SetMoveRoutine(trigger);
            //}
            yield return new WaitForSeconds(1);
        }
    }*/
}
