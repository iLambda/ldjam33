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
            var citizen = Instantiate(CitizenPrefab, new Vector3(Random.Range(-45f, 45f), 1, Random.Range(-45f, 45f)), Quaternion.Euler(0, 0, 0)) as GameObject;
            var props = citizen.GetComponent<MoveBehavior>();
            props.Speed = Random.Range(1f, 5f);
            citizen.transform.SetParent(transform);
        }
	}
	
	void Update () {
	    
	}
}
