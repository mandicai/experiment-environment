// These are namespaces that I am using 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Inheriting from 3 different interfaces
// IPointerDownHandler, IDragHandler
public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler  {

	private Vector2 pointerOffset;
	private RectTransform canvasRectTransform;
	private RectTransform panelRectTransform;

	void Awake () {
		// Look all the way up the hierarchy and grab the root parent
		Canvas canvas = GetComponentInParent <Canvas> ();
		// If our canvas is not equal to null, cast the canvas transform as canvasRectTransform 
		// Cast the current position to panelRectTransform 
		if (canvas != null) {
			canvasRectTransform = canvas.transform as RectTransform;
			// Transform.parent allows you to drag the entire panel (the parent) by just clicking on the drag zone 
			panelRectTransform = transform.parent as RectTransform;
		}
	}

	// Invoked when a pointer goes down on a UI element 
	public void OnPointerDown (PointerEventData data) {
		// Becomes the object that is rendered last, which means it is on top, which is what you want !!!
		// Think of a window that comes to the front when you click on it 
		panelRectTransform.SetAsLastSibling ();
		// RectTransformUtility comes with the namespace Unity Engine
		// ScreenPointtoLocalPointInRectangle finds where the pointer came down on the panel 
		// This function is so fucking complicated wtf 
		// data.position is the position of our mouse 
		// data.pressEventCamera is the position of the camera 
		RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
	}

	public void OnDrag (PointerEventData data) {
		if (panelRectTransform == null)
			return;

		// ClampToWindow clamps mouse position 
		Vector2 pointerPosition = ClampToWindow (data);
		Vector2 localPointerPosition;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (canvasRectTransform, pointerPosition, data.pressEventCamera, out localPointerPosition)) {
			panelRectTransform.localPosition = localPointerPosition - pointerOffset;
		}
	}

	// Clamp cursor position to be within the window 
	Vector2 ClampToWindow (PointerEventData data) {
		// Takes in raw position 
		Vector2 rawPointerPosition = data.position;
		// We need to grab the corners of our canvas
		// Create an array of 4 Vector3's
		Vector3 [] canvasCorners = new Vector3[4];
		// GetWorldCorners gets coordinates of all 4 corners and stores it in canvasCorners 
		canvasRectTransform.GetWorldCorners (canvasCorners);
		float clampedX = Mathf.Clamp (rawPointerPosition.x, canvasCorners [0].x, canvasCorners [2].x);
		float clampedY = Mathf.Clamp (rawPointerPosition.y, canvasCorners [0].y, canvasCorners [2].y);

		Vector2 newPointerPosition = new Vector2 (clampedX, clampedY);
		return newPointerPosition;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
