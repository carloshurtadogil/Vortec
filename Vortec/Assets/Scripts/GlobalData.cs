using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Global static class to house the game's main data
 */
public static class GlobalData {

	//Global Static variables
	private static int topScore = 0;//Standard, but if incremented, then it stays incremented
	private static bool lf = false;//Standard
	private static bool pause = false;//Allow the player to pause the menu at certain times
	private static bool save = false;//Allow the player to save at certain points in the game
	private static int score = 0, hazardCount = 0, levelCount = 0;//Standard 
	private static float audLevel = 1.0f, TimeInc = 1.0f;//Standard

	// Gets or sets the save
	public static bool Save {
		get { return save; }
		set { save = value; }
	}

	// Gets or sets the pause
	public static bool Pause {
		get { return pause; }
		set { pause = value; }
	}

	// Gets or sets the top score
	public static int HighScore {
		get { return topScore; }
		set { topScore = value; }
	}

	// Gets or sets the load factor
	public static bool LoadFactor {
		get { return lf; }
		set { lf = value; }
	}

	// Gets or sets the score
	public static int Score {
		get { return score; }
		set {  score = value; }
	}

	// Gets or sets the hazard count
	public static int HazardCount {
		get { return hazardCount; }
		set { hazardCount = value; }
	}

	// Gets or sets the level count
	public static int LevelCount {
		get { return levelCount; }
		set { levelCount = value; }
	}

	// Gets or sets the audio level
	public static  float AudLevel{
		get { return audLevel; }
		set { audLevel = value; }
	}

	// Gets or sets the time scale
	public static float TimeIncrement{
		get { return TimeInc; }
		set { TimeInc = value; }
	}

	// Check to see if the player as instantiated a new game
	public static bool NewGameCheck {
		get {
			return (score == 0 && hazardCount == 0 && levelCount == 0);
		}
	}

	public static void changed() {
		Debug.Log ("Load Factor: " + lf);
		Debug.Log ("Paused: " + pause);
		Debug.Log ("Save: " + save);
		Debug.Log ("Score: " + score);
		Debug.Log ("Hazard Count: " + hazardCount);
		Debug.Log ("LevelCount: " + levelCount);
		Debug.Log ("Audio Level: " + AudioListener.volume);
	}
}
