using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public Camera camera;

	public float smoothSpeed = 0.15f;
	public Vector3 offset;


	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float cameraSize;

	//deals with boss camera
	bool bossBounded = false;
	float bossBoundedRadius = 0.2f;
	public Transform bossBoundCheck;
	public LayerMask whatIsBossBound;

	void Update(){

		if (Application.loadedLevelName == "Stage_Three") {
			//check if player is in boss area
			bossBounded = Physics2D.OverlapCircle (bossBoundCheck.position, bossBoundedRadius, whatIsBossBound);

			//change camera position
			if (bossBounded) {
				camera.orthographicSize = cameraSize;
				transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax), Mathf.Clamp (target.position.y, yMin, yMax));
			}
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		Vector3 desiredPos = target.position + offset;
		Vector3 smoothedPos = Vector3.Lerp (transform.position, desiredPos, smoothSpeed);
		transform.position = smoothedPos;
	}
}
