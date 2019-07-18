using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//14:14
/**
 * Master class that controls the hazard and enemy spawning, as well as score keeping and display
 */
public class GameManager: MonoBehaviour {
	public GameObject[] hazards;//Array of the different types of hazards that we will be throwing at the player
	public GameObject player, bgObject;
	public Vector3 spawnValues; //Position for which the hazard should initially spawn
	public int hazardCount;//The amount of hazards currently in the game
	public float spawnWait;//The period of time between each hazard entry before a hazard spawns into the game
	public float startWait;//The period of time before the wave begins to spawn
	public float waveWait;//The period of time between each waves
	public float gameOverCounter;

	public GameObject panel;
	public Save sd;
	public Button audBtn;//Buttons for the volume

	public UnityEngine.UI.Text scoreText;//Reference to the Score Text GUI object to be updated with the appropriate score
	public UnityEngine.UI.Text restartText;//Reference to the Restart Text GUI object to let the player know that they can restart the game
	public UnityEngine.UI.Text gameOverText;//Reference to the Game Over Text GUI object to let the player know that their death has ended the game

	private bool gameOver;//Tracks when the game is over
	private bool restart;//Tracks when it is appropriate to restart the game
	private bool ready;
	private Button btn;
	private int highscore = 0;
	private int score;//The player's current score
	private int quitInstance = 0;
	private int levelCount = 0; //The current 'level' the player is on
	private float timeInc = 1;
	private float r = 0.4f;
	private Vector3 pRate;

	public PlayerController p;
	public BGScroller bg;
	//public PauseGame pg;

	private bool phase1 = true;
	private bool phase2 = false;

	// Instantiate enemies and hazards into the game at any given point in time
	void Start()
	{
		score = 0; //All new games begin with a score of 0
		if(sd.HSFileExists()) {
			sd.LoadHSFile ();
			highscore = GlobalData.HighScore;
		}
		if (GlobalData.LoadFactor) {
			sd.LoadFile ();
			score = GlobalData.Score;
			hazardCount = GlobalData.HazardCount;
			levelCount = GlobalData.LevelCount;
			timeInc = GlobalData.TimeIncrement;
		}
		btn = audBtn.GetComponent<Button> ();
		btn.onClick.AddListener (changeClicked);
		if (AudioListener.volume == 1.0f) {
			btn.GetComponentInChildren<Text> ().text = "Turn Off Audio";
		} else {
			btn.GetComponentInChildren<Text> ().text =  "Turn On Audio";
		}
		Time.timeScale = 0.05f;
		pRate = new Vector3 (0, 0, r);
		ready = false;
		gameOver = false;//The game has started
		restart = false;//It is not the appropriate time to restart the game
		restartText.text = "";
		gameOverText.text = "";
		UpdateScore (); //Display Initial score to UI
	}

	// Constant check to see if the player is allowed to restart. Allows the player to press 'R' to restart the game after certain conditions have been met
	void Update(){

		//3 part code that animates the ship entering the playing  field
		if (!ready) {
			if (phase1 && player.transform.position.z < 10) {
				player.transform.position += (pRate);
				r *= Time.deltaTime;
				if (player.transform.position.z > 10) {
					phase1 = false;
					phase2 = true;
					r = 0.1f;
					pRate = new Vector3 (0, 0, r);
					bg.changeSSD(3.0f);
				}
			} else if (phase2 && player.transform.position.z > 2.9) {
				player.transform.position -= (pRate);
				r *= Time.deltaTime;
				bg.changeSS (3f * Time.deltaTime);
				if (player.transform.position.z < 2.9) {
					phase2 = false;
					Time.timeScale = 1.0f;
				}
			} else {
				p.changeZMin (); 
				bg.changeSSD (-0.225f);
				ready = true;
				GlobalData.Pause = true;
				StartCoroutine (AllowMovement());
				StartCoroutine(SpawnWaves ());
			}
		} else {
			if (restart) {//Check to see if the game has ended and the player has been notified that they may begin a new game
				if (Input.GetKeyDown (KeyCode.R)) {//Check to see if the player has pressed the 'R' button
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);//Reload the level
				}
			}
			if (gameOver) {
				GlobalData.Pause = false;
				gameOverCounter -= Time.deltaTime;
				if (gameOverCounter < 0 && quitInstance == 0) {
					quitInstance = 1;
					gameOverText.text = "";
					panel.SetActive (true);
				}
			}
		}
	}

	// Update the Score Text GUI to display the an update score
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
		GlobalData.Score = score;
	}

	// Spawn waves of hazards to the scene throughout the duration of the game until the player has been destroyed
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);//Give the player ample time to prepare before the first wave
		var worldToPixels = ((Screen.width / 2.0f) / Camera.main.orthographicSize) / 3;
		while (true) {//Begin spawning a new wave of hazards
			for(int i = 0; i < hazardCount; i++)
			{
				if (p != null) {
					GameObject hazard = hazards [Random.Range (0, hazards.Length)];//Spawn a random hazard from hazards array
					//the x-position will be randomized from a range specifed by -input and +input
					Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x*worldToPixels, spawnValues.x*worldToPixels), 
						spawnValues.y, spawnValues.z); //Control where in the game the hazard will spawn
					Quaternion spawnRotation = Quaternion.identity;// Hazard will not have another initial rotation value on instantiation
					Instantiate (hazard, spawnPosition, spawnRotation); //Add the hazard to the game
					yield return new WaitForSeconds(spawnWait);
				}
			} 
			hazardCount += (Random.Range(1,5));//Increment the amount of hazards in each wave
			timeInc += 0.02f;
			levelCount++;
			GlobalData.HazardCount = hazardCount;
			GlobalData.TimeIncrement = timeInc;
			GlobalData.LevelCount = levelCount;
			if (levelCount % 2 == 0 && p != null) { 
				sd.SaveFile ();
				if (score > highscore) {
					highscore = score;
					GlobalData.HighScore = highscore;
					sd.SaveHSFile ();
				}

			}
			Time.timeScale = timeInc; //Increase the speed of the game
			yield return new WaitForSeconds(waveWait);//Wait a certain time period before spawning a new wave

			if (gameOver) { //Check to see if the game has ended
				restart = true;
				break;//end the spawn wave by exiting out of the while loop
			}
		}
	}

	//Coroutine to allow the player to move once animation sequence is complete
	IEnumerator AllowMovement() {
		yield return new WaitForSeconds (1);
		p.canMove (true);
		StopCoroutine (AllowMovement ());
	}
		

	// Increment the score by a specified amount and update the GUI to alert the player of their current score
	public void AddScore(int val)
	{
		score += val;//increment the score value
		UpdateScore();
	}

	// Update the scene to notify the player that the  game is over
	public void GameOver()
	{
		gameOverText.text = "Game Over!"; //Notify the player that the game has ended
		gameOver = true;//Halts all hazards and objects from reappearing into the scene
	}

	// Change the audio to its opposite form
	void changeClicked() {
		if (AudioListener.volume == 1.0f) {
			turnOffAudio ();
			btn.GetComponentInChildren<Text> ().text = "Turn On Audio";
		} else {
			turnOnAudio ();
			btn.GetComponentInChildren<Text> ().text =  "Turn Off Audio";
		}
	}

	// Turn on the audio of the entire game
	void turnOnAudio() {
		AudioListener.volume = 1.0f;
		GlobalData.AudLevel = 1.0f;
	}

	// Turn off the audio of the entire game
	void turnOffAudio() {
		AudioListener.volume = 0.0f;
		GlobalData.AudLevel = 0.0f;
	}

	// Retrieve the amount of hazards that appear in the game
	public int getHazardCount() {
		return hazardCount;
	}

	// Change the number of hazards that appear in the game
	public void setHazardCount(int val) {
		hazardCount = val;
	}

	// Retrieve the number of 'levels' the player has accomplished
	public int getLevelCount() {
		return levelCount;
	}

	// Change the number of 'levels' the player has achieved
	public void setLevelCount(int val) {
		levelCount = val;
	}

	// Retrieve the current score the player has achieved
	public int getScore() {
		return score;
	}

	// Change the games score to a new value
	public void setScore(int val) {
		score = val;
	}
}
