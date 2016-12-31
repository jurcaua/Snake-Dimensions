using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	private SnakeController snakeController;

	void Start(){
		Time.timeScale = 1;

		snakeController = GameObject.FindGameObjectWithTag ("Snake").GetComponent<SnakeController> ();
		snakeController.addBodyPart (5);
	}

	public void Play(){
		SceneManager.LoadScene ("Main");
	}

	public void Controls(){
		SceneManager.LoadScene ("ControlsMenu");
	}
}
