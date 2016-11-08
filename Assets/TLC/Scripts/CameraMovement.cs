using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private int screenshotCount = 0;
	private GameObject Player;

	private Vector3 offset;

	void Start () {
		Player = GameObject.Find ("Player");

		offset = transform.position - Player.transform.position;
		Cursor.visible = false;
	}

	void Update () {
		transform.position = Player.transform.position + offset;
		// take screenshot on up->down transition of F9 key
		if (Input.GetKeyDown("f9"))
		{        
			string screenshotFilename;
			do
			{
				screenshotCount++;
				screenshotFilename = "screenshot" + screenshotCount + ".png";

			} while (System.IO.File.Exists(screenshotFilename));

			Application.CaptureScreenshot(screenshotFilename);
		}
	}
}
