using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Camera main;
	public Camera fps;
	public GameObject gameArea;
	public float spawnPadding;
	public GameObject[] pickUps;

	private Renderer rend;
	private SnakeController snakeController;
	private int lastPickUp = 0;

	// Use this for initialization
	void Start () {
		ResetCamera ();

		rend = gameArea.GetComponent<Renderer> ();

		snakeController = GameObject.FindGameObjectWithTag ("Snake").GetComponent<SnakeController> ();

		SpawnPickUp ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnPickUp() {
		float randX = 0; // initializing
		float randZ = 0;
		int randPickUp = -1; // cant be neagtiev one cause thats lastPickUps' initialized value

		bool found = false; 
		while (!found) { // get random positions on the game area
			randX = Random.Range ((gameArea.transform.position.x - rend.bounds.size.x / 2) + spawnPadding, (gameArea.transform.position.x + rend.bounds.size.x / 2) - spawnPadding);
			randZ = Random.Range ((gameArea.transform.position.z - rend.bounds.size.z / 2) + spawnPadding, (gameArea.transform.position.z + rend.bounds.size.z / 2) - spawnPadding);

			if (!Physics.Raycast (new Vector3 (randX, 1f, randZ), Vector3.down, 1f, 9)) { // check if theres anything where we are spawning the pickup
				found = true;
			}
		}

		randPickUp = Random.Range (0, pickUps.Length); // random pickup from avaliable pickups
		while (pickUps[randPickUp].name == pickUps[lastPickUp].name && pickUps[lastPickUp].name == "FPSPickUp") {
			randPickUp = Random.Range (0, pickUps.Length);
		}

		lastPickUp = randPickUp;
		//Debug.Log(pickUps[lastPickUp].name);
		Instantiate (pickUps [randPickUp], new Vector3 (randX, pickUps [randPickUp].transform.position.y, randZ), pickUps [randPickUp].transform.rotation);
	}

	public void RestartLevel(){
		snakeController.ResetVelocities ();

		GameObject[] jumpTriggers = GameObject.FindGameObjectsWithTag("JumpTrigger");
		for (int i = 0; i < jumpTriggers.Length; i++) {
			Destroy (jumpTriggers [i]);
		}

		SceneManager.LoadScene ("Main");
	}

	public void ResetCamera(){
		main.enabled = true;
		fps.enabled = false;
	}
}
