using System;
using UnityEngine;


class Attractor : MonoBehaviour
{
    //attributes
    public float hearRange = 10.0f; // TODO adjust this value
    int zombiesLayer = 1 << LayerMask.NameToLayer("zombieHearing");

    public void Update()
    {
        //play sound here
        Collider[] zombiesWhoHeard = Physics.OverlapSphere(transform.position, hearRange, zombiesLayer);
        Debug.Log("There are " + zombiesWhoHeard.Length + " hearing me");

        foreach (Collider enemy in zombiesWhoHeard)
        {
            // use either Send Message or GetComponent and tell this enemy he heard something
            GenericAgent zombie = enemy.gameObject.GetComponent<GenericAgent>();
            var heading = transform.position;
            zombie.nextPos = heading;
        }

    }
}