using UnityEngine;
using System.Collections;

public class FatUpPickUp : MonoBehaviour {

	public float fatUpValue = 1f;

	private Camera main;
	private Camera fps;
	private GameManager gameManager;
	private SnakeController snakeController;

	private GameObject snake;

	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag ("Snake");

		gameManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager> ();
		snakeController = snake.GetComponent<SnakeController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head") {
			gameManager.collectSound.Play ();

			gameManager.ResetCamera ();

			//snake.transform.localScale += new Vector3(fatUpValue, fatUpValue, fatUpValue);
			snakeController.ScaleUp(fatUpValue);
			//snakeController.SpeedUp (fatUpValue);

			Destroy (gameObject);
			gameManager.SpawnPickUp ();
		}
	}
}
