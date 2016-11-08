using UnityEngine;
using System.Collections;

public class ContabilizarAtaqueOT : MonoBehaviour {

	public int Damage;
	public float Tempo;

	void OnTriggerStay(Collider other) {
		//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
		if (other.gameObject.layer == 12 && transform.root.GetComponent<EnemyStatus>().Attacking == true) {

			other.transform.root.GetComponent<PlayerStatus> ().receberDano (Damage, other.transform.position, true, Tempo); 
			other.transform.root.GetComponent<PlayerStatus> ().mostrarStatus();
			//receberDano(other.transform.parent.gameObject.GetComponent<OctahedronXBehavior>().EnemyStrength);
		}
	}

		//if (other.transform.tag == "ELaser") {
		//	Instantiate (Efeito, other.transform.position, other.transform.rotation);
		//	receberDano(1);
		//
	//}
}
