using UnityEngine;
using System.Collections;

public class DeathLayer : MonoBehaviour {
	public PlayerControl playerControl; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"){
			Debug.Log ("hit");
			playerControl.death ();
		}
	}
}
