﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusUpdater : MonoBehaviour {

	public static int zombiesCount = 0;
	public static int humanCount = 0;
	public static int contaminatedCount = 0;

	private UnityEngine.UI.Text zombiesCountText;
	private UnityEngine.UI.Text humansCountText;
	private UnityEngine.UI.Text contaminatedCountText;

	// Use this for initialization
	void Start () {		
		zombiesCount = 0;
		humanCount = 0;
		contaminatedCount = 0;
		zombiesCountText = (UnityEngine.UI.Text) GameObject.Find ("ZombiesCountText").GetComponent<Text>(); 
		humansCountText = (UnityEngine.UI.Text) GameObject.Find ("HumansCountText").GetComponent<Text>(); 
		contaminatedCountText = (UnityEngine.UI.Text) GameObject.Find ("ContaminedCountText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		zombiesCountText.text = StatusUpdater.zombiesCount.ToString ();
		humansCountText.text = StatusUpdater.humanCount.ToString ();
		contaminatedCountText.text = StatusUpdater.contaminatedCount.ToString ();
	}


}
