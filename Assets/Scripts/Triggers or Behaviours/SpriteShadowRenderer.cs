using UnityEngine;
using System.Collections;

public class SpriteShadowRenderer : MonoBehaviour {

	private SpriteRenderer s;

	// Use this for initialization
	void Start () {
		s = GetComponent<SpriteRenderer> ();

		s.receiveShadows = true;
		s.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
