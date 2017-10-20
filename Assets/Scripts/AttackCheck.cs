using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour {

	bool attackEnemy = false;
	public Transform attackCheck;
	float attackRadius = 0.9f;
	public LayerMask whatIsEnemy;

	Animator anim;
	bool attack = false;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void HandleAttacks(){
		if (attack && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack")) {
			anim.SetTrigger ("attack");
		}
	}

	void HandleInput(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			attack = true;
		}
	}

	
	// Update is called once per frame
	void Update () {

		attackEnemy = Physics2D.OverlapCircle (attackCheck.position, attackRadius, whatIsEnemy);

		//print ("attack" + attack);
		//print ("Enemy" + attackEnemy);
		if (!attackEnemy && attack) {
			//register hit
			print("Hit Registered!");
		}

		HandleInput ();

	}

	void FixedUpdate(){
		HandleAttacks ();
		ResetValues ();
	}

	void ResetValues(){
		attack = false;
	}


}
