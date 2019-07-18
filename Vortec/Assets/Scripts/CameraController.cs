using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that sets the camera to a certain distance from the player
 */
public class CameraController : MonoBehaviour {

	public GameObject player; //The player that the camera will focus on
	private Vector3 offset; //The offset distance between the player and the camera

	// Use this for initialization
	void Start () {
		//calculate and store the offset value by fetting the distance between the ship's and camera's positions.
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//set the position of the camera's transform to be the same as the player's but offset by the calculated offset distance
		transform.position = player.transform.position + offset;
	}
}
