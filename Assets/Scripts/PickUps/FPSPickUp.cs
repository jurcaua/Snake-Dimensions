using UnityEngine;
using System.Collections;

public class FPSPickUp : MonoBehaviour {

	public Camera main;
	public Camera fps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.name);
		if (other.name == "SnakeHead") {
			main.enabled = false;
			fps.enabled = true;
			other.gameObject.GetComponent<SnakeHeadMovement> ().isFPS = true;
		}
	}
}
