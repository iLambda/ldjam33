using UnityEngine;

public class MainMenuScript : MonoBehaviour {
	
	public void StartGame()
	{
		Application.LoadLevel("Main");
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
	
	void Update () {		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
