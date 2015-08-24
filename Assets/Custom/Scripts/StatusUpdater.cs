using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusUpdater : MonoBehaviour {

	public static int zombiesCount = 0;
	public static int humanCount = 0;
	public static int timeCount = 0;
	public static int contaminatedCount = 0;

	private UnityEngine.UI.Text zombiesCountText;
	private UnityEngine.UI.Text humansCountText;
	private UnityEngine.UI.Text timeCountText;
	private UnityEngine.UI.Text contaminatedCountText;
    private UnityEngine.UI.Text totalZombiesCountText;

	// Use this for initialization
	void Start () {

		zombiesCountText = (UnityEngine.UI.Text) GameObject.Find ("ZombiesCountText").GetComponent<Text>(); 
		humansCountText = (UnityEngine.UI.Text) GameObject.Find ("HumansCountText").GetComponent<Text>(); 
		timeCountText = (UnityEngine.UI.Text) GameObject.Find ("TimeCountText").GetComponent<Text>(); 
		contaminatedCountText = (UnityEngine.UI.Text) GameObject.Find ("ContaminedCountText").GetComponent<Text>();
        totalZombiesCountText = (UnityEngine.UI.Text)GameObject.Find("score").GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
		zombiesCountText.text = StatusUpdater.zombiesCount.ToString ();
		humansCountText.text = StatusUpdater.humanCount.ToString ();
		timeCountText.text = StatusUpdater.timeCount.ToString ();
		contaminatedCountText.text = StatusUpdater.contaminatedCount.ToString ();
        totalZombiesCountText.text = StatusUpdater.contaminatedCount.ToString();
	}


}
