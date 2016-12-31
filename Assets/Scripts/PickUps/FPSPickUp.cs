using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class FPSPickUp : MonoBehaviour {

	public int scoreValue;

	private Camera main;
	private Camera fps;
	private GameManager gameManager;

	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		GameObject fpsCamera = GameObject.FindGameObjectWithTag ("FPSCamera");
		GameObject mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		main = mainCamera.GetComponent<Camera> ();
		fps = fpsCamera.GetComponent<Camera> ();
		gameManager = mainCamera.GetComponent<GameManager> ();

		sprite = GetComponent<SpriteRenderer> ();
		sprite.receiveShadows = true;
		sprite.shadowCastingMode = ShadowCastingMode.On;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Head") {
			gameManager.addScore (scoreValue);
			gameManager.collectSound.Play ();

			main.enabled = false;
			fps.enabled = true;
			other.gameObject.GetComponent<SnakeHeadMovement> ().isFPS = true;

			Destroy (gameObject);
			gameManager.SpawnPickUp ();
		}
	}
}
