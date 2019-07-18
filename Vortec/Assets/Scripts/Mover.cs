using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls the speed and movement of the weapon that was deployed
 */
public class Mover : MonoBehaviour {
	public float speed; //Variable that controls how fast the bolt can move forward

	// Use this for initialization the first frame the bolt appears
	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}
}
