using UnityEngine;
using System.Collections;

//attack mode:
// 0: noraml 30%
// 1: rush   30%
// 2: roar   30%
// 3: ulti   10%


public class DragonControl : MonoBehaviour {
	public float rushSpeed;		// speed of the rush
	public float roarTime = 2.0f;	// duration time for roar

	public GameObject headLaserEmitter;	// the laser emitter of the head
	public GameObject headLaserPartical;

	public float attackCoolDown = 3.0f;	// the cooldown between different modes
	public float ultiStandBy = 1.0f;
	public GameObject shield;

	public Transform ground;

	private float timer;	// the timer of the overall battle
	private float ultiStandByTimer;

	private Animator anim;	// the animator controls the dragon boss
	private Rigidbody2D rb;
	private int mode;		// attack mode
	private bool attacking;	// is the dragon attacking
	private Transform player;	// the transform of the player
	private bool facingRight;	// is the dragon facing right or left
	private float startRoar;	// the timer for the dragon when roaring
	private MyCamera myCamera;

	private LineRenderer headLR;	// the lineRenderer of the head laser

	private float angle;	// controls the head laser
	private GameObject tempShield;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		timer = Time.time;
		attacking = false;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MyCamera>();
		facingRight = false;

		headLR = headLaserEmitter.GetComponent<LineRenderer> ();

	}

	// Update is called once per frame
	void Update () {
		// control the dragon to always face the player
		if (player.transform.position.x < transform.position.x - 3.0f) {
			if (facingRight)
				Flip ();
		} else if (player.transform.position.x > transform.position.x + 3.0f) {
			if (!facingRight)
				Flip ();
		}

		//if not attacking and not in the cooldown
		if (!attacking && Time.time - timer > attackCoolDown) {
			// randomly select the mode
			int rnd = Random.Range (0, 100);
			// normal attack
			if (rnd >= 0 && rnd < 30) {
				mode = 0;
				attacking = true;
				ultiStandByTimer = Time.time;
				ultiReady ();
			}
			// rush mode
			else if (rnd >= 30 && rnd < 60) {
				mode = 1;
				rush ();
				attacking = true;
			}
			// roar mode
			else if (rnd >= 60 && rnd < 90) {
				mode = 2;
				roar ();
				attacking = true;
			}
			// ulti mode
			else {
				mode = 3;
			}
		} 
		else if (attacking){
			// if in roar mode and exceeds the roar time limitation, stop the roar mode;
			if (mode == 2 && Time.time - startRoar > roarTime) {
				stopRoar ();
			} else if (mode == 0 && ultiStandByTimer > 0 && Time.time - ultiStandByTimer > ultiStandBy) {
				ultiStandByTimer = -ultiStandByTimer;
				ulti ();
			}
		}
	}


	void FixedUpdate(){
		if (attacking) {
			// if in the ulti mode, emit the laser
			if (mode == 0 && ultiStandByTimer < 0) {
				Vector3 emitterPos = headLaserEmitter.transform.position;
				headLR.SetPosition(0, emitterPos);
				//headLR.SetPosition(1, new Vector3(0,0,0));
				RaycastHit2D hit = Physics2D.Raycast (new Vector2 (emitterPos.x, emitterPos.y), new Vector2 (angle, -1), 100, 1 << LayerMask.NameToLayer("Ground"));
				if (hit.collider != null) {
					headLR.SetPosition (1, hit.point);
					headLaserPartical.transform.position = hit.point;
					if (!facingRight)
						angle -= Time.deltaTime * 1f;
					else
						angle += Time.deltaTime * 1f;
				}
				else {
					stopUlti ();
				}

			}
		}
	}

	void stopRoar(){
		attacking = false;
		anim.SetBool ("roar", false);
		timer = Time.time;
		myCamera.shake = false;
	}


	public void stopRush(){
		attacking = false;
		anim.SetBool ("rush", false);
		rb.velocity = new Vector2 (0.0f, 0.0f);
		timer = Time.time;
	}

	void stopUlti(){
		attacking = false;
		anim.SetBool ("ulti", false);
		timer = Time.time;
		headLR.enabled = false;
		headLaserPartical.GetComponent<ParticleSystem> ().Stop ();
		if (tempShield != null)
			Destroy (tempShield);
	}

	void roar(){
		anim.SetBool ("roar", true);
		startRoar = Time.time;
		myCamera.shake = true;
	}

	void ultiReady(){
		anim.SetBool ("ulti", true);

		Vector3 shieldPos = new Vector3 ();
		shieldPos.x = player.position.x + Random.Range (-3f, 3f);
		shieldPos.y = ground.position.y + 1f;
		shieldPos.z = 0.0f;
		tempShield = Instantiate (shield, shieldPos, shield.transform.rotation) as GameObject;
	}
	void ulti(){		
		angle = 0.2f;
		headLR.enabled = true;
		headLaserPartical.GetComponent<ParticleSystem> ().Play ();
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
