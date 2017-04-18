 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMoveController : MonoBehaviour {
	
	public float enemySpeed; //enemy speed

	Animator enemyAnimator;// change enemy state

	//facing
	public GameObject enemyGraphic; 
	bool canFlip = true; //hit player or player leave aggression zone than change facing
	bool facingRight = false;
	float flipTime = 5f; 
	float nextFlipChance = 0f;

	//attacking
	//player went the attack zone, player has time preparing and death start attack
	public float chargeTime;
	float startChageTime; 
	bool charging;
	Rigidbody2D enemyRB;


	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponentInChildren<Animator> ();
		enemyRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextFlipChance) {
			if (Random.Range (0, 10) >= 5) {
				flipFacing ();
			} nextFlipChance = Time.time + flipTime; // every 5s has a change to flip and chance is 50%
		}
	}

	//player enter danger zone
	void onTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (facingRight && other.transform.position.x < transform.position.x) {
				flipFacing ();
			} else if (!facingRight && other.transform.position.x > transform.position.x) {
				flipFacing ();
			}
			canFlip = false;
			charging = true;
			startChageTime = Time.time + chargeTime;
		}
	}

	//player in danger zone
	void onTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			if (startChageTime >= Time.time) {
				if (!facingRight)
					enemyRB.AddForce (new Vector2 (-1, 0) * enemySpeed);
				else enemyRB.AddForce (new Vector2 (1, 0) * enemySpeed);
				enemyAnimator.SetBool ("isCharging", charging);
			}
		}
	}

	//player leave danger zone
	void onTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			canFlip = true;
			charging = false;
			enemyRB.velocity = new Vector2 (0f, 0f);
			enemyAnimator.SetBool ("isCharging", charging);
		}
	}

	void flipFacing(){
		if (!canFlip)
			return;
		float facingX = enemyGraphic.transform.localScale.x;
		facingX *= -1f;
		enemyGraphic.transform.localScale = new Vector3 (facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
		facingRight = !facingRight;	
	}
}
