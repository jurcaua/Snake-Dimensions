using UnityEngine;
using System.Collections;

public class SnakeBodyMovement : MonoBehaviour {

	[HideInInspector] public Vector3 jumpLocation = Vector3.up;
	[HideInInspector] public int bodyNum;

	private Rigidbody r;
	private Vector3 currentDest;
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

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Move ();
	}

	public void setDestination(Vector3 dest){
		currentDest = dest;
		setMovement ();
	}

	public Vector3 getPosition(){
		return transform.position;
	}

	public void setMovement(){
		vertical = Mathf.Clamp(currentDest.z - transform.position.z, -1f, 1f); // either gonna be -1 or 1 or 0 and same below
		horizontal = Mathf.Clamp(currentDest.x - transform.position.x, -1f, 1f);

//		horizontal = hor;
//		vertical = vert;
	}

	void Move(){
//		Vector3 movement = new Vector3 (horizontal * speed * Time.deltaTime, 0f, vertical * speed * Time.deltaTime);
//
//		r.MovePosition (r.position + movement);

		r.velocity = new Vector3 (horizontal * speed, r.velocity.y, vertical * speed);
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
