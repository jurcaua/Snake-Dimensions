using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SnakeHeadMovement : MonoBehaviour, JumpingObject {

	public GameObject jumpTrigger;
	public float bodyPartTimeOut = 0.5f;
	public float turningRate;
	public float loadJumpDelay = 0.5f;
	public float dragThreshold;

	public AudioSource jumpSound;

	[HideInInspector] public bool isFPS = false;

	[HideInInspector] public Rigidbody r;
	private float vertical = 0f;
	private float horizontal = -1f;
	//private float speed;
	//private float jumpValue;
	[HideInInspector] public string directionFacing = "left";
	private float curTime = 0;
	private bool Dragging = false;

	private Vector2 startPos = Vector2.zero;
	private Vector2 endPos = Vector2.zero;
	private float swipeVerticalDistance;
	private float swipeHorizontalDistance;
	private float swipeDistance;


	//[HideInInspector] public SphereCollider collider;

	private SnakeController snakeController;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
		snakeController = GetComponentInParent<SnakeController> ();
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager>();

		//collider = GetComponent<SphereCollider> ();

		//speed = snakeController.speed;
		//jumpValue = snakeController.jumpValue;
	}

	void Update(){
		getInput ();
		curTime += Time.deltaTime;
		//Debug.Log (r.velocity);
	}

	void FixedUpdate () {
		Move ();
		Rotate ();
	}

	void getInput(){
		if (snakeController.isMain && (Input.anyKeyDown || Input.touchCount > 0)) {
			float pastVert = vertical;
			float pastHor = horizontal;

			if (Application.platform == RuntimePlatform.WindowsEditor) {
				vertical = Input.GetAxisRaw ("Vertical");
				horizontal = Input.GetAxisRaw ("Horizontal");

				if (isFPS) {
					if (directionFacing == "up") {
						// do nothing cause it works normally
					} else if (directionFacing == "down") {
						horizontal = -horizontal;
						vertical = -vertical;
					} else if (directionFacing == "left") {
						float temp = vertical;
						vertical = horizontal;
						horizontal = -temp;
					} else if (directionFacing == "right") { 
						float temp = vertical;
						vertical = -horizontal;
						horizontal = temp;
					}
				}
			}

			if (Mathf.Abs (vertical) == 1f && Mathf.Abs (horizontal) == 1f) {
				if (Mathf.Abs (pastVert) == 1f) {
					vertical = 0;
				} else if (Mathf.Abs (pastHor) == 1f) {
					horizontal = 0;
				}
			} else if (vertical == 0f && horizontal == 0f) {
				vertical = pastVert;
				horizontal = pastHor;
			}

			if ((vertical == -pastVert || horizontal == -pastHor) && snakeController.nBodyParts != 0) {
				vertical = pastVert;
				horizontal = pastHor;
			}

			if (Input.GetButtonDown ("Jump")) {
				//snakeController.sendJump (transform.position, 0);
				//Destroy(GameObject.FindGameObjectWithTag("JumpTrigger"));
				//			GameObject temp = Instantiate (jumpTrigger, transform.position, transform.rotation) as GameObject;
				//			Destroy(temp, snakeController.nBodyParts * bodyPartTimeOut);
				Jump ();
				//r.AddForce (transform.up * jumpValue, ForceMode.Impulse);
			}
		}



//		if (Input.GetKeyDown ("r")) {
//			//snakeController.collisionText.text = string.Empty;
//			SceneManager.LoadScene ("Main");
//		}
	}

	void Move(){
		float speed = snakeController.speed;
		r.velocity = new Vector3 (horizontal * speed, r.velocity.y, vertical * speed);
	}

	void Rotate(){
		Quaternion targetRotation = Quaternion.identity;
		if (vertical == 1 && horizontal == 0) { // going up
			targetRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			directionFacing = "up";
		} else if (vertical == -1 && horizontal == 0) { // going down
			targetRotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			directionFacing = "down";
		} else if (vertical == 0 && horizontal == 1) { // going right
			targetRotation = Quaternion.Euler (new Vector3 (0, 90, 0));
			directionFacing = "right";
		} else if (vertical == 0 && horizontal == -1) { // going left
			targetRotation = Quaternion.Euler (new Vector3 (0, -90, 0));
			directionFacing = "left";
		}
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, turningRate * Time.deltaTime); // smooth rotation
	}

	public Vector3 getPosition(){
		return transform.position - transform.forward * transform.localScale.x/3;
	}

	public float getHorizontal(){
		return horizontal;
	}

	public float getVertical(){
		return vertical;
	}

	void OnTriggerEnter(Collider other){
		//snakeController.checkCollision (other);
	}

	bool onGround(){
		return Physics.Raycast (transform.position, Vector3.down, transform.lossyScale.x / 2 + 0.01f); // check if its touching the floor
	}

	public void Jump(){
		if (!Dragging && onGround() && curTime > loadJumpDelay && !gameManager.Paused){
			float jumpValue = snakeController.jumpValue;

			jumpSound.Play ();

			GameObject temp = Instantiate (jumpTrigger, transform.position, transform.rotation) as GameObject; // creating the jump trigger
			Destroy(temp, snakeController.nBodyParts * bodyPartTimeOut); // destory it after some time according to length of snake
			r.velocity += new Vector3 (0, jumpValue, 0); // JUMPPP
		}
	}

	public void startDrag(){
		Dragging = true;
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];

			startPos = touch.position;
		}
	}

	public void endDrag(){
		if (Input.touchCount > 0) {
			float pastHor = horizontal;
			float pastVert = vertical;

			Touch touch = Input.touches [0];

			endPos = touch.position;

			// see how far they swiped
			swipeVerticalDistance = (endPos.y - startPos.y);
			swipeHorizontalDistance = (endPos.x - startPos.x);
			swipeDistance = (endPos - startPos).magnitude;

			if (swipeDistance > dragThreshold) {
				if (startPos != Vector2.zero && endPos != Vector2.zero) {
					if (Mathf.Abs (swipeVerticalDistance) > Mathf.Abs (swipeHorizontalDistance) && Mathf.Sign (swipeVerticalDistance) > 0) { // going up
						vertical = 1;
						horizontal = 0;
					} else if (Mathf.Abs (swipeVerticalDistance) > Mathf.Abs (swipeHorizontalDistance) && Mathf.Sign (swipeVerticalDistance) < 0) { // going down
						vertical = -1;
						horizontal = 0;
					} else if (Mathf.Abs (swipeVerticalDistance) < Mathf.Abs (swipeHorizontalDistance) && Mathf.Sign (swipeHorizontalDistance) > 0) { // going down
						vertical = 0;
						horizontal = 1;
					} else if (Mathf.Abs (swipeVerticalDistance) < Mathf.Abs (swipeHorizontalDistance) && Mathf.Sign (swipeHorizontalDistance) < 0) { // going down
						vertical = 0;
						horizontal = -1;
					}
					startPos = Vector2.zero;
					endPos = Vector2.zero;
				}

				if (isFPS) {
					if (directionFacing == "up") {
						// do nothing cause it works normally
					} else if (directionFacing == "down") {
						horizontal = -horizontal;
						vertical = -vertical;
					} else if (directionFacing == "left") {
						float temp = vertical;
						vertical = horizontal;
						horizontal = -temp;
					} else if (directionFacing == "right") { 
						float temp = vertical;
						vertical = -horizontal;
						horizontal = temp;
					}
				}

				if ((vertical == -pastVert || horizontal == -pastHor) && snakeController.nBodyParts != 0) {
					vertical = pastVert;
					horizontal = pastHor;
				}
			} else {
				Jump ();
			}
		}
		Dragging = false;
	}
}
