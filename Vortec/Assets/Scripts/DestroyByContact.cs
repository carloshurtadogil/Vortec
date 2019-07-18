using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that handles object destructions in the case that another game object collides with an asteroid
 */
public class DestroyByContact : MonoBehaviour {
	public GameObject explosion; //Explosion game object that is instantiated when an object(s) is/are destroyed
	public GameObject playerExplosion; //Explosion game object specified for player explosion
	public int scoreValue;//The amount of points that the player has earned

	private GameManager gameManager;//Reference to Game Controller object so that methods in DestroyByContact may be called

	void Start() {
		GameObject gameManagerObject = GameObject.FindWithTag ("GameController"); //Find the instance of GameController by searching for the tag
		if (gameManagerObject != null) { //Make sure that a GameController object exists in the game
			gameManager = gameManagerObject.GetComponent<GameManager> ();//Instantiate the gameManager object
		}
		//Checkpoint to see if the gameManager has been initialized
		if (gameManager == null) {
			Debug.Log ("Cannot find 'gameManager' script");
		}
	}

	// Destroys asteroid and anything that comes into contac
	void  OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundary" ) //Ignore the Boundary object and enemy object
		{
			return;
		}

		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);//play the explosion animation
		}
		if(other.tag == "Player"){//Check to see if the player has collided with this object
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation); //activates player explosion since its the other Collider object
			gameManager.GameOver();
		}
		gameManager.AddScore (scoreValue); 
		Destroy (other.gameObject); //Destroys other game object that collides with this object
		Destroy (gameObject); //Destroys this game object when it collides with another 
	}
}
