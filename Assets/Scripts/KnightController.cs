using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {
	//intialize classes
	private Rigidbody2D rigi;
	Animator anim;

	//deals with skeleton collision
	bool touching = false;
	float touchRadius = 0.4f;
	public Transform touchCheck;
	public LayerMask whatIsTouch;

	//setting max speed & jump power
	public float maxSpeed = 10f;
	public float jumpForce = 0.5f;
	public bool canDoubleJump = false;
	public float move = 0;

	//deals with bounds
	bool bounded = false;
	float boundRadius = 0.4f;
	public Transform boundsCheck;
	public LayerMask whatIsBound;

	//deals with portal
	bool portaled = false;
	float portalRadius = 0.5f;
	public Transform portalCheck;
	public LayerMask whatIsPortal;

	//deals with ground
	bool grounded = false;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;

	//bool for flipping character
	bool faceRight = true;

	//deals with attacking
	bool attacking = false;
	private float attackTimer = 10f;
	private float attackCoolDown = 0.3f;
	public Collider2D attackTrigger;

	//deals with edges
	public BoxCollider2D EdgeCheck;
	public BoxCollider2D EdgeTrigger;


	private void Awake(){
		//getting component of collision box
		rigi = GetComponent<Rigidbody2D>();
		attackTrigger.enabled = false;
	}

	// Use this for initialization
	void Start () {
		//getting component of animation controller
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		//update attacking
		if (!attacking && Input.GetKeyDown (KeyCode.Space)) {
			attacking = true;
			anim.Play ("Knight_Attack");
			attackTimer = attackCoolDown;
			attackTrigger.enabled = true;
		}

		if (attacking) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		}
		anim.SetBool ("Attack", attacking);

		//check if player is out of bound
		bounded = Physics2D.OverlapCircle (boundsCheck.position, boundRadius, whatIsBound);

		if (bounded) {
			Application.LoadLevel ("Main_Menu");
		}

		//check if player is touching skeleton
		touching = Physics2D.OverlapCircle (touchCheck.position, touchRadius, whatIsTouch);

		//check if player is on ground
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		//check if player is in portal
		portaled = Physics2D.OverlapCircle (portalCheck.position, portalRadius, whatIsPortal);

		//keypress gets horizontal axis and places into variable
		float move = Input.GetAxis ("Horizontal");

		//set animation speed by horizontal movement & if jumping
		anim.SetFloat ("Speed", Mathf.Abs(move));
		anim.SetBool ("Ground", grounded);

		//update x axis
		rigi.velocity = new Vector2(move * maxSpeed, rigi.velocity.y);

		//update portal
		if (Input.GetKeyDown (KeyCode.W) && portaled) {
			Application.LoadLevel ("Stage_Two");
		}

		//skeleton destroy check
		if (touching && attacking) {
			Destroy (GameObject.FindWithTag ("Skeleton"));
		}

		//double jump
		if (!grounded && canDoubleJump && Input.GetKeyDown (KeyCode.W)) {
			rigi.velocity = new Vector2 (move * maxSpeed, 0);
			anim.SetBool ("Ground", false);
			rigi.AddForce (Vector2.up * jumpForce);
			canDoubleJump = false;
		}

		//facing & switching direction
		if (move > 0 && !faceRight) {
			Flip ();
		} else if (move < 0 && faceRight) {
			Flip ();
		}

	}

	void Update () {
		//updating y axis (jumping)
		if ((grounded && Input.GetKeyDown (KeyCode.W)) || (grounded && Input.GetKeyDown(KeyCode.UpArrow))) {
			rigi.velocity = new Vector2 (move * maxSpeed, 0);
			anim.SetBool ("Ground", false);
			rigi.AddForce (Vector2.up * jumpForce);
			canDoubleJump = true;
		}
	}

	//function controls facing and switching direction
	void Flip(){
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		//flips character
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//get knocked back
	void OnCollisionExit2D(Collision2D col){

	}
}
