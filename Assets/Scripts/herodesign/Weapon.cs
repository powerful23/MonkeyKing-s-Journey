using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public Rigidbody2D bullet;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

	private MonkeyControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.
	private bool fireButtonClicked = false;

	void Awake()
	{
		// Setting up the references.
		anim = transform.parent.gameObject.GetComponent<Animator>();
		playerCtrl = transform.parent.GetComponent<MonkeyControl> ();
	}

	public void pressFire(){
		fireButtonClicked = true;
	}

	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1") || fireButtonClicked)
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.

			fireButtonClicked = false;
			anim.SetTrigger("Shoot");

			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(bullet, transform.GetChild(0).position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);


			}
			else
			{
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(bullet, transform.GetChild(0).position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);

			}
		}
	}
}

