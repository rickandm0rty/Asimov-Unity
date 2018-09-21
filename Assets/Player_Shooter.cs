using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooter : MonoBehaviour {
	private Camera playerCam;
	public float damage = 10f;
	public float range = 1000f;
	

	// Use this for initialization
	void Start () {
		playerCam = GetComponent<Camera>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 point = new Vector3 (playerCam.pixelWidth / 2, playerCam.pixelHeight/2, 0);
			Ray ray = playerCam.ScreenPointToRay(point);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit))
			{
				GameObject hitObject = hit.transform.gameObject;

				Enemy_Shot target = hitObject.GetComponent<Enemy_Shot>();
				if(target != null)
				{
					target.takeDamage(damage);
					target.GotShot();
				} 
				else
				{
					StartCoroutine(ShotGen(hit.point)); 		
				}

				StartCoroutine(ShotGen(hit.point));   //Launch coRoutine in response to hit
			}
		}
	}

	private IEnumerator ShotGen(Vector3 pos)
	{
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		Rigidbody gameObjectsRigidBody = sphere.AddComponent<Rigidbody>(); // Add the rigidbody.
		sphere.transform.localScale = new Vector3 (0.1f,0.1f,0.1f);
		//sphere.transform.position = pos;
		sphere.GetComponent<Rigidbody>().velocity = sphere.transform.forward * 12;

		yield return new WaitForSeconds(1);

		Destroy(sphere);
	}

	//RETICLE:
	//OnGUI forces Unity to render Reticle on top of screen at all times
	void OnGUI()
	{
		int size = 12;
		float posX = playerCam.pixelWidth / 2 - size / 4;
		float posY = playerCam.pixelHeight/2 - size /2;
		GUI.Label (new Rect (posX, posY, size, size), "*");
		
	}
}
