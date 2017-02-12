using UnityEngine;
using System.Collections;

public class WheelSpawner : MonoBehaviour {

	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float spawnTime = 1.0f;
	public float speed = 2f;				// The speed the rocket will fire at.
	public Transform parent;

	private Animator anim;					// Reference to the Animator component.


	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		InvokeRepeating("Spawn",  0, spawnTime);
	}



	void Spawn ()
	{
		// Instantiate a rocket
		Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, transform.rotation) as Rigidbody2D;
		bulletInstance.velocity = new Vector2 ((transform.position.x - parent.position.x)*speed, (transform.position.y - parent.position.y)*speed);

	}
}
