using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that destroys any object that leaves the boundary
 */
public class DestroyByBoundary : MonoBehaviour {
	// Destruction code that destroys game objects
	void OnTriggerExit(Collider other){
		Destroy (other.gameObject);
	}
}
