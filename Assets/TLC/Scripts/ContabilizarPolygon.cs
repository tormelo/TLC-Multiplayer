using UnityEngine;
using System.Collections;

public class ContabilizarPolygon : MonoBehaviour {

	public int Valor;

	void OnTriggerEnter(Collider other) {
		//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
		if (other.gameObject.layer == 12) 
		{
			GameObject.Find ("GameMaster").GetComponent<Master> ().Polygons += Valor;
			GameObject.Find ("Player").GetComponent<PlayerStatus> ().somMoeda ();
			Destroy (transform.root.gameObject);
		}

		if (other.gameObject.layer == 11 && SaveSystem.current.selectedPerk != 0) 
		{
			transform.root.GetComponent<Rigidbody> ().isKinematic = true;
			transform.root.GetComponent<Collider> ().isTrigger = true;
		}
	}
}
