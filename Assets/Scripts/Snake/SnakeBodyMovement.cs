using UnityEngine;
using System.Collections;

public class SnakeBodyMovement : MonoBehaviour {

	[HideInInspector] public Vector3 jumpLocation = Vector3.up;
	[HideInInspector] public int bodyNum;

	[HideInInspector] public Rigidbody r;
	private Vector3 currentDest;
	//private Rigidbody currentDest;
	private float vertical = 0f;
	private float horizontal = -1f;
	//private float speed;
	//private float jumpValue;
	[HideInInspector] public string directionFacing = "left";

	private GameObject snake;
	private SnakeController snakeController;
	[HideInInspector] public SphereCollider collider;

	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
		snakeController = GetComponentInParent<SnakeController> ();
		collider = GetComponent<SphereCollider> ();

		snake = GameObject.FindGameObjectWithTag ("Snake");

		//speed = snakeController.speed;
		//jumpValue = snakeController.jumpValue;

		Debug.Log (bodyNum);
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Move ();
		Rotate ();
	}

	public void setDestination(Vector3 dest){
	//public void setDestination(Rigidbody dest){
		currentDest = dest;
		setMovement ();
	}

	public Vector3 getPosition(){
		return transform.position;
	}

	public void setMovement(){
		vertical = Mathf.Clamp(currentDest.z - transform.position.z, -1f, 1f); // either gonna be -1 or 1 or 0 and same below
		horizontal = Mathf.Clamp(currentDest.x - transform.position.x, -1f, 1f);

//		float posZ = currentDest.position.z - Mathf.Clamp (currentDest.velocity.z, -1f, 1f);
//		float posX = currentDest.position.x - Mathf.Clamp (currentDest.velocity.x, -1f, 1f);
//
//		vertical = Mathf.Clamp(posZ - transform.position.z, -1f, 1f);
//		horizontal = Mathf.Clamp(posX - transform.position.x, -1f, 1f);

//		horizontal = hor;
//		vertical = vert;
	}

	void Move(){
//		Vector3 movement = new Vector3 (horizontal * speed * Time.deltaTime, 0f, vertical * speed * Time.deltaTime);
//
//		r.MovePosition (r.position + movement);

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
		transform.rotation = targetRotation; // smooth rotation
	}

	public float getHorizontal(){
		return horizontal;
	}

	public float getVertical(){
		return vertical;
	}

	public void Jump(){
		//r.AddForce (transform.up * jumpValue, ForceMode.Impulse);
		if (onGround ()) {
			float jumpValue = snakeController.jumpValue;
			r.velocity += new Vector3 (0, jumpValue, 0);
		}
	}

	bool onGround(){
		return Physics.Raycast (transform.position, Vector3.down, transform.lossyScale.x / 2 + 0.1f);
	}

	void OnTriggerEnter(Collider other){
//		Debug.Log (other.gameObject.name);
//		if (other.gameObject.name == "SnakeHead") 
//			snakeController.collisionText.text = "Collision Detected!";
//		}
		if (bodyNum != 0 && bodyNum != 1) {
			snakeController.checkCollision (other);
		}
	}
}
