using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour {

	public float speed = 5f;
	public float jumpValue = 6f;
	[HideInInspector] public float nBodyParts;

	public Text collisionText;

	private SnakeHeadMovement snakeHead;
	//private SnakeBodyMovement[] snakeBody;
	private List<SnakeBodyMovement> snakeBody;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision (8, 8, true);

		GameObject snakeHeadObj = GameObject.FindGameObjectWithTag ("Head"); // get the head first
		GameObject[] snakeBodyObj = GameObject.FindGameObjectsWithTag ("Body"); // get all the body parts

		snakeHead = snakeHeadObj.GetComponent<SnakeHeadMovement>(); // set head at beginning
		snakeBody = new List<SnakeBodyMovement>();

		//snakeBody = new SnakeBodyMovement[snakeBodyObj.Length];
		if (snakeBodyObj.Length > 0) { // if we have body parts rn...
			for (int i = 0; i < snakeBodyObj.Length; i++) { // set them up
				//Debug.Log(snakeBodyObj[i].name);
				snakeBody.Add(snakeBodyObj [i].GetComponent<SnakeBodyMovement>());
				snakeBody [i].bodyNum = i;
			}
		}
		//nBodyParts = snakeBody.Length;
		nBodyParts = snakeBody.Count;
		//Debug.Log (snakeBody.Count);

		if (collisionText != null) {
			collisionText.text = string.Empty;
		}

		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (snakeBody.Capacity);
		AlertBodyParts ();
	}

	// for physics
	void FixedUpdate(){
		
	}

	void AlertBodyParts(){
		snakeBody[0].setDestination(snakeHead.getPosition ());
//		snakeBody[0].setMovement(snakeHead.getHorizontal(), snakeHead.getVertical());
		for (int i = 1; i < snakeBody.Count; i++) {
			snakeBody [i].setDestination (snakeBody [i - 1].getPosition ());
//			snakeBody[i].setMovement(snakeBody[i-1].getHorizontal(), snakeBody[i-1].getVertical());
		}
	}

	public void checkCollision(Collider other){
//		Debug.Log (other.gameObject.name);
//		Debug.Log (snakeHead.name);
		if (other.gameObject.name == snakeHead.name) {
			//Debug.Log ("You lose! " + other.gameObject.name);
//			if (collisionText != null) {
//				collisionText.text = "Collision Detected!";
//			}
			gameManager.RestartLevel();

			// DO MORE THINGS HERE NOW THAT WE CAN DETECT COLLISIONS
		}
	}

	public void sendJump(Collider other){
//		Debug.Log ("got to index: " + indexToSendTo);
//		Debug.Log ("position sent is: " + pos);
//		if (indexToSendTo < snakeBody.Length) {
//			snakeBody [indexToSendTo].jumpLocation = pos;
//		}
	}

	public void ResetVelocities(){
		snakeHead.r.velocity = Vector3.zero;
		for (int i = 0; i < snakeBody.Count; i++) {
			snakeBody[i].r.velocity = Vector3.zero;
		}
	}
}
