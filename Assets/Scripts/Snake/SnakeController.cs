using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour {

	public float speed = 5f;
	public float jumpValue = 6f;
	public float snakeColliderRadius = 0.4f;
	[HideInInspector] public int nBodyParts;

	public bool isMain;

	public Text collisionText;
	public GameObject snakeBodyPart;

	private GameObject snake;
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

		snake = GameObject.FindGameObjectWithTag ("Snake");
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
		if (snakeBody.Count > 0) {
			snakeBody [0].setDestination (snakeHead.getPosition ());
//			snakeBody [0].setDestination (snakeHead.r);
			for (int i = 1; i < snakeBody.Count; i++) {
				snakeBody [i].setDestination (snakeBody [i - 1].getPosition ());
//				snakeBody [i].setDestination (snakeBody [i - 1].r);
			}
		}
	}

	public void checkCollision(Collider other){
//		Debug.Log (other.gameObject.name);
//		Debug.Log (snakeHead.name);
		if (other.gameObject.name == snakeHead.name && isMain) {
			Debug.Break ();
			//Debug.Log ("You lose! " + other.gameObject.name);
//			if (collisionText != null) {
//				collisionText.text = "Collision Detected!";
//			}
			if (gameManager != null) {
				gameManager.RestartLevel ();
			}

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

	public void addBodyPart(int toAdd){
		if (snakeBody.Count == 0) { // adding when we didnt have any body parts to beginning with
			nBodyParts = 1;
			GameObject nextPart = Instantiate (snakeBodyPart, snakeHead.transform.position - (snakeHead.transform.forward * snakeHead.transform.localScale.x), snakeHead.transform.rotation, snake.transform) as GameObject;
			SnakeBodyMovement nextPartScript = nextPart.GetComponent<SnakeBodyMovement> ();
			nextPartScript.directionFacing = snakeHead.directionFacing;
			nextPartScript.bodyNum = nBodyParts - 1;
			snakeBody.Add (nextPartScript);
			for (int i = 0; i < toAdd - 1; i++) {
				nBodyParts++; // since we're adding a part
				SnakeBodyMovement lastPart = snakeBody [snakeBody.Count - 1];
				nextPart = Instantiate (snakeBodyPart, lastPart.transform.position - (lastPart.transform.forward * lastPart.transform.localScale.x), lastPart.transform.rotation, snake.transform) as GameObject;
				nextPartScript = nextPart.GetComponent<SnakeBodyMovement> ();
				nextPartScript.directionFacing = lastPart.directionFacing;
				nextPartScript.bodyNum = nBodyParts - 1;
				snakeBody.Add (nextPartScript);
			}
			addBodyPart (toAdd - 2);
		} else { // adding when we already have one
			for (int i = 0; i < toAdd; i++) {
				nBodyParts++; // since we're adding a part
				SnakeBodyMovement lastPart = snakeBody [snakeBody.Count - 1];
				GameObject nextPart = Instantiate (snakeBodyPart, lastPart.transform.position, lastPart.transform.rotation, snake.transform) as GameObject;
				SnakeBodyMovement nextPartScript = nextPart.GetComponent<SnakeBodyMovement> ();
				nextPartScript.directionFacing = lastPart.directionFacing;
				nextPartScript.bodyNum = nBodyParts - 1;
				snakeBody.Add (nextPartScript);
			}
		}
	}

	public void SpeedUp(float speedUpBy){
		speed += speedUpBy;
	}

	// NOT USED CAUSE ITS BUGGY BUT THE FUNCTIONALITY IS HERE IF I EVER WANA WORK ON IT
	public void ScaleUp(float scaleUpBy){
		snakeHead.transform.localScale += new Vector3 (scaleUpBy, scaleUpBy, scaleUpBy);
		snakeHead.transform.Translate (new Vector3 (0, snakeColliderRadius / snakeHead.transform.localScale.x, 0));
		//snakeHead.collider.radius = snakeColliderRadius / snakeHead.transform.localScale.x;
		for (int i = 0; i < snakeBody.Count; i++) {
			snakeBody[i].transform.localScale += new Vector3 (scaleUpBy, scaleUpBy, scaleUpBy);
			snakeBody[i].transform.Translate (new Vector3 (0, snakeColliderRadius / snakeBody [i].transform.localScale.x, 0));
			//snakeBody [i].collider.radius = snakeColliderRadius / snakeBody [i].transform.localScale.x;
		}
	}
}
