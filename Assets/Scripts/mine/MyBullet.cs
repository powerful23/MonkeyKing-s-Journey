using UnityEngine;
using System.Collections;

public class MyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log ("hit");
		Destroy (gameObject);
	}
}
