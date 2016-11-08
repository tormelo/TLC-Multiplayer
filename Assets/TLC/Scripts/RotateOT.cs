using UnityEngine;
using System.Collections;

public class RotateOT : MonoBehaviour {
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3(0, 3, 0));
	}
}
