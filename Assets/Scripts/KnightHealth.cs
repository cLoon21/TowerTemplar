using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealth : MonoBehaviour {

	Animator anim;

	public int health = 100;
	public bool beingDamaged = false;
	public bool damageCollide = false;

	//deals with boss camera
	bool bossBounded = false;
	float bossBoundedRadius = 0.2f;
	public Transform bossBoundCheck;
	public LayerMask whatIsBossBound;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetInteger ("Health", health);
	}
	
	// Update is called once per frame
	void Update () {
		//check if player is in boss area
		bossBounded = Physics2D.OverlapCircle (bossBoundCheck.position, bossBoundedRadius, whatIsBossBound);

		if (bossBounded) {
			transform.localScale = new Vector3 (1.5f, 1.5f, 1);
			transform.localPosition = new Vector3 (-9, 6.5f, 0.3f);
		}


		/*
		if ("COLLISION" && !beingDamaged) {
			beingDamaged = true;

			StartCoroutine (delayTime (2));
		}
		if (health == 0) {
			StartCoroutine (delayTime (1));
		}
		*/
	}
		
	IEnumerator delayTime(int time){
		yield return new WaitForSeconds (time);
	}

	public void DecrementHealth(){

		StartCoroutine (delayTime (10));

		Debug.Log ("Ouch");
		health--;
		anim.SetInteger ("Health", health);
		if (health <= 0) {
			Debug.Log ("Im dead");
			if (Application.loadedLevelName == "Stage_One") {
				Application.LoadLevel ("Stage_One");
			} else if (Application.loadedLevelName == "Stage_Two") {
				Application.LoadLevel ("Stage_Two");
			} else if (Application.loadedLevelName == "Stage_Three") {
				Application.LoadLevel ("Stage_Three");
			}
		}
	}

	public void DecrementHealthFireball(){

		StartCoroutine (delayTime (10));

		Debug.Log ("Ouch");
		health -= 12;
		anim.SetInteger ("Health", health);
		if (health <= 0) {
			Debug.Log ("Im dead");
				if(Application.loadedLevelName == "Stage_One"){
					Application.LoadLevel ("Stage_One");
				}else if (Application.loadedLevelName == "Stage_Two") {
					Application.LoadLevel ("Stage_Two");
				}else if (Application.loadedLevelName == "Stage_Three") {
					Application.LoadLevel ("Stage_Three");
				}
		}
	}
}
