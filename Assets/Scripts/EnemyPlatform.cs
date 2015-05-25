using UnityEngine;
using System.Collections;

public class EnemyPlatform : MonoBehaviour {

	public float maxSpeed = 0.6f;
	public float moveForce = 100f;

	private bool movingRight = true; 
	private Rigidbody2D rb2d;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		float direction;

		direction = (movingRight) ? 1 : -1;

		if (rb2d.velocity.x < maxSpeed) 
			rb2d.AddForce(Vector2.right * direction * moveForce);
		
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Wall") {
			movingRight = (movingRight) ? false : true;
		}
	}

}
