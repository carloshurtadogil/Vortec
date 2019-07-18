using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class that closes the application when script is used
 */
public class QuitOnClick : MonoBehaviour {

	// Close the applications
	public void Quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
