using UnityEngine;
using System.Collections;

public class AddLengthPickUp : MonoBehaviour {

	public int toAdd;
	public int scoreValue;

	private GameManager gameManager;
	private SnakeController snakeController;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
		snakeController = GameObject.FindGameObjectWithTag ("Snake").GetComponent<SnakeController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head" && gameManager != null) {
			gameManager.addScore (scoreValue);
			gameManager.collectSound.Play ();

			gameManager.ResetCamera (); // if we were in fps mode, we exit

			snakeController.addBodyPart (toAdd); // add the specified number of body parts

			Destroy (gameObject); // destroy this pick up now
			gameManager.SpawnPickUp (); // spawn another one
		}
	}
}
