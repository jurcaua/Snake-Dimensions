﻿using UnityEngine;
using System.Collections;

public class FPSPickUp : MonoBehaviour {

	private Camera main;
	private Camera fps;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		GameObject fpsCamera = GameObject.FindGameObjectWithTag ("FPSCamera");
		GameObject mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		main = mainCamera.GetComponent<Camera> ();
		fps = fpsCamera.GetComponent<Camera> ();
		gameManager = mainCamera.GetComponent<GameManager> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "SnakeHead") {
			main.enabled = false;
			fps.enabled = true;
			other.gameObject.GetComponent<SnakeHeadMovement> ().isFPS = true;
			Destroy (gameObject);
			gameManager.SpawnPickUp ();
		}
	}
}