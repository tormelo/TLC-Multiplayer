using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

	public float minPitch;
	public float maxPitch;

	private AudioSource som;

	public void tocarSom(){
		som.pitch = Random.Range (minPitch, maxPitch);
		som.Play ();
	}

	void Start () {
		som = GetComponent<AudioSource> ();
	}
}
