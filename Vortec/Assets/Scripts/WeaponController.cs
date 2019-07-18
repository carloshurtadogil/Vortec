using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class to control the enemy weapons system
 */
public class WeaponController : MonoBehaviour {

	public GameObject shot;//Reference to a bolt game object to be deployed when fired
	public Transform shotSpawn;//Area in the scene where the shot will spawn from
	public float fireRate;//How fast the bolt will transform across the scene
	public float delay;//Wait time before instantiating bolt objects

	private AudioSource audioSource; //The weapon's audio source when fired

	// Use this for initialization
	void Start () {
		audioSource = GetComponent <AudioSource> ();
		InvokeRepeating ("Fire", delay, Random.Range (0.5f, fireRate));//Repetitively invoke the fire shot at
	}

	/**
	 * Fire the enemy's weapon
	 */
	void Fire(){
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
}
