using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofBounds : MonoBehaviour {

	bool bounded = false;
	public Transform OutofBoundsCheck;
	float boundRadius = 0.9f;
	public LayerMask whatIsBound;

	void Start () {
		
	}

	void Update () {
		bounded = Physics2D.OverlapCircle (OutofBoundsCheck.position, boundRadius, whatIsBound);

		if (bounded) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}
