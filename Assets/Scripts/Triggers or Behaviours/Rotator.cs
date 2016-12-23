using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float rotateAngle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, rotateAngle * Time.deltaTime, 0));
	}
}
