using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/**
 * Class whose sole purpose is to store the high score for saving and loading purposes 
 */
public class HighScore {

	private int TopScore;

	// Default constructor
	public HighScore(int hScore) {
		TopScore = hScore;
	}

	// Return the high score
	public int getHS(){ return TopScore; }

	// Set the top score
	public void setHS(int val) {
		TopScore = val;
	}
}
