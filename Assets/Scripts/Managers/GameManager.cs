using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Camera main;
	public Camera fps;

	// Use this for initialization
	void Start () {
		main.enabled = true;
		fps.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
