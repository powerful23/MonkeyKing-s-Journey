using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour
{
	public Rigidbody2D bullet;				// Prefab of the rocket.
	public float speed = 15f;				// The speed the rocket will fire at.

	void Start ()
	{
		InvokeRepeating ("Fire", 1f, 1f);
	}
		
	void Fire()
	{
		Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
		if(transform.localScale.x > 0)
		{
			bulletInstance.velocity = new Vector2 (speed, 0);
		}
		else
		{
			bulletInstance.velocity = new Vector2 (speed * -1, 0);
		}
	}
}
