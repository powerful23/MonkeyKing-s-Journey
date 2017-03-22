using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
//	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
	public bool collisionDead = false;

	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private Transform frontCheckHero;
	private bool dead = false;			// Whether or not the enemy is dead.
	private bool isRushing = false;		// Whether or not the enemy is rushing.
//	private Score score;				// Reference to the Score script.

	private GameObject gameCtrl = null;




	
	void Awake()
	{
		// Setting up the references.
		ren = transform.GetComponent<SpriteRenderer>();
		frontCheck = transform.Find("frontCheck").transform;
		frontCheckHero = transform.Find ("frontCheckHero").transform;
//		score = GameObject.Find("Score").GetComponent<Score>();
	}


	void FixedUpdate ()
	{
		// Create an array of all the colliders in front of the enemy.
		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

		Collider2D[] frontHeroHits = Physics2D.OverlapPointAll (frontCheckHero.position);

		/*
		// Check each of the colliders.
		foreach(Collider2D c in frontHits)
		{
			// If any of the colliders is an Obstacle...
			if(c.tag == "Obstacle")
			{
				// ... Flip the enemy and stop checking the other colliders.
				Flip ();
				break;
			}
		}
		*/
		foreach(Collider2D c in frontHeroHits)
		{
			// If any of the colliders is an Obstacle...
			if(c.tag == "Player")
			{
				// damage the player
				//c.gameObject.GetComponent<PlayerHealth>().Hurt();
				// ... enemy will then rush to the player
				Rush();
				break;
			}
		}

		// Set the enemy's velocity to moveSpeed in the x direction.
		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

//		Debug.Log (GetComponent<Rigidbody2D> ().velocity);

		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
			
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();
	}
	
	public void Hurt()
	{
		// Reduce the number of hit points by one.
		HP--;
	}

	public void SetGameCtrl(GameObject obj){
		gameCtrl = obj;
	}

	public void Death()
	{

		if (gameCtrl != null) {
			gameCtrl.GetComponent<GameController> ().DecreaseEnemy ();
		}


		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		ren.sprite = deadEnemy;


		// Set dead to true.
		dead = true;

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		explode ();


		Destroy (gameObject, 2);
	}

	void explode(){
		
	}
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;

		if (transform.Find ("Ken_gun") != null)
		{
			Vector3 gun = transform.Find ("Ken_gun").localScale;
			gun.x *= -1;
			transform.Find ("Ken_gun").localScale = gun;
		}

		Vector3 frontcheck = transform.Find ("frontCheck").localScale;
		frontcheck.x *= -1;
		transform.Find ("frontCheck").localScale = frontcheck;

		Vector3 frontcheckhero = transform.Find ("frontCheckHero").localScale;
		frontcheckhero.x *= -1;
		transform.Find ("frontCheckHero").localScale = frontcheckhero;
	}

	public void Rush()
	{
		if (!isRushing) {
			isRushing = true;
			moveSpeed = moveSpeed * 2;
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag.Equals("Enemy")){
			
			Flip ();
		}
		else if (col.tag.Equals("Player") && collisionDead){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<MonkeyControl> ().death ();
		}
	}


}
