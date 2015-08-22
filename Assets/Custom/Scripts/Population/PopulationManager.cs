﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PopulationManager : MonoBehaviour {

    // The simple citizen prefab
    public GameObject CitizenPrefab;
    // The number of citizens spawned
    public int SpawnCount = 200;
    // The seed used
    public int RandomSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
    // The boundaries
    public Rect Boundaries;
    
	void Start () 
    {
        // Setting the seed
        UnityEngine.Random.seed = RandomSeed;

        // Spawning citizens
        for (int c = 0; c < SpawnCount; c++)
        {
            // Spawning a citizen
            var citizen = Instantiate(CitizenPrefab, new Vector3(UnityEngine.Random.Range(Boundaries.min.x, Boundaries.max.x), 1, UnityEngine.Random.Range(Boundaries.min.y, Boundaries.max.y)), Quaternion.Euler(0, 0, 0)) as GameObject;
            
            // Set as a parent
            citizen.transform.SetParent(transform);

            // Setting behavior
            var props = citizen.GetComponent<MoveBehavior>();
            props.Speed = UnityEngine.Random.Range(1f, 5f);
        }
	}
	
	void Update () 
    {
	    
	}

    public float GetPotential(float x, float y)
    {
        // Get potential from children
        var childrenPotential = gameObject.GetComponentsInChildren<IPotential>();

        // Return value 
        return childrenPotential != null ? -childrenPotential.Select(Pot => Pot.GetPotential(x, y)).Sum() : 0f;
    }
}
