using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {
	
	public GameObject AttrackActionPrefab;
	public GameObject RepulseActionPrefab;
	public GameObject GameAera;
	public float xySpeed = 8.0f;
	public float zoomSpeed = 16.0f;
	public float chargeAttrack = 0.0f;
	public float chargeRepulse = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Transform cameraPosition = this.gameObject.GetComponent<Transform> ();

		if (Input.GetAxis ("Vertical") > 0) 
		{
			cameraPosition.transform.Translate(new Vector3(0, 0, Time.deltaTime * xySpeed), Space.World);
		}
		else if (Input.GetAxis ("Vertical") < 0) 
		{
			cameraPosition.transform.Translate(new Vector3(0, 0, -Time.deltaTime * xySpeed), Space.World);
		}		
		if (Input.GetAxis ("Horizontal") > 0) 
		{
			cameraPosition.transform.Translate(new Vector3(Time.deltaTime * xySpeed, 0, 0), Space.World);
		}
		else if (Input.GetAxis ("Horizontal") < 0) 
		{
			cameraPosition.transform.Translate(new Vector3(-Time.deltaTime * xySpeed, 0, 0), Space.World);
		}		
		if ((Input.GetAxis ("Mouse ScrollWheel") > 0) || (Input.GetKeyDown(KeyCode.Minus)))
		{
			cameraPosition.transform.Translate(new Vector3(0, -Time.deltaTime * zoomSpeed, 0), Space.World);
		}
		else if ((Input.GetAxis ("Mouse ScrollWheel") < 0) || (Input.GetKeyDown(KeyCode.Plus)))
		{
			cameraPosition.transform.Translate(new Vector3(0, +Time.deltaTime * zoomSpeed, 0), Space.World);
		}
		KeepCameraInBound();

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}

		if (Input.GetMouseButtonDown(0)) {
            chargeAttrack = Time.time;

		}
		if (Input.GetMouseButtonUp (0)) {
            chargeAttrack = Time.time - chargeAttrack;
			RaycastHit hit = new RaycastHit();
			bool hoverUi = (EventSystem.current != null) && EventSystem.current.IsPointerOverGameObject();
			
			if (!hoverUi
			    && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
				//if (hit.collider.GetComponent<ActionReceiver>() != null)
				{
					var action = Instantiate(AttrackActionPrefab, new Vector3(hit.point.x, -1, hit.point.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                    action.SendMessage("SetTime", chargeAttrack);
				}
			}
			chargeAttrack = 0.0f;
		}

		if (Input.GetMouseButtonDown (1)) {
			chargeRepulse = Time.time;
		}
        if (Input.GetMouseButtonUp(1))
        {
            chargeRepulse = Time.time - chargeRepulse;
            RaycastHit hit = new RaycastHit();
            bool hoverUi = (EventSystem.current != null) && EventSystem.current.IsPointerOverGameObject();

            if (!hoverUi
                && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //if (hit.collider.GetComponent<ActionReceiver>() != null)
                
				var action = Instantiate(RepulseActionPrefab, new Vector3(hit.point.x, -1, hit.point.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                    action.SendMessage("SetTime", chargeRepulse);
            }
			
        }
	}

	private void KeepCameraInBound() 
	{
		if (gameObject.transform.position.y < 5) 
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 5);
		} 
		else if (gameObject.transform.position.y > 50) 
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 50);
		}
	}
}
