﻿using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;


	void Awake () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();

	}

	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
			GameObject fill = GameObject.Find("Fill");
			HealthController health = (HealthController) fill.GetComponent(typeof(HealthController));
			health.getHit(collision.gameObject.tag);
		}
	}

	void FixedUpdate (){
		float h = Input.GetAxis ("Horizontal");
		print (Mathf.Abs (h));
		if (Mathf.Abs (h) > 0)
			anim.SetBool ("Speed", true);
		else
			anim.SetBool ("Speed", false);

		if (h * rb2d.velocity.x < maxSpeed) 
			rb2d.AddForce(Vector2.right * h * moveForce);

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();


		if (jump) {
			anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

	}

	
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
