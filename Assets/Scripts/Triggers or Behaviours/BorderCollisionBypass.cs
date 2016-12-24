using UnityEngine;
using System.Collections;

public class BorderCollisionBypass : MonoBehaviour {

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head") {
			gameManager.RestartLevel ();
		}
	}
}
