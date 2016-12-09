// Namespaces that you might need 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextureScroll : MonoBehaviour 
{
	// Set the speed at which you want to move the texture
	// You want the units to be meters per second, the same as the treadmill
	public float scrollSpeed;
	// Instantiate the renderer to move the texture 
	public Renderer rend;
	// Make a reference to the object that you are grabbing your script from
	public GameObject inputField;
	// Starting point for going through the speeds 
	int i = 0;
	// Reference to the load screen
	public GameObject loadScreen;
	// Seconds that the loading screen loads between trials
	public float sec = 1f;

	void Start() {
		// sharedMaterial means that all objects that have this material will be affected
		// savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
		rend = GetComponent<Renderer>();
	}
		
	// Set the speed with a function
	void SetSpeed () {
		FlowSpeedCalculator flowSpeedScript = inputField.GetComponent<FlowSpeedCalculator> ();
		// Checks if a speed has been entered yet, and if so, start the trials!
		// If the spacebar is pressed, the scrollSpeed transitions to the next speed in the list
		if (flowSpeedScript.opticFlowSpeeds.Count == 0) {
			Debug.Log ("No speeds have been entered yet!");
		} else {
			// Thank you Brittany B for this simple solution <3
			// Check if the index is less than the size of the list
			// If you don't check it, you will get an Argument Out of Range error
			Debug.Log("The number of speeds is " + flowSpeedScript.opticFlowSpeeds.Count);
			// Fades in the loading screen 
			//GameObject.Find ("FadeManager").GetComponent<Fading> ().BeginFade (1);
			if (i < flowSpeedScript.opticFlowSpeeds.Count) {
				// When the space bar is pressed, transition to the next speed 
				if (Input.GetKeyDown ("space")) {
					StartCoroutine (LoadScreenActive ());
					Debug.Log ("Flowing at " + flowSpeedScript.opticFlowSpeeds [i] + " meters per second");
					if (this.tag == "Wall1") {
						scrollSpeed = flowSpeedScript.opticFlowSpeeds [i] * -1f * 0.1f;
					} else if (this.tag == "Wall2") {
						scrollSpeed = flowSpeedScript.opticFlowSpeeds [i] * 0.1f;
					} else {
						scrollSpeed = flowSpeedScript.opticFlowSpeeds [i] * -1f * 0.1f;
					}
					i = i + 1;
				}
			} else {
				Debug.Log ("Trials finished!");
				scrollSpeed = 0f;
			}
		}
	}

	// Function that activates loading screen 
	IEnumerator LoadScreenActive() {
		loadScreen.SetActive (true);
		yield return new WaitForSeconds (sec);
		loadScreen.SetActive (false);
	}
		
	void Update() {
		EmergencyStop ();
		SetSpeed ();
		// Repeats the texture that is moving on the tile
		// Maximum is 1, offsetting by 1 means that the image will just stay where is it
		// Equivalent to rotating something by 360 degrees 
		// Time.time is equal to the number of seconds that have elapsed since the project started
		// Time.deltatime is the time in seconds it took to complete the last frame 
		// Mathf.Repeat returns the remainder of the 1st argument divided by the 2nd argument 
		float offset = Time.time * scrollSpeed;
		// Checks to see whether the object is the Wall or the Floor 
		// _MainTex is the main diffuse texture that will be affected
		if (this.tag == "Wall1" || this.tag == "Wall2") {
			rend.material.SetTextureOffset ("_MainTex", new Vector2(offset,0));
		} else {
			rend.material.SetTextureOffset ("_MainTex", new Vector2 (0, offset));
		}
	}

	// Serves as an emergency pause button everytime the 1 key is pressed
	void EmergencyStop() {
		if (Input.GetKeyDown ("p")) {
			Time.timeScale = 0f;
		} else if (Input.GetKeyDown("s")) {
			Time.timeScale = 1f;
		}
	}
}