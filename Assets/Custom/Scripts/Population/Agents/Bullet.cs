using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float _maxLifeTime = 5.0f; 
	private float _startTime; 

	// Use this for initialization
	void Start () {
		_startTime = UnityEngine.Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0) return;
		if (UnityEngine.Time.time - _startTime > _maxLifeTime) {
			Destroy(this.gameObject);
		}
	}
}
