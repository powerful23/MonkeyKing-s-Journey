using UnityEngine;
using System.Collections;

//attack mode:
// 0: noraml 30%
// 1: rush   30%
// 2: roar   30%
// 3: ulti   10%


public class DragonControl : MonoBehaviour {
	public float rushSpeed;
	public float rushCD = 2;
	public float[] cooldown;

	public float attackCoolDown = 3.0f;


	private float timer;
	private Animator anim;
	private Rigidbody2D rb;
	private int mode;
	private bool attacking;
	private CircleCollider2D cc;
	private Transform player;
	private bool facingRight;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		timer = Time.time;
		attacking = false;
		cc = GetComponent<CircleCollider2D> ();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		facingRight = false;
	}

	// Update is called once per frame
	void Update () {
		if (player.transform.position.x < transform.position.x - 3.0f) {
			if (facingRight)
				Flip ();
		} else if (player.transform.position.x > transform.position.x + 3.0f) {
			if (!facingRight)
				Flip ();
		}

		//if not attacking and not in the cooldown
		if (!attacking && Time.time - timer > attackCoolDown) {
			
			int rnd = Random.Range (0, 100);
			if (rnd >= 0 && rnd < 30) {
				mode = 0;
			} else if (rnd >= 30 && rnd < 60) {
				mode = 1;
				rush ();
				attacking = true;
			} else if (rnd >= 60 && rnd < 90) {
				mode = 2;
				roar ();
				attacking = true;
			} else {
				mode = 3;
			}
		}
	}


	public void stopRush(){
		attacking = false;
		anim.SetBool ("rush", false);
		rb.velocity = new Vector2 (0.0f, 0.0f);
		timer = Time.time;
	}

	void roar(){
		anim.SetBool ("roar", true);


	}

	void rush(){
		anim.SetBool ("rush", true);
		// rush to the player
		if (facingRight) rb.velocity = new Vector2 (transform.right.x * rushSpeed * Time.deltaTime, rb.velocity.y);
		else rb.velocity = new Vector2 (-transform.right.x * rushSpeed * Time.deltaTime, rb.velocity.y);
	}

	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
		facingRight = !facingRight;
	}
}
