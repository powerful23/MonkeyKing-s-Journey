using UnityEngine;
using System.Collections;

public class HeadCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Wall") {
			Debug.Log ("hit");
			gameObject.GetComponentInParent<DragonControl> ().stopRush ();
		}
	}
}
