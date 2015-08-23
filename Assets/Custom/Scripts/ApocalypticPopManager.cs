using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ApocalypticPopManager : MonoBehaviour {

    // The humans and zombies prefab
    public GameObject ZombiePrefab;
    public GameObject HumanPrefab;
    // The number of spawned
	public int ZombieSpawn = 50;
	public int HumanSpawn = 50;
    // The seed used
    public int RandomSeed = 0;
    // The boundaries
    public Vector3 worldBoundsMin = new Vector3(-25, -1, -25);
    public Vector3 worldBoundsMax = new Vector3(25, -1, 25);
    
	void Start () 
    {
        if (RandomSeed == 0)
            RandomSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        // Setting the seed
        UnityEngine.Random.seed = RandomSeed;


        // Spawning humans
		for (int c = 0; c < HumanSpawn; c++)
        {
            // Spawning a citizen
            var citizen = Instantiate(HumanPrefab, new Vector3(UnityEngine.Random.Range(worldBoundsMin.x, worldBoundsMax.x), 0.0f, UnityEngine.Random.Range(worldBoundsMin.z, worldBoundsMax.z)), Quaternion.Euler(0, 0, 0)) as GameObject;
			// Set as a parent270
            citizen.transform.SetParent(transform);
        }
        
		
		// Spawning humans
		for (int c = 0; c < ZombieSpawn; c++)
        {
            var citizen = Instantiate(ZombiePrefab, new Vector3(UnityEngine.Random.Range(worldBoundsMin.x, worldBoundsMax.x), 0.0f, UnityEngine.Random.Range(worldBoundsMin.z, worldBoundsMax.z)), Quaternion.Euler(0, 0, 0)) as GameObject;

            // Set as a parent
            citizen.transform.SetParent(transform);
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
