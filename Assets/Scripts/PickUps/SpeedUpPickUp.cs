using UnityEngine;
using System.Collections;

public class SpeedUpPickUp : MonoBehaviour {

	public float speedUpBy = 1f;
	public int scoreValue;

	private GameManager gameManager;
	private SnakeController snakeController;
	private SnakeHeadMovement snakeHead;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
		snakeController = GameObject.FindGameObjectWithTag ("Snake").GetComponent<SnakeController> ();
		snakeHead = GameObject.FindGameObjectWithTag ("Head").GetComponent<SnakeHeadMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head") {
			gameManager.addScore (scoreValue);
			gameManager.collectSound.Play ();

			gameManager.ResetCamera (); // if we were in fps mode, we exit
			snakeHead.isFPS = false;

			snakeController.SpeedUp (speedUpBy);

			Destroy (gameObject); // destroy this pick up now
			gameManager.SpawnPickUp (); // spawn another one
		}
	}
}
