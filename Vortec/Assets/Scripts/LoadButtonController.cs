using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class that controls when the load button should appear. Other functionality includes loading the main data file and adjusting volume when the game is initialized
 */
public class LoadButtonController : MonoBehaviour {

	public GameObject btn;
	public UnityEngine.UI.Text score, highscore, deleted;
	public Save sd;

	// Use this for initialization
	void Start () {
		if (sd.FileExists ()) {
			btn.SetActive (true);
			sd.LoadFile ();
			score.text = ("Current Score: " + GlobalData.Score);
		} else {
			btn.SetActive (false);
		}

		AudioListener.volume = GlobalData.AudLevel;
		if (sd.HSFileExists ()) {
			sd.LoadHSFile ();
			if (GlobalData.HighScore > 0) {
				highscore.text = "High Score: " + GlobalData.HighScore;
			} else {
				highscore.text = "";
			}
		} else {
			highscore.text = "";
		}

		if (deleted != null) {
			deleted.text = "";
		}
	}

	IEnumerator TextDuration() {
		yield return new WaitForSeconds (5);
		deleted.text = "";
		StopCoroutine (TextDuration());
	}

	// Update is called once per frame
	void Update () {
		if (sd.FileExists ()) {
			btn.SetActive (true);
		} else {
			btn.SetActive (false);
		}
	}

	public void fileDeleted() {
		sd.DeleteFile ();
		deleted.text = "File Terminated!";
		StartCoroutine (TextDuration());
	}
}
