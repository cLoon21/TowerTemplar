using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

	public KnightHealth knightHealth;

	//intialize classes
	private Rigidbody2D rigi;
	Animator anim;
	public Transform player;
	//public ConstantForce2D force;

	//decrement knight health
	bool touching = false;
	float touchRadius = 0.4f;
	public Transform touchCheck;
	public LayerMask whatIsTouch;

	//setting max speed
	public float move = 5.0f;
	public float movementDirection = 1;

	//bool for flipping character
	bool faceRight = true;

	//deals with attacking
	public bool attacking = false;
	private float attackTimer = 10f;
	private float attackCoolDown = 0.3f;
	public Collider2D attackTrigger;
	Vector3 direction;

	//deals with edges
	public BoxCollider2D EdgeCheck;
	public BoxCollider2D EdgeTrigger;

	//deals with ground
	bool grounded = false;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	//deals with boss camera
	bool bossBounded = false;
	float bossBoundedRadius = 0.2f;
	public Transform bossBoundCheck;
	public LayerMask whatIsBossBound;

	/*private void Awake(){
		//getting component of collision box
		attackTrigger.enabled = false;
	}*/

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigi = GetComponent<Rigidbody2D>();
		EdgeCheck = GetComponent<BoxCollider2D> ();
		EdgeTrigger = GetComponent<BoxCollider2D> ();
		Flip ();
		if (Application.loadedLevelName == "Stage_Three") {
			rigi.gravityScale = 0;
		}
		//force = GetComponent<ConstantForce2D> ();
		//constantForce.relativeForce = Vector2 (5, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Application.loadedLevelName == "Stage_Three") {
			//check if player is in boss area
			bossBounded = Physics2D.OverlapCircle (bossBoundCheck.position, bossBoundedRadius, whatIsBossBound);
			//check if player is on ground
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			if (bossBounded) {
				rigi.gravityScale = 6;
			}
			if (!grounded) {
				move = 0.0f;
			}
		}

		//check if player is touching skeleton
		touching = Physics2D.OverlapCircle (touchCheck.position, touchRadius, whatIsTouch);

		//skeleton decrement knight health
		if (touching && attacking) {
			knightHealth.DecrementHealth ();
		}

		anim.SetFloat ("Speed", Mathf.Abs (move)); 
		rigi.velocity = new Vector2 (move * movementDirection, 0f);

		if (Vector3.Distance (player.position, this.transform.position) < 2.5) {
			move = 0f;
			if (player.position.x - this.transform.position.x < 0 && movementDirection > 0) {
				Flip ();
			} else if (player.position.x - this.transform.position.x > 0 && movementDirection < 0) {
				Flip ();
			}
		} else {
			move = 5f;
		}
			attacking = true;
			anim.Play ("Knight_Attack");
			attackTimer = attackCoolDown;
			attackTrigger.enabled = true;
		anim.SetBool ("Attack", attacking);
	}

	//function controls facing and switching direction
	void Flip(){
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		//flips character
		theScale.x *= -1;
		transform.localScale = theScale;
		movementDirection *= -1;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Edge") {
			Flip ();		
		}
		else if (col.gameObject.tag == "Player") {
			Debug.Log ("He hit me");
			knightHealth.DecrementHealth ();
		}
	}

}
