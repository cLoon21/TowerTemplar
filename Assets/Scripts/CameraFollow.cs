using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public Camera camera;

	public float smoothSpeed = 0.15f;
	public Vector3 offset;

	//deals with boss camera
	bool bossBounded = false;
	float bossBoundedRadius = 0.2f;
	public Transform bossBoundCheck;
	public LayerMask whatIsBossBound;

	void FixedUpdate (){

		//check if player is in boss area
		bossBounded = Physics2D.OverlapCircle (bossBoundCheck.position, bossBoundedRadius, whatIsBossBound);

		if (bossBounded) {
			camera.orthographicSize = 13.5f;
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		Vector3 desiredPos = target.position + offset;
		Vector3 smoothedPos = Vector3.Lerp (transform.position, desiredPos, smoothSpeed);
		transform.position = smoothedPos;

	}
}
