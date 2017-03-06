using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.


	void Start () 
	{
		// Destroy the rocket after 1 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 4);
	}


	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(!col.tag.Equals("Obstacle") && !col.tag.Equals("Enemy") && !col.tag.Equals("ChaseEnemy") && !col.tag.Equals("EnemyBullet"))
		{
			// Do damage to Player here
			// ... find the Player script and call the Hurt function.
			//col.gameObject.GetComponent<PlayerHealth>().Hurt();

			// Call the explosion instantiation.
			if (col.tag.Equals ("Player")) {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<MonkeyControl> ().hurt ();
			}
			OnExplode();

			// Destroy the rocket.
			Destroy (gameObject);
		}
	}
}
