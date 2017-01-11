using UnityEngine;
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

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Back ();
		}
	}
	
	public void Next(){
		if (nextText.text == "Start Menu") {
			SceneManager.LoadScene ("StartMenu");
		} else {
			currentPanel++;
			if (currentPanel < controlPanels.Length) {
				controlPanels [currentPanel - 1].SetActive (false);
				controlPanels [currentPanel].SetActive (true);
			}
			if (currentPanel == controlPanels.Length - 1){
				nextText.text = "Start Menu";
			}
		}
	}

	void Back(){
		if (currentPanel == 0) {
			SceneManager.LoadScene ("StartMenu");
		} else {
			currentPanel--;
			controlPanels [currentPanel + 1].SetActive (false);
			controlPanels [currentPanel].SetActive (true);

			nextText.text = "Next";
		}
	}
}
