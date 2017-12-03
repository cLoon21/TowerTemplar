using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {

	public float lifespan;

	public float speed;

	public KnightController player;

	public KnightHealth knightHealth;

	private Rigidbody2D rigi;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<KnightController>();
		knightHealth = FindObjectOfType<KnightHealth> ();
		rigi = GetComponent<Rigidbody2D> ();

		if (player.transform.position.x < transform.position.x) {
			speed = -speed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		rigi.velocity = new Vector2 (speed, rigi.velocity.y);
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Knight") {
			knightHealth.DecrementHealthFireball ();
			Debug.Log ("it hit me");
			Destroy (gameObject);
		}
		Destroy (gameObject);
	}

}
