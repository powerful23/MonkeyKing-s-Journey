using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroy : MonoBehaviour {
	public float destroyTime = 10.0f;
	// Use this for initialization
	void Start () {

		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, destroyTime);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col){
		if (!col.tag.Equals("Rock"))
			Destroy (gameObject);


	}
}
