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
		if (col.tag.Equals("Enemy")){
			//col.gameObject.GetComponent<Enemy>().death();
		}
		else if (col.tag.Equals("DragonBoss")){
			col.gameObject.GetComponent<DragonControl>().hurt();
		}
		Destroy (gameObject);
	}
}
