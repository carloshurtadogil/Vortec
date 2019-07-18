using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that controls basic maneuvers for computer-controlled ships
 */
public class EnemyController : MonoBehaviour {

	public Boundary boundary;//The boundary that the computer-generated ship may not leave
	public Vector2 startWait;//Range that can be used to randomize when to start maneuvering
	public Vector2 maneuverTime;//How long to maneuver
	public Vector2 maneuverWait;//How long before manuevering
	public float dodge;//Our target manuever
	public float smoothing;//How fast should we move the ship in respect to frame rate
	public float tilt;//Angle that the ship should tilt when turning

	private float currentSpeed;//Current speed of the computer-controlled ship
	private float targetManeuver;//General direction to maneuver to
	private Rigidbody rb;//Reference to computer-controlled ship Game Object

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();//allow us to control the physics component of our ship
		currentSpeed = rb.velocity.z;//Always control the speed down the z-plane
		StartCoroutine (Evade());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),//Set the x-boundaries
			0.0f,//Set the y-boundaries
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)//Set the z-boundaries
		);
		rb.rotation = Quaternion.Euler (0.0f, 180f, rb.velocity.x * -tilt);//Control the tilt animation of the ship
	}

	// Basic ship maneuver tactics
	IEnumerator Evade()	{
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));//Wait some random time before maneuvering

		while (true) {
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign(transform.position.x); //Pick a random maneuver target
			//-Mathf.Sign will return the opposite Sign value to the x-position
			yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));//Continue in that direction for a certain period of time
			targetManeuver = 0;//Continue down towards the bottom of the screen
			yield return new WaitForSeconds (Random.Range(maneuverWait.x, maneuverWait.y));//Continue waiting before we move again in the loop
		}
	}
}