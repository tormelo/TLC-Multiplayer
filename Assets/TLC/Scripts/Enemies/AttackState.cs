using UnityEngine;
using System.Collections;

public class AttackState : MonoBehaviour {

	//Sinaliza que um ataque está acontecendo
	void inicializarAtaque(){
		transform.root.GetComponent<EnemyStatus>().Attacking = true;
	}

	//Sinaliza que o ataque chegou ao fim da animação ou já contabilizou dano
	void terminarAtaque(){
		transform.root.GetComponent<EnemyStatus> ().Attacking = false;
	}
}
