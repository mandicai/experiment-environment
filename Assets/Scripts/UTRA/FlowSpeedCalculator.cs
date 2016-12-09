using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System;

public class FlowSpeedCalculator : MonoBehaviour {

	// public InputField mainInputField;
	// Declare a variable for the Input Field text
	// public float PWS; 

	//Create a list to store the optic flow speeds
	public List<float> opticFlowSpeeds = new List<float>();
	// Create a variable to store the preferred walking speed that you enter into the start window 
	public float preferredWalkingSpeed;

	void Start () {
		// Grab the input field component from the current game object 
		var input = gameObject.GetComponent<InputField> ();
		// InputField.SubmitEvent means you create an event
		var se = new InputField.SubmitEvent ();
		// AddListener refers to the concept of event listener
		// An event listener waits for a specific action to happen, like a mouse click
		// . AddListener adds a non persistent event listener to the object 
		// Non-persistent means that it will be forgotten when you exit play mode 
		se.AddListener (Calculator);
		// onEndEdit means when the editing has ended 
		input.onEndEdit = se;
		// Basically this code is saying WHEN THE EDITING HAS ENDED, CALL THE FUNCTION SUBMITNAME
	}
		
	public List<float> Randomize(List<float> numbers) {
		List<float> randomized = new List<float>();
		List<float> original = new List<float>(numbers);
		System.Random r = new System.Random();
		while (original.Count > 0) {
			int index = r.Next(original.Count);
			randomized.Add(original[index]);
			original.RemoveAt(index);
		}

		return randomized;
	}

	public void Calculator(string pws) {
		// Converts input variable from a string to a float
		float.TryParse(pws, out preferredWalkingSpeed);

		// Manipulate for the different trials
		// Change these percentages eventually 
		opticFlowSpeeds.Add (preferredWalkingSpeed/60); // 60 accounts for the conversation from m/min to m/sec
		opticFlowSpeeds.Add (preferredWalkingSpeed/60 * 0.75f);
		opticFlowSpeeds.Add (preferredWalkingSpeed/60 * 0.85f);
		opticFlowSpeeds.Add (preferredWalkingSpeed/60 * 1.15f);
		opticFlowSpeeds.Add (preferredWalkingSpeed/60 * 1.25f);
		// opticFlowSpeeds.Add (0.0f);
		// Print the list elements to the console
		// foreach (object i in opticFlowSpeeds) {
			// Convert.ToString(i);
			// Debug.Log(i);
		opticFlowSpeeds = Randomize(opticFlowSpeeds);
	}
}
