using UnityEngine;
using System.Collections;

public class MainMenuMusicLooper : MonoBehaviour {

	public AudioClip[] stings;
	public AudioSource stingSource;
	public float timeBetweenLoop;
	public float waitLoop;
	private int current;

	// Use this for initialization
	void Start () {
		current = 0;
		waitLoop = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		waitLoop -= Time.deltaTime;
		if (waitLoop < 0)
		{
			if (!stingSource.isPlaying) 
			{
				stingSource.clip = stings[current];
				stingSource.Play();
				current++;
				if (current >= stings.Length) 
				{
					current = 0;
					waitLoop = timeBetweenLoop + stingSource.clip.length;
				}
			}
		}
	}
}
