using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class Save : MonoBehaviour {
	//Data to store
	private bool display;
	private int score, hazardCount, levelCount;
	private float audLevel, TimeInc;
	private GameData data;//Primary game data

	//public UnityEngine.UI.Text scoreText;
	public UnityEngine.UI.Text saveText;


	// Initialize animation components
	void Start() {
		display = false;
		if (saveText != null) {
			saveText.text = "";
		}
	}

	// Alert the player that their progress has been saved
	void Update() {
		if (display) {
			saveText.text = "Progress Saved";
			StartCoroutine (Wait ());
		} 
	}

	// Wait a certain amount of time to allow process to be carried out
	IEnumerator Wait() {
		yield return new WaitForSeconds (5);
		display = false;
		saveText.text = "";
		StopCoroutine (Wait ());
	}

	// Check if the main data file exists
	public bool FileExists() {
		string destination = Application.persistentDataPath + "/save.dat";
		return File.Exists (destination);
	}

	// Check if the high score file exists
	public bool HSFileExists() {
		string destination = Application.persistentDataPath + "/savehs.dat";
		return File.Exists (destination);
	}

	public GameData getData() {
		return data;
	}

	public void DeleteFile () {
		string destination = Application.persistentDataPath + "/save.dat";
		if (File.Exists (destination)) {
			File.Delete (destination);
			RefreshEditorProjectWindow ();
		}
		if(File.Exists (destination)) 
			Debug.Log("Still exists");
	}

	// For testing purposes
	public void DeleteHSFile() {
		string destination = Application.persistentDataPath + "/savehs.dat";
		if (HSFileExists ()) {
			File.Delete (destination);
			RefreshEditorProjectWindow ();
		}

		if(File.Exists (destination)) 
			Debug.Log("HS still exists");
		else
			Debug.Log("HS File is gone");
	}

	// Save the all data into an internal file
	public void SaveFile() {
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if(File.Exists(destination)) 
			file = File.OpenWrite(destination);
		else 
			file = File.Create(destination);

		score = GlobalData.Score;
		hazardCount = GlobalData.HazardCount;
		levelCount = GlobalData.LevelCount;
		audLevel = GlobalData.AudLevel;
		TimeInc = GlobalData.TimeIncrement;

		GameData data = new GameData(score, hazardCount, levelCount, audLevel, TimeInc);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
		if (saveText != null) {
			display = true;
		}
	}

	// Save the high score in a separate save file
	public void SaveHSFile() {
		string destination = Application.persistentDataPath + "/savehs.dat";
		FileStream file;

		if(File.Exists(destination)) 
			file = File.OpenWrite(destination);
		else 
			file = File.Create(destination);

		int highscore = GlobalData.HighScore;
		HighScore hs = new HighScore (highscore);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, hs);
		file.Close();
	}

	// Read save data for high score
	public void LoadHSFile (){
		string destination = Application.persistentDataPath + "/savehs.dat";
		FileStream file;

		if (File.Exists (destination)) {
			file = File.OpenRead (destination);
		} else {
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		HighScore hs = (HighScore) bf.Deserialize(file);
		file.Close();

		GlobalData.HighScore = hs.getHS();
	}

	// Read save data file and load the values to continue user experience
	public void LoadFile() {
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if (File.Exists (destination)) {
			file = File.OpenRead (destination);
		} else {
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		data = (GameData) bf.Deserialize(file);
		file.Close();

		GlobalData.Score = data.score;
		GlobalData.HazardCount = data.hazardCount;
		GlobalData.LevelCount = data.levelCount;
		GlobalData.TimeIncrement = data.TimeInc;

	}



	// Refresh the assets stored within the game
	void RefreshEditorProjectWindow() 
	{
		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}
}
