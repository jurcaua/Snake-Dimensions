using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public Music[] backgroundMusic;
	public Text credsText;

	private AudioSource audioSource;

	private static BackgroundMusic instance = null;

	void Awake(){
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);

		audioSource = GetComponent<AudioSource> ();
		playSong ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying) {
			audioSource.Play ();
		}
	}

	void playSong(){
		int rand = UnityEngine.Random.Range (0, backgroundMusic.Length);
		audioSource.clip = backgroundMusic [rand].music;
		credsText.text = "Music Playing Credits: " + backgroundMusic [rand].credits;

		audioSource.Play ();
	}

	public static BackgroundMusic Instance{
		get { return instance; }
	}
}

[Serializable]
public struct Music {
	public string credits;
	public AudioClip music;
}