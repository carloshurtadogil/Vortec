using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that handles the rotation speed and directions of an asteroid
 */
public class RandomRotator : MonoBehaviour {

	public float rotSpeed; //Control the rotating speed

	// Initialize the asteroids rotation at the instance it appears in the game
	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * rotSpeed; //how fast a rigidbody object is rotating
	}
}
