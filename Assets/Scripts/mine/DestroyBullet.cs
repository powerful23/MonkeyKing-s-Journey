using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {
	public float destroyTime;
	// Use this for initialization
	void Start () {
	
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		Destroy (gameObject);
	}
}
