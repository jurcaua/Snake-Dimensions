using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public GameObject obj;

	private JumpingObject script;

	void Start(){
		script = obj.GetComponent<JumpingObject> ();
	}

	void OnTriggerEnter(Collider other){
		script.Jump ();
	}
}
