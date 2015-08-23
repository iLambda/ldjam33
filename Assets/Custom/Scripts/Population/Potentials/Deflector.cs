using System;
using UnityEngine;


class Deflector : MonoBehaviour
{
    //attributes
    public float hearRange = 10.0f; // TODO adjust this value
    int zombiesLayer = 1 << LayerMask.NameToLayer("zombieHearing");
    public float timer;
    public Collider[] zombiesWhoHeard;

    public void Start()
    {
        timer = 5.0f;
    }

    public void Update()
    {
        //play sound here
        Collider[] zombiesWhoHeard = Physics.OverlapSphere(transform.position, hearRange, zombiesLayer);
       // Debug.Log("There are " + zombiesWhoHeard.Length + " hearing me");

        foreach (Collider enemy in zombiesWhoHeard)
        {
            // use either Send Message or GetComponent and tell this enemy he heard something
            GenericAgent zombie = enemy.gameObject.GetComponent<GenericAgent>();
            var heading = zombie.transform.position - transform.position;
			zombie.SetDestination(new Vector3(heading.x, -1, heading.z));
        }
        if (timer >= 0)
        {
            timer -= UnityEngine.Time.deltaTime; // TODO decrement temporally
        }
        else
        {
            Debug.Log("timer = " + timer);
            stopSound();
        }

    }

    public void stopSound()
    {
        foreach (Collider enemy in zombiesWhoHeard)
        {
            // use either Send Message or GetComponent and tell this enemy he heard something
            int r = UnityEngine.Random.Range(4, 10);
            double theta = UnityEngine.Random.Range(0, 360) * Math.PI / 180.0;
            float a = (float)Math.Cos(theta) * r;
            float b = (float)Math.Sin(theta) * r;
            GenericAgent zombie = enemy.gameObject.GetComponent<GenericAgent>();
			zombie.SetDestination(new Vector3(transform.position.x + a, transform.position.y, transform.position.z + b));
        }
        Destroy(gameObject);

    }
}