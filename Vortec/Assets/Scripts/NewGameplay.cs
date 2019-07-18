using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class to load a scene depending on the index
 */
public class NewGameplay : MonoBehaviour {

	public bool loadfactor; //Determines wheter or not to load data from file or simply start a new game

	// Load the a scene using the SceneManager based of the scene index 
	public void LoadByIndex(int index) {
		SceneManager.LoadScene (index); 
		if (index != 0) {
			GlobalData.LoadFactor = loadfactor;
		}
	}
}
