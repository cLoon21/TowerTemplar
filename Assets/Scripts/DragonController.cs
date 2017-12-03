using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

	Animator anim;

	public float playerRange;

	public GameObject fireball;

	public GameObject skeleton;

	public KnightController player;

	public Transform launchPoint;
	public Transform launchPointSkele;
	public Transform launchPointSkele2;
	public Transform launchPointSkele3;

	public float delay;
	public float fireballCounter;

	public float skeletonCounter;
	public float delaySkeleton;

	//deals with hurt animation
	bool hurting = false;
	float hurtRadius = 2.5f;
	public Transform hurtCheck;
	public LayerMask whatIsHurt;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<KnightController> ();
		fireballCounter = delay;
		skeletonCounter = delaySkeleton;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (new Vector3 (transform.position.x - playerRange, transform.position.y, transform.position.z), 
			new Vector3 (transform.position.x + playerRange, transform.position.y, transform.position.z));

		fireballCounter -= Time.deltaTime;

		//check if dragon is hurt
		hurting = Physics2D.OverlapCircle (hurtCheck.position, hurtRadius, whatIsHurt);

		if (hurting) {
			anim.SetBool ("Hurt", hurting);
		}

		anim.SetBool ("Hurt", hurting);

		if (skeletonCounter < 0) {
			Instantiate (skeleton, launchPointSkele.position, launchPointSkele.rotation);
			Instantiate (skeleton, launchPointSkele2.position, launchPointSkele2.rotation);
			Instantiate (skeleton, launchPointSkele3.position, launchPointSkele3.rotation);
		}

		if (fireballCounter < 0.5f && !hurting) {
			anim.SetBool ("Attack", true);
		}

		if (player.transform.position.x < transform.position.x && player.transform.position.x > (transform.position.x - playerRange) && fireballCounter < 0 && !hurting) {
			Instantiate (fireball, launchPoint.position, launchPoint.rotation);
			fireballCounter = delay;
			anim.SetBool ("Attack", false);
		}
	}
}
