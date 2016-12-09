using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour {

	// Stuff you need to declare before you even start programming!
	// Screen to open automatically at the start of the Scene
	public Animator initiallyOpen;

	//Currently open Scene
	private Animator m_Open;

	//Hash of the parameter we use to control the transitions
	private int m_OpenParameterID;

	private GameObject m_PreviouslySelected;

	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";

	public void OnEnable () {
		m_OpenParameterID = Animator.StringToHash (k_OpenTransitionName);

		// Open the initial screen if nothing is initially open
		if (initiallyOpen == null)
			return;
		OpenPanel (initiallyOpen);
	}

	// Closes the currently open panel and opens the provided one
	// Take cares of handling the navigation, sets the new Selected element 
	public void OpenPanel (Animator anim) {
		if (m_Open == anim)
			// You don't need curly brackets for an if function that returns nothing
			return;

		// Set the scene as active
		anim.gameObject.SetActive (true);
		// currentSelectedGameObject = The GameObject currently considered active by the EventSystem
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;
		// Bring the scene to the front
		anim.transform.SetAsLastSibling ();

		// Close the current scene
		CloseCurrent ();

		m_PreviouslySelected = newPreviouslySelected;

		//Set the new Screen as the open one
		m_Open = anim;
		m_Open.SetBool(m_OpenParameterID, true);

		GameObject go = FindFirstEnabledSelectable (anim.gameObject);
		SetSelected(go);
	}

	//Finds the first Selectable element in the providade hierarchy.
	static GameObject FindFirstEnabledSelectable (GameObject gameObject) {
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	// Closes the currently open Sceen
	// Takes care of navigation
	// Reverts selection to the Selectable used before opening the current screen

	public void CloseCurrent() {
		if (m_Open == null)
			return;
		
		m_Open.SetBool (m_OpenParameterID, false);
		SetSelected (m_PreviouslySelected);
		StartCoroutine (DisablePanelDelayed (m_Open));
		m_Open = null;
	}

	IEnumerator DisablePanelDelayed(Animator anim) {
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose) {
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

			wantToClose = !anim.GetBool(m_OpenParameterID);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}

	//Make the provided GameObject selected
	//When using the mouse/touch we actually want to set it as the previously selected and 
	//set nothing as selected for now.
	private void SetSelected(GameObject go) {
		//Select the GameObject.
		EventSystem.current.SetSelectedGameObject(go);

		//If we are using the keyboard right now, that's all we need to do.
		var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
		if (standaloneInputModule != null) //&& standaloneInputModule.inputMode == StandaloneInputModule.InputMode.Buttons)
			return;

		//Since we are using a pointer device, we don't want anything selected. 
		//But if the user switches to the keyboard, we want to start the navigation from the provided game object.
		//So here we set the current Selected to null, so the provided gameObject becomes the Last Selected in the EventSystem.
		EventSystem.current.SetSelectedGameObject(null);
	}
}

