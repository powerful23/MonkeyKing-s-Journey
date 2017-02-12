using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour {
	public int direction;
	public float bound1;
	public float bound2;
	public float speed;
	public int stayTime;

	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		ChangeSpeed ();
	}
	
	void FixedUpdate(){
		//left and reach the bound
		if (direction == 1 && transform.position.x <= bound1 && rb.velocity.x < 0) {
			direction = 2;
			ChangeSpeed ();
		}
		//right and reach the bound
		if (direction == 2 && transform.position.x >= bound2 && rb.velocity.x > 0) {
			direction = 1;
			ChangeSpeed ();
		}
		//down and reach the bound
		if (direction == 3 && transform.position.y <= bound1 && rb.velocity.y < 0) {
			direction = 4;
			ChangeSpeed ();
		}
		//up and reach the bound
		if (direction == 4 && transform.position.y >= bound2 && rb.velocity.y > 0) {
			direction = 3;
			ChangeSpeed ();
		}


	}

	void ChangeSpeed(){
		if (direction == 1) {
			rb.velocity = new Vector2 (-speed, 0);
		} else if (direction == 2) {
			rb.velocity = new Vector2 (speed, 0);
		} else if (direction == 3) {
			rb.velocity = new Vector2 (0, -speed);
		} else {
			rb.velocity = new Vector2 (0, speed);

		}
	}
}
