using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour {

	public void open (GameObject element) {
		element.SetActive (true);
	}
	public void close (GameObject element) {
		element.SetActive (false);
	}
}
