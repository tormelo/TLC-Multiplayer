using UnityEngine;
using System.Collections;

public class ContabilizarAtaqueJogador : MonoBehaviour {

	public int Damage;

	void OnTriggerEnter(Collider other) {
		//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
		if (other.gameObject.layer == 10) {
			//Sinaliza que o ataque já foi contabilizado.

			other.transform.root.GetComponent<EnemyStatus> ().receberDano (Damage, transform.position, false, 0);
			//receberDano(other.transform.parent.gameObject.GetComponent<OctahedronXBehavior>().EnemyStrength);
		}
	}

		//if (other.transform.tag == "ELaser") {
		//	Instantiate (Efeito, other.transform.position, other.transform.rotation);
		//	receberDano(1);
		//
	//}
}
