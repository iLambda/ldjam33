using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {
	
	public GameObject ActionPrefab;
    public GameObject Action2Prefab;
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
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			cameraPosition.transform.Translate(new Vector3(0, -Time.deltaTime * zoomSpeed, 0), Space.World);
		}
		else if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			cameraPosition.transform.Translate(new Vector3(0, +Time.deltaTime * zoomSpeed, 0), Space.World);
		}

		if (Input.GetMouseButtonDown(0)) {
			chargeAttrack += Time.deltaTime;

		}
		if (Input.GetMouseButtonUp (0)) {
			RaycastHit hit = new RaycastHit();
			bool hoverUi = (EventSystem.current != null) && EventSystem.current.IsPointerOverGameObject();
			
			if (!hoverUi
			    && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
				//if (hit.collider.GetComponent<ActionReceiver>() != null)
				{
					var action = Instantiate(ActionPrefab, new Vector3(hit.point.x, -1, hit.point.z), Quaternion.Euler(0, 0, 0)) as GameObject;
				}
			}
			chargeAttrack = 0.0f;
		}

		if (Input.GetMouseButtonUp (1)) {
			chargeRepulse += Time.deltaTime;
		}
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit = new RaycastHit();
            bool hoverUi = (EventSystem.current != null) && EventSystem.current.IsPointerOverGameObject();

            if (!hoverUi
                && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //if (hit.collider.GetComponent<ActionReceiver>() != null)
                
                    var action = Instantiate(Action2Prefab, new Vector3(hit.point.x, -1, hit.point.z), Quaternion.Euler(0, 0, 0)) as GameObject;
                
            }
			chargeRepulse = 0.0f;
        }
	}
}
