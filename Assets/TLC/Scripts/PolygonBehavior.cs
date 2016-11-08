using UnityEngine;
using System.Collections;

public class PolygonBehavior : MonoBehaviour {

	public int Tempo;
	private Rigidbody rb;
	private Transform player;


	void destruir()
	{
		Destroy (gameObject);
	}

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddRelativeForce(new Vector3(0, 0.5f, 1f) * (Random.Range(300, 500)));
		player = GameObject.Find ("Player").transform;

		Invoke ("destruir", Tempo);
	}

	void Update()
	{
		if (SaveSystem.current.selectedPerk == 0) 
		{
			transform.LookAt (player.position);

			rb.AddRelativeForce(Vector3.forward*20);
		}
	}
}
