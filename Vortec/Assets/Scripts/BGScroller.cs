using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to control the speed of the background
 */
public class BGScroller : MonoBehaviour {

	public float scrollSpeed;//The speed that the background is scrolled through
	public float tileSizedZ;//The size of the tile on the z-plan
	public Vector3 startPosition;//Initial position of the background

	// Use this for initialization
	void Start () {
		startPosition = transform.position;//Set the initial position of the background as the starting position
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizedZ);//New position of the background (dependant on the time and the speed the background should move)
		transform.position = startPosition + Vector3.forward * newPosition;//Update the position of the background to give the illusion that it is moving
	}

	//Changes the scroll speed of the background. To be used for animation sequence.
	public void changeSS(float amount) {
		if (scrollSpeed > -0.225f)
			scrollSpeed -= amount;
		else
			scrollSpeed = -0.225f;
	}

	//Changes the scroll speed of the background. To be used when increasing the time scale of the game
	public void changeSSD(float amount) {
		scrollSpeed = amount;
	}

	//Change the size of the background.
	public void changeScale(Vector3 scale) {
		transform.localScale = scale;
	}
}
