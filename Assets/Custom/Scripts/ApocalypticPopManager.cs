using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ApocalypticPopManager : MonoBehaviour {

    // The humans and zombies prefab
    public GameObject ZombiePrefab;
    public GameObject HumanPrefab;
    //game over show
    public GameObject menu; // Assign in inspector
    private bool isShowing;
    // The number of spawned
	public int ZombieSpawn = 50;
	//public int HumanSpawn = 50;
    // The seed used
    public int RandomSeed = 0;
    private float xMax;
    private float xMin;
    private float zMax;
    private float zMin;
    //Spawn parameters
    private float timer = 0.0f;
    public Vector3 centerpos1 = Vector3.zero;
    public Vector3 centerpos2 = Vector3.zero;
    public Vector3 centerpos3 = Vector3.zero;
    public Vector3 centerpos4 = Vector3.zero;
    public int HumanSpawn = 0;
    public static int HumanWavesNumber;
    public static int wavesNumber = 12;
        
	void Start () 
    {
        // The boundaries
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
		Collider worldCollider = GameObject.Find("GameArea").GetComponent<Collider>();
		xMax = worldCollider.bounds.max.x;
		xMin = worldCollider.bounds.min.x;
		zMax = worldCollider.bounds.max.z;
		zMin = worldCollider.bounds.min.z;
        HumanWavesNumber = wavesNumber;

        //Setting RandomSeed
        if (RandomSeed == 0)
            RandomSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        // Setting the seed
        UnityEngine.Random.seed = RandomSeed;
      
		
		// Spawning zombies
		for (int c = 0; c < ZombieSpawn; c++)
        {
			var citizen = Instantiate(ZombiePrefab, 
			                          new Vector3(UnityEngine.Random.Range(xMin+worldCollider.bounds.size.x/4, xMax-worldCollider.bounds.size.x/4), 
			                                      -1.0f, 
			                          UnityEngine.Random.Range(zMin+worldCollider.bounds.size.z/4, zMax-worldCollider.bounds.size.z/4)), 
			                          Quaternion.Euler(0, 0, 0)) as GameObject;

            // Set as a parent
            citizen.transform.SetParent(transform);
        }

        //Setting spawning positions
        centerpos1 = new Vector3(xMin+ xMax/4, -1.0f, zMax/2);
        centerpos2 = new Vector3(xMax - xMax/4, -1.0f, zMax/2);
        centerpos3 = new Vector3(xMax/2, -1.0f, zMax - zMax/4);
        centerpos4 = new Vector3(xMax/2, -1.0f, zMin + zMax / 4);
	}
     
    public void Update () {
        if (Time.timeScale == 0) return;
         if ( (StatusUpdater.humanCount <= 1) && (HumanWavesNumber > 0) )
         {
             Debug.Log("No Humans yet");
             if (HumanWavesNumber < wavesNumber/3)
             {
                 Spawn(3);
             }
             else if (HumanWavesNumber < 2*wavesNumber / 3)
             {
                 Spawn(2);
             }
             else
             {
                 Spawn(1);
             }
             HumanWavesNumber--;
         }
         if (HumanWavesNumber <= 0)
         {
              isShowing = !isShowing;
              menu.SetActive(isShowing);
              Time.timeScale = 0.0F;
         }
    }
     
    public void Spawn(int number){
        Vector3 spawnPosition = Vector3.zero;
        int humansNumber = 0;
        //choose the number of humans
        switch(number){
            case 1:
                humansNumber = UnityEngine.Random.Range(3,10);
                break;
            case 2 :
                humansNumber = UnityEngine.Random.Range(10,20);
                break;
            case 3:
                humansNumber = UnityEngine.Random.Range(20,40);
                break;
        }

        //choose a random spawn position
        int randomPick = 1;
        randomPick = UnityEngine.Random.Range(1, 5);
        switch(randomPick){
            case 1:
                spawnPosition = centerpos1;
                break;
            case 2 :
                spawnPosition = centerpos2;
                break;
            case 3:
                spawnPosition = centerpos3;
                break;
            case 4:
                spawnPosition = centerpos4;
                break;
        }
        for (int c = 0; c < humansNumber; c++)
        {
            randomPick = UnityEngine.Random.Range(1, 5);
            switch (randomPick)
            {
                case 1:
                    spawnPosition = centerpos1;
                    break;
                case 2:
                    spawnPosition = centerpos2;
                    break;
                case 3:
                    spawnPosition = centerpos3;
                    break;
                case 4:
                    spawnPosition = centerpos4;
                    break;
            }
            // Spawning a citizen
            var citizen = Instantiate(HumanPrefab, new Vector3(UnityEngine.Random.Range(spawnPosition.x-xMax/4, spawnPosition.x+xMax/4), -1.0f, UnityEngine.Random.Range(spawnPosition.z-zMax/4, spawnPosition.z-zMax/4)), Quaternion.Euler(0, 0, 0)) as GameObject;
			// Set as a parent270
            citizen.transform.SetParent(transform);
        }

    }
}
