using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour
{
	public Rigidbody2D bullet;				// Prefab of the rocket.
	public float speed = 15f;				// The speed the rocket will fire at.
	public float attackCoolDown = 2.0f;

	void Start ()
	{
		InvokeRepeating ("Fire", 0f, attackCoolDown);
	}
		
	void Fire()
	{
		
		if(transform.localScale.x > 0)
		{
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (speed, 0);
		}
		else
		{
			Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0,0,180))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (speed * -1, 0);
		}
	}
}
