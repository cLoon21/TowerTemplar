using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealth : MonoBehaviour {

	Animator anim;

	public int health = 3;
	public bool beingDamaged = false;
	public bool damageCollide = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.P) && beingDamaged == false) {
			beingDamaged = true;
			anim.SetInteger ("Health", health);
			StartCoroutine (delayTime (2));
		}
		if (health == 0) {
			StartCoroutine (delayTime (1));
		}
	}
		
	IEnumerator delayTime(int time){
		yield return new WaitForSeconds (time);

		if (health > 0) {
			health--;
		} else if (health == 0) {
			Application.LoadLevel ("Main_Menu");
		}

		beingDamaged = false;
	}
}
