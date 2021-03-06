using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpTriggerBypass : MonoBehaviour {

	//private SnakeController snakeController;
	private HashSet<int> alreadyJumped;

	// Use this for initialization
	void Start () {
		//snakeController = GameObject.Find("Snake").GetComponent<SnakeController> ();
		alreadyJumped = new HashSet<int> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		SnakeBodyMovement temp = other.gameObject.GetComponent<SnakeBodyMovement> ();
		if (other.gameObject.CompareTag("Body") && !alreadyJumped.Contains(temp.bodyNum)) {
			alreadyJumped.Add (temp.bodyNum);
			temp.Jump ();
//			if (temp.bodyNum == snakeController.nBodyParts - 1){ // because index
//				Destroy(this.gameObject);
//			}
		}
	}
}
