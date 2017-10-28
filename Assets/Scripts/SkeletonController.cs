using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

	//intialize classes
	private Rigidbody2D rigi;
	Animator anim;
	public Transform player;
	//public ConstantForce2D force;

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
		//force = GetComponent<ConstantForce2D> ();
		//constantForce.relativeForce = Vector2 (5, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat ("Speed", Mathf.Abs (move)); 
		rigi.velocity = new Vector2 (move * movementDirection, 0f);

		if (Vector3.Distance (player.position, this.transform.position) < 7.5) {
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
			Flip ();		}
	}

}
