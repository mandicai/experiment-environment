using UnityEngine;
using System.Collections;

public class TogglePanel : MonoBehaviour {

	// The purpose of this script is to toggle the panel on the screen
	// We store this script into a Game Manager, so that when the button is clicked, it calls this script
	// from the Game Manager and calls TogglePanelButton, which effects the panel itself

	public void TogglePanelButton (GameObject panel) {
		panel.SetActive(!panel.activeSelf);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
