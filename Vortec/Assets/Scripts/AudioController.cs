using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class to control the overall audio in the main menu
 */
public class AudioController : MonoBehaviour {

	public Button audButton;//The button being affected
	private Button btn;//New instance of aud button for editing purposes

	// Use this for initialization
	void Start () {
		AudioListener.volume = GlobalData.AudLevel;//When game first starts, then volume is always 1.0f
		btn = audButton.GetComponent<Button> ();
		btn.onClick.AddListener (changeClicked);
		if (AudioListener.volume == 1.0f) {
			btn.GetComponentInChildren<Text> ().text = "Turn Off Audio";
		} else {
			btn.GetComponentInChildren<Text> ().text =  "Turn On Audio";
		}
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
}
