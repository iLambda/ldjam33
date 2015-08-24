using System;
using UnityEngine;


class Deflector : MonoBehaviour
{
    //attributes
    public float hearRange = 7.0f; // TODO adjust this value
    int zombiesLayer = 1 << LayerMask.NameToLayer("zombieHearing");
    public float timer=5.0f;
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
            if(enemy != null)
            {
                // use either Send Message or GetComponent and tell this enemy he heard something
                GenericAgent zombie = enemy.gameObject.GetComponent<GenericAgent>();
                var heading = zombie.transform.position - transform.position;
                float localDistance = Vector3.Distance(transform.position, zombie.transform.position);
                if (zombie.distanceToClosestSound > localDistance)
                {
                    zombie.SetDistanceToClosestSound(localDistance);
                    zombie.SetDestination(zombie.transform.position + 2*heading);
                }
            }
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