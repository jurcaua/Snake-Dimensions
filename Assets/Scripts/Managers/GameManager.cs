using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

	[Header("Cameras")]
	public Camera main;
	public Camera fps;

	[Header("Game Area")]
	public GameObject gameArea;
	public float spawnPadding;
	public float noSpawnRadius = 4f;

	[Header("UI")]
	public Text scoreText;
	[Space(5)]
	public Image pausePlayImage;
	public Sprite pauseSprite;
	public Sprite playSprite;
	[Space(5)]
	public GameObject highscoresPanel;
	public Text[] scoreTexts;
	public Animator continueFlasher;
	public float timeBeforeRestart;

	[Space(10)]

	public AudioSource collectSound;
	public GameObject[] pickUps;

	private Renderer rend;
	//private SnakeController snakeController;
	private int lastPickUp = 0;
	private int score = 0;
	[HideInInspector] public bool Paused = false;
	private bool GameOver = false;
	private List<int> highscores;
	private int maxHighscores = 5;
	private string highscorePath;
	private bool gotHighscore = false;
	private int highscoreIndex;
	private float currentTime = 0f;

	void Awake(){
		highscorePath = Application.persistentDataPath + "/highscores.dat";

		Load ();
	}

	void OnApplicationQuit(){
		//Save ();
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;

		ResetCamera ();

		rend = gameArea.GetComponent<Renderer> ();

		//snakeController = GameObject.FindGameObjectWithTag ("Snake").GetComponent<SnakeController> ();

		SpawnPickUp ();

		scoreText.text = "0";

		pausePlayImage.sprite = pauseSprite;

		highscoresPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if (GameOver) {
			//Debug.Log (Time.fixedTime);
			if (Time.realtimeSinceStartup > currentTime + timeBeforeRestart && Input.anyKey) {
				SceneManager.LoadScene ("Main");
			}
		}
	}

	public void SpawnPickUp() {
		float randX = 0; // initializing
		float randZ = 0;
		int randPickUp = -1; // cant be neagtiev one cause thats lastPickUps' initialized value

		bool found = false; 
		while (!found) { // get random positions on the game area
			randX = UnityEngine.Random.Range ((gameArea.transform.position.x - rend.bounds.size.x / 2) + spawnPadding, (gameArea.transform.position.x + rend.bounds.size.x / 2) - spawnPadding);
			randZ = UnityEngine.Random.Range ((gameArea.transform.position.z - rend.bounds.size.z / 2) + spawnPadding, (gameArea.transform.position.z + rend.bounds.size.z / 2) - spawnPadding);

			Debug.DrawRay(new Vector3 (randX, 5f, randZ), Vector3.down * 10, Color.red, 2f);

			RaycastHit hit;
			if (!Physics.SphereCast (new Vector3 (randX, 5f, randZ), noSpawnRadius, Vector3.down, out hit, 10f, 9)) { // check if theres anything where we are spawning the pickup
				found = true;
			} 
		}

		randPickUp = UnityEngine.Random.Range (0, pickUps.Length); // random pickup from avaliable pickups
		while (pickUps[randPickUp].name == pickUps[lastPickUp].name && pickUps[lastPickUp].name == "FPSPickUp") {
			randPickUp = UnityEngine.Random.Range (0, pickUps.Length);
		}

		lastPickUp = randPickUp;
		//Debug.Log(pickUps[lastPickUp].name);
		Instantiate (pickUps [randPickUp], new Vector3 (randX, pickUps [randPickUp].transform.position.y, randZ), pickUps [randPickUp].transform.rotation);
	}

	public void RestartLevel(){
//		snakeController.ResetVelocities ();

//		GameObject[] jumpTriggers = GameObject.FindGameObjectsWithTag("JumpTrigger");
//		for (int i = 0; i < jumpTriggers.Length; i++) {
//			Destroy (jumpTriggers [i]);
//		}

		Time.timeScale = 0;

		// update the score in the system
		UpdateHighscores ();

		// update the text values
		UpdateHighscoreText();

		// turn on this shit
		highscoresPanel.SetActive (true);
		continueFlasher.Play ("PressToContinueClip");

		GameOver = true;
		currentTime = Time.realtimeSinceStartup;
	}

	void UpdateHighscores(){
		if (highscores.Count == 0) {
			highscores.Add (score);
		} else {
			for (int i = 0; i < highscores.Count; i++) {
				if (score > highscores [i]) {
					highscores.Insert (i, score);
					gotHighscore = true;
					highscoreIndex = i;
					break;
				}
			}

			if (highscores.Count > maxHighscores) {
				for (int i = highscores.Count - 1; i > maxHighscores - 1; i--) {
					highscores.RemoveAt (i);
				}
			}
		}

		Save ();
	}

	void UpdateHighscoreText(){
		for (int i = 0; i < highscores.Count; i++) {
			scoreTexts [i].text = highscores [i].ToString();
		}

		if (gotHighscore) {
			scoreTexts[highscoreIndex].text += " NEW HIGHSCORE!";
		}
	}

	public void ResetCamera(){
		main.enabled = true;
		fps.enabled = false;
	}

	public void addScore(int toAdd){
		score += toAdd;
		scoreText.text = score.ToString();
	}

	public void Pause(){
		if (!Paused) { // game was playing, want to pause
			pausePlayImage.sprite = playSprite;
			Paused = true;
			Time.timeScale = 0;

		} else { // game was paused, want to play
			pausePlayImage.sprite = pauseSprite;
			Paused = false;
			Time.timeScale = 1;
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (highscorePath);

		Highscores scores = new Highscores ();
		scores.highscores = highscores;

		bf.Serialize (file, scores);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (highscorePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (highscorePath, FileMode.Open);
			Highscores scores = (Highscores)bf.Deserialize (file);
			file.Close ();

			highscores = scores.highscores;
		} 
		if (highscores == null) {
			highscores = new List<int>();
		}
	}
}