using UnityEngine;
using System.Collections;

public class KeepInPos : MonoBehaviour {

	public GameObject sphere;
	public Material green;
	public Material red;

	// Use this for initialization
	void Start () {
		sphere.GetComponent<Renderer>().material = green;
	}

	void Update() {
		//Debug.Log (transform.position);

		if (transform.position.z > .3 || transform.position.z < -.2) {
			sphere.GetComponent<Renderer> ().material = red;
		} else {
			sphere.GetComponent<Renderer> ().material = green;
		}
	}

	/*
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.CompareTag ("Front")) {
			Debug.Log ("FRONT!");
			sphere.GetComponent<Renderer>().material = red;
		} else if (collider.gameObject.CompareTag ("Back")) {
			Debug.Log ("BACK!");
			sphere.GetComponent<Renderer>().material = red;
		}
	}

	void onTriggerExit(Collider collider) {
		if (collider.gameObject.CompareTag("Front") || collider.gameObject.CompareTag("Back")) {
			Debug.Log ("CENTER!");
			sphere.GetComponent<Renderer>().material = green;
		}
	}
	*/
}