using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

	public float Time;

	void DestruirObjeto (){
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		Invoke ("DestruirObjeto", Time);
	}
}
