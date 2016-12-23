using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public void Play(){
		SceneManager.LoadScene ("Main");
	}
}
