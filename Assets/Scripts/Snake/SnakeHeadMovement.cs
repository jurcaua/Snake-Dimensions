using UnityEngine;
using System.Collections;

public class SnakeHeadMovement : MonoBehaviour, JumpingObject {

	public GameObject jumpTrigger;
	public float bodyPartTimeOut = 0.5f;

	private Rigidbody r;
	private float vertical = 0f;
	private float horizontal = -1f;
	private float speed;
	private float jumpValue;

	private SnakeController snakeController;

	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
		snakeController = GetComponentInParent<SnakeController> ();

		speed = snakeController.speed;
		jumpValue = snakeController.jumpValue;
	}

	void Update(){
		getInput ();
		//Debug.Log (r.velocity);
	}

	void FixedUpdate () {
		Move ();
	}

	void getInput(){
		if (Input.anyKeyDown) {
			float pastVert = vertical;
			float pastHor = horizontal;

			vertical = Input.GetAxisRaw ("Vertical");
			horizontal = Input.GetAxisRaw ("Horizontal");

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
		}

		if (Input.GetButtonDown("Jump") && onGround()){
			//snakeController.sendJump (transform.position, 0);
			//Destroy(GameObject.FindGameObjectWithTag("JumpTrigger"));
//			GameObject temp = Instantiate (jumpTrigger, transform.position, transform.rotation) as GameObject;
//			Destroy(temp, snakeController.nBodyParts * bodyPartTimeOut);
			Jump ();
			//r.AddForce (transform.up * jumpValue, ForceMode.Impulse);
		}

		if (Input.GetKeyDown ("r")) {
			snakeController.collisionText.text = string.Empty;
		}
	}

	void Move(){
		//Vector3 movement = new Vector3 (horizontal * speed * Time.deltaTime, 0f, vertical * speed * Time.deltaTime);

		r.velocity = new Vector3 (horizontal * speed, r.velocity.y, vertical * speed);
		//r.MovePosition(r.position + movement);
	}

	public Vector3 getPosition(){
		return transform.position;
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
		return Physics.Raycast (transform.position, Vector3.down, transform.lossyScale.x / 2 + 0.1f);
	}

	public void Jump(){
		GameObject temp = Instantiate (jumpTrigger, transform.position, transform.rotation) as GameObject;
		Destroy(temp, snakeController.nBodyParts * bodyPartTimeOut);
		r.velocity += new Vector3 (0, jumpValue, 0);
	}
}
