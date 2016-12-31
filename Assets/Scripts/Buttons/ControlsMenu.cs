﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlsMenu : MonoBehaviour {

	public GameObject[] controlPanels;
	public Text nextText;

	private int currentPanel = 0; // do by index cause its easier

	// Use this for initialization
	void Start () {
		controlPanels [0].SetActive (true);
		for (int i = 1; i < controlPanels.Length; i++) {
			controlPanels [i].SetActive (false);
		}
	}
	
	public void Next(){
		if (nextText.text == "Play") {
			SceneManager.LoadScene ("Main");
		} else {
			currentPanel++;
			if (currentPanel < controlPanels.Length) {
				controlPanels [currentPanel - 1].SetActive (false);
				controlPanels [currentPanel].SetActive (true);
			} else {
				nextText.text = "Play";
			}
		}
	}
}
