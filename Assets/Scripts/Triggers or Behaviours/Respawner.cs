using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

	public GameObject spawn;
	public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Transform[] nodes = obj.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < nodes.Length; i++) {
			nodes [i].position = spawn.transform.position;
		}
	}
}
