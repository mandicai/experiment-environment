using UnityEngine;
using System.Collections;
using System;

// This script is responsible for the initial load screen deactivating itself after a couple seconds 
public class InitialLoadScreenInactive : MonoBehaviour {

	// The amount of time you want the loading screen to be active between trials
	public float sec = 25f;
	// The load screen between trials
	public GameObject loadScreen;

	void Start () {
		StartCoroutine (LoadScreenInactive ());
	}

	IEnumerator LoadScreenInactive()
	{
		yield return new WaitForSeconds (sec);
		if (loadScreen.activeInHierarchy == true) {
			loadScreen.SetActive (false);
		}
	}
}
