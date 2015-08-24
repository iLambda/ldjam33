using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUpdater : MonoBehaviour {

    private UnityEngine.UI.Text totalZombiesCountText;

	// Use this for initialization
	void Start () {
        totalZombiesCountText = (UnityEngine.UI.Text)GameObject.Find("FinalScore").GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
		totalZombiesCountText.text = StatusUpdater.zombiesCount.ToString();
	}


}
