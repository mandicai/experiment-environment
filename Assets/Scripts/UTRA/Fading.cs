using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture; // The texture that will overlay everything
	public float fadeSpeed = 0.5f; // The fading speed 

	private int drawDepth = -1000; // The texture's order in the draw hierarchy, a low number means it's on top
	private float alpha = 0f; // The texture's alpha value between 0 and 1
	private int fadeDir; // The direction to fade, in = -1 or out = 1, referring to the scene not the texture

	// Called for rendering and handling GUI events 
	void OnGUI() {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture( new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}
		
	public void BeginFade (int direction) {
		fadeDir = direction;
	}
} 
