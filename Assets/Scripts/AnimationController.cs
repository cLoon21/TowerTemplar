using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	public Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.4f;
	public LayerMask whatIsGround;

	void Start () {
		anim = GetComponent<Animator> ();
	}    
		
	void Update () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		float move = Input.GetAxisRaw ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));


	}
		
}