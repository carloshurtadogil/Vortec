using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class the removes the referenced game object from the game after a certain period of time
 */
public class DestroyByTime : MonoBehaviour {
	public float lifeTime; //The amount of time a game object will be in our game
	void Start()
	{
		Destroy (gameObject, lifeTime); //Destroy the game object after a certain period of time has elapsed
	}
}
