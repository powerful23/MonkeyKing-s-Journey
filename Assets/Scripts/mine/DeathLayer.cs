using UnityEngine;
using System.Collections;

public class DeathLayer : MonoBehaviour {
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"){
			col.GetComponent<MonkeyControl> ().death ();
		//	playerControl.death ();
		}
	}
}
