using System;
using UnityEngine;


class Attractor : MonoBehaviour
{
    //attributes
    public float hearRange = 30.0f; // TODO adjust this value
    int zombiesLayer = 1 << LayerMask.NameToLayer("zombieHearing");
    public float timer = 5.0f;
    public Collider[] zombiesWhoHeard;

    public void Start()
    {
        //play sound here
        zombiesWhoHeard = Physics.OverlapSphere(transform.position, hearRange, zombiesLayer);
        //Debug.Log("There are " + zombiesWhoHeard.Length + " hearing me");
    }

    public void Update()
    {  
        foreach (Collider enemy in zombiesWhoHeard)
        {
            // use either Send Message or GetComponent and tell this enemy he heard something
            if (enemy != null)
            {
                GenericAgent zombie = enemy.gameObject.GetComponent<GenericAgent>();
                var heading = transform.position;
                float localDistance = Vector3.Distance(transform.position, zombie.transform.position);
                if (zombie.distanceToClosestSound > localDistance)
                {
                    zombie.SetDistanceToClosestSound(localDistance);
                    zombie.SetDestination(new Vector3(heading.x, -1, heading.z));
                }
            }
        }
        if (timer >= 0)
        {
            timer -= UnityEngine.Time.deltaTime; // TODO decrement temporally
        }
        else
        {
            stopSound();
        }

    }

    public void stopSound()
    {
        foreach (Collider enemy in zombiesWhoHeard)
        {
            if (enemy != null)
            {
                int r = UnityEngine.Random.Range(4, 10);
                double theta = UnityEngine.Random.Range(0, 360) * Math.PI / 180.0;
                float a = (float)Math.Cos(theta) * r;
                float b = (float)Math.Sin(theta) * r;
                GenericAgent zombie = enemy.gameObject.GetComponent<GenericAgent>();
                zombie.SetDestination(new Vector3(transform.position.x + a, transform.position.y, transform.position.z + b));
                zombie.SetDistanceToClosestSound(Mathf.Infinity);
            }
        }
        Destroy(gameObject);

    }

    public void SetTime(float chargetime)
    {
        timer = 4*chargetime;
        if (timer < 3)
        {
            timer = 3.0f;
        }
        Debug.Log("chargetime " + timer);
    }
}