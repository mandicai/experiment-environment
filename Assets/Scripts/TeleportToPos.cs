using UnityEngine;
using System.Collections;

public class TeleportToPos : MonoBehaviour {

	float tempY;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {

			tempY = transform.position.y;

			transform.position = new Vector3 (0, tempY, 0);
		}
	}
}