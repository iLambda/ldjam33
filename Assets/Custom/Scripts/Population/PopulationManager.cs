using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour {
    public GameObject CitizenPrefab;

    public int PopulationCount = 200;
    public int RandomSeed = Random.Range(int.MinValue, int.MaxValue);

    public List<Transform> Children;


	void Start () {
        Random.seed = RandomSeed;
        for (int c = 0; c < PopulationCount; c++)
        {
            var citizen = Instantiate(CitizenPrefab, new Vector3(Random.Range(-45f, 45f), 0.5f, Random.Range(-45f, 45f)), Quaternion.Euler(0, 0, 0)) as GameObject;
            citizen.transform.SetParent(transform);
        }
	}
	
	void Update () {
	    
	}
}
