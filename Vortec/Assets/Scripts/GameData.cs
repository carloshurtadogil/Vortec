using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/**
 * Class to store the main data of the game when the player last saved the game
 */
public class GameData {

	public int score, hazardCount, levelCount;
	public float audLevel, TimeInc;

	// Store the main data of the game/
	public GameData(int pScore, int hCount, int levelC, float level, float inc)
	{
		score = pScore;
		hazardCount = hCount;
		levelCount = levelC;
		audLevel = level;
		TimeInc = inc;
	}

}
