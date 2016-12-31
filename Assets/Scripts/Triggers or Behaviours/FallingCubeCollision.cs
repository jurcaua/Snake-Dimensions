using UnityEngine;
using System.Collections;

public class FallingCubeCollision : MonoBehaviour {

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Head") {
			gameManager.RestartLevel ();
		}
	}
}
