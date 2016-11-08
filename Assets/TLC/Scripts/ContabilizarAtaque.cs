using UnityEngine;
using System.Collections;

public class ContabilizarAtaque : MonoBehaviour {

	public int Damage;

	void OnTriggerEnter(Collider other) {
		//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
		if (other.gameObject.layer == 12 && transform.root.GetComponent<EnemyStatus>().Attacking == true) {
			//Sinaliza que o ataque já foi contabilizado.
			transform.root.GetComponent<EnemyStatus>().Attacking = false;

			other.transform.root.GetComponent<PlayerStatus> ().receberDano (Damage, other.transform.position, false, 0); 
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
