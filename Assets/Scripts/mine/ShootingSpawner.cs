using UnityEngine;
using System.Collections;

public class ShootingSpawner : MonoBehaviour {

	public Rigidbody2D rocket;				// Prefab of the rocket.
	public int spawnTime = 1;
	public float speed = 2f;				// The speed the rocket will fire at.
	public int direction = 3;

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

		//left
		if (direction == 1) {
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (-speed, 0);
		}
		//right
		else if (direction == 2) {
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (speed, 0);
		}
		//down
		else if (direction == 3) {
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,-90))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (0, -speed);
		}
		//up
		else if (direction == 4) {
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,90))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (0, speed);
		}
	}


}
