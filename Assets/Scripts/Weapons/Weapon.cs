using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	public float speed = 20f;				// The speed the rocket will fire at.
	public Rigidbody2D normalBullet;
	public Rigidbody2D superBullet;
	public Rigidbody2D bombBullet;
	public Rigidbody2D flameBullet;

	public float superFireCoolDown;

	public Vector2 bombForce;

	private MonkeyControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.
	private bool fireButtonClicked = false;
	private Rigidbody2D bullet;				// Prefab of the rocket.
	// 0: normal, 1: super, 2:bomb, 3:flame
	public int weaponMode;
	private bool isHoldFire = false;
	private float lastFireTime = 0.0f;
	private Rigidbody2D currentFlameBullet;

	void Awake()
	{
		// Setting up the references.
		anim = transform.parent.gameObject.GetComponent<Animator>();
		playerCtrl = transform.parent.GetComponent<MonkeyControl> ();

	}

	public void pressFire(){
		fireButtonClicked = true;
	}

	public void holdFire(){
		isHoldFire = true;
	}

	public void releaseFire(){
		isHoldFire = false;
	}

	public void switchWeapon(int mode){
		weaponMode = mode;
	}

	void Update ()
	{
		// in normal mode
		if (weaponMode == 0) {
			bullet = normalBullet;
			// If the fire button is pressed...
			if (Input.GetButtonDown ("Fire1") || fireButtonClicked) {
				// ... set the animator Shoot trigger parameter and play the audioclip.
				fireButtonClicked = false;
				anim.SetTrigger ("Shoot");
				GetComponent<AudioSource> ().Play ();

				// If the player is facing right...
				if (playerCtrl.facingRight) {
					// ... instantiate the rocket facing right and set it's velocity to the right. 
					Rigidbody2D bulletInstance = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (speed, 0);
				} else {
					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
					Rigidbody2D bulletInstance = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2 (-speed, 0);
				}
			}
		}
		// in super mode
		else if (weaponMode == 1) {
			bullet = superBullet;
			if (Input.GetButton ("Fire1") || isHoldFire) {
				if (Time.time - lastFireTime > superFireCoolDown) {
					anim.SetTrigger ("Shoot");
					GetComponent<AudioSource> ().Play ();

					// If the player is facing right...
					if (playerCtrl.facingRight) {
						// ... instantiate the rocket facing right and set it's velocity to the right. 
						Rigidbody2D bulletInstance = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
						bulletInstance.velocity = new Vector2 (speed, 0);
					} else {
						// Otherwise instantiate the rocket facing left and set it's velocity to the left.
						Rigidbody2D bulletInstance = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
						bulletInstance.velocity = new Vector2 (-speed, 0);
					}

					lastFireTime = Time.time;
				}

			}
		}
		// in bomb mode
		else if (weaponMode == 2) {
			bullet = bombBullet;
			if (Input.GetButtonDown ("Fire1") || fireButtonClicked) {
				// ... set the animator Shoot trigger parameter and play the audioclip.
				fireButtonClicked = false;
				anim.SetTrigger ("Shoot");
				GetComponent<AudioSource> ().Play ();

				// If the player is facing right...
				if (playerCtrl.facingRight) {
					// ... instantiate the rocket facing right and set it's velocity to the right. 
					Rigidbody2D bulletInstance = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
					bulletInstance.AddForce (new Vector2(bombForce.x, bombForce.y));
				} else {
					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
					Rigidbody2D bulletInstance = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 180f))) as Rigidbody2D;
					bulletInstance.AddForce (new Vector2(-bombForce.x, bombForce.y));
				}
			}
		}
		// in flame mode
		else if (weaponMode == 3) {
			bullet = flameBullet;
			// If the fire button is pressed...
			if (Input.GetButtonDown ("Fire1") || fireButtonClicked) {
				// ... set the animator Shoot trigger parameter and play the audioclip.
				fireButtonClicked = false;

				currentFlameBullet = Instantiate (bullet, transform.GetChild (0).position, Quaternion.Euler (new Vector3 (0, 0, 0))) as Rigidbody2D;
			}
			if (!Input.GetButton ("Fire1")) {
				if (currentFlameBullet != null) {
					Destroy (currentFlameBullet.gameObject);
					currentFlameBullet = null;
				}
			} else {
				Vector3 targetPos = gameObject.transform.position;

				if (playerCtrl.facingRight) {
					currentFlameBullet.transform.position = new Vector3 (targetPos.x + 0.6f, targetPos.y, targetPos.z);
					currentFlameBullet.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
				} else {
					currentFlameBullet.transform.position = new Vector3 (targetPos.x - 0.6f, targetPos.y, targetPos.z);
					currentFlameBullet.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 270));
				}
			}





		}

	}
}

