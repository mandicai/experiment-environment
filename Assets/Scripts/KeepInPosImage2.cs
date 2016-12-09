using UnityEngine;
using System.Collections;

public class KeepInPosImage2 : MonoBehaviour {

	public GameObject plane;
	public Material fast;
	public Material slow;
	public Material good;

	// Use this for initialization
	void Start () {
		plane.GetComponent<Renderer>().material = good;
	}

	// Runs every frame
	void Update() {
		if (transform.position.z > .4) {
			plane.GetComponent<Renderer> ().material = slow;
		}
		else if (transform.position.z < -.05) {
			plane.GetComponent<Renderer> ().material = fast;
		}
		else {
			plane.GetComponent<Renderer> ().material = good;
		}
	}
}