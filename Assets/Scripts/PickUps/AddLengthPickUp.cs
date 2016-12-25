﻿using UnityEngine;
using System.Collections;

public class AddLengthPickUp : MonoBehaviour {

	public int toAdd;

	private GameManager gameManager;
	private SnakeController snakeController;
	private SnakeHeadMovement snakeHead;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameManager> ();
		snakeController = GameObject.FindGameObjectWithTag ("Snake").GetComponent<SnakeController> ();
		snakeHead = GameObject.FindGameObjectWithTag ("Head").GetComponent<SnakeHeadMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head") {
			gameManager.ResetCamera (); // if we were in fps mode, we exit
			snakeHead.isFPS = false;

			snakeController.addBodyPart (toAdd); // add the specified number of body parts

			Destroy (gameObject); // destroy this pick up now
			gameManager.SpawnPickUp (); // spawn another one
		}
	}
}
