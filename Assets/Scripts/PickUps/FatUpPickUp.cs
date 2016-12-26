using UnityEngine;
using System.Collections;

public class FatUpPickUp : MonoBehaviour {

	public float fatUpValue = 1f;

	private Camera main;
	private Camera fps;
	private GameManager gameManager;
	private SnakeController snakeController;

	private GameObject snake;
	private SnakeHeadMovement snakeHead;

	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag ("Snake");
		snakeHead = GameObject.FindGameObjectWithTag ("Head").GetComponent<SnakeHeadMovement> ();;

		GameObject fpsCamera = GameObject.FindGameObjectWithTag ("FPSCamera");
		GameObject mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		main = mainCamera.GetComponent<Camera> ();
		fps = fpsCamera.GetComponent<Camera> ();

		gameManager = mainCamera.GetComponent<GameManager> ();
		snakeController = snake.GetComponent<SnakeController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head") {
			main.enabled = true;
			fps.enabled = false;

			snakeHead.isFPS = false;

			//snake.transform.localScale += new Vector3(fatUpValue, fatUpValue, fatUpValue);
			snakeController.ScaleUp(fatUpValue);
			//snakeController.SpeedUp (fatUpValue);

			Destroy (gameObject);
			gameManager.SpawnPickUp ();
		}
	}
}
