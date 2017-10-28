using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealth : MonoBehaviour {

	Animator anim;

	public int health = 100;
	public bool beingDamaged = false;
	public bool damageCollide = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetInteger ("Health", health);
	}
	
	// Update is called once per frame
	void Update () {
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
			Application.LoadLevel ("Main_Menu");
		}
	}
}
