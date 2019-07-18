using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * Pause the game when the player has clicked
 */
public class PauseGame : MonoBehaviour {

	private bool pause = false;//Determine if the player can pause the game at the moment
	private float timeInc = 1;
	public GameObject[] panels; //The pause panel that is to be activated
	public GameObject offBtn, onBtn;//Volume buttons' game objects

	void Start() {
		GlobalData.Pause = false;
	}

	// Update the frame
	void Update () {
		pause = GlobalData.Pause;
		if (Time.timeScale > 0 && pause) { //Activate pause menu
			if (Input.GetKeyDown (KeyCode.P)) {
				timeInc = Time.timeScale; //Record the current time scale
				panels[0].SetActive (true);
			}
		} else { //Deactivate pause menu
			if (Input.GetKeyDown (KeyCode.P)) {
				foreach (GameObject panel in panels) {
					panel.SetActive (false);
				}
			}
		}
		if (panels[0].activeSelf || panels[1].activeSelf || panels[2].activeSelf || panels[3].activeSelf) { //Check if any  pause panels are active 
			Time.timeScale = 0;
		} else {
			Time.timeScale = timeInc;
		}
	}
}
