using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This script is for taking text entered into the input field in the start panel
public class InputFieldGrabber : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		// Grab the input field component from the current game object 
		var input = gameObject.GetComponent<InputField> ();
		// InputField.SubmitEvent means you create an event
		var se = new InputField.SubmitEvent ();
		// AddListener refers to the concept of event listener
		// An event listener waits for a specific action to happen, like a mouse click
		// . AddListener adds a non persistent event listener to the object 
		// Non-persistent means that it will be forgotten when you exit play mode 
		se.AddListener (SubmitName);
		// onEndEdit means when the editing has ended 
		input.onEndEdit = se;
		// Basically this code is saying WHEN THE EDITING HAS ENDED, CALL THE FUNCTION SUBMITNAME
	}

	private void SubmitName(string arg0) {
		// Print the text input to the console
		Debug.Log (arg0);
	}
}
