using UnityEngine;
using System.Collections;

public class SpawnCubes : MonoBehaviour {

	public int cubesToSpawn;
	public int scoreValue;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
	}
	
	void OnTriggerEnter(Collider other){
		gameManager.addScore (scoreValue);
		gameManager.collectSound.Play ();

		gameManager.SpawnFallingCube (cubesToSpawn);

		gameManager.ResetCamera ();

		Destroy (gameObject);
		gameManager.SpawnPickUp ();
	}
}