using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//allow the boundary class to be visibile to Unity
/**
 * Class that sets the boundaries that the player can play in
 */
public class Boundary
{
	public float xMin, xMax, zMin, zMax; //Variables the define the x-z boundaries for the game
}

/**
 * Class that handles the players control of the ship
 */
public class PlayerController : MonoBehaviour 
{
	// Variables to constain certain apects of the ship
	public float speed;//Variable representing the maximum speed the player can travel
	public float tilt;//Variable that allows the ship to rotate a little when moving back and forth
	public Boundary boundary; //Instantiate a boundary object

	//Variables to constrain certain aspects of the ship's projectiles
	public GameObject shot;
	public Transform shotArea;

	public float fireRate; //Constant to determine how fast consecutive shots may be fired
	public float nextFire;

	private bool movement = false;

	void Start(){
		//movement = true;
		//boundary.zMin = -4;
		increaseBoundary();
	}

	// Updates physics to the x-z plane
	void FixedUpdate()
	{
		if (movement) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 move = new Vector3 (moveHorizontal, 0.0f, moveVertical); //constrain player movement to x-z plane
			GetComponent<Rigidbody>().velocity = move * speed; //the maximum velocity the player can travel
			GetComponent<Rigidbody>().position = new Vector3 
				(
					Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), //set the x-boundaries
					0.0f,//set the y-boundaries
					Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax) //set the z-boundaries
				); //set the boundaries for the game

			GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		}
	}

	// Allow the ship to shoot bolts projectiles
	void Update(){
		if (movement) {
			if (Input.GetButton("Jump") && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotArea.position, shotArea.rotation);//create an instance of a shot to when fired
				GetComponent<AudioSource>().Play();
			} 
		}
	}

	//Increase the size of the player boundary depending on the size of the screen
	void increaseBoundary() {
		var worldToPixels = ((Screen.width / 2.0f) / Camera.main.orthographicSize);
		boundary.zMax += 2;
		boundary.xMin *= (worldToPixels/3 - 5);
		boundary.xMax *= (worldToPixels/3 - 5);
		
	} 

	// After entrance animation, change southern boundary so that player may not escape
	public void changeZMin() {
		boundary.zMin = -4;
	}

	// After entrance animation, player may not control the ship
	public void canMove(bool val) {
		movement = val;
	}
}
