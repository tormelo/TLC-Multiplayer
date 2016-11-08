using UnityEngine;
using System.Collections;

public class ContabilizarAtaqueGhost : MonoBehaviour {

	public int Damage;
	public float Tempo;

	void possibilitarNovoAtaque()
	{
		transform.root.GetComponent<EnemyStatus>().Attacking = true;
	}

	void OnTriggerStay(Collider other) 
	{
		//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
		if (other.gameObject.layer == 12 && transform.root.GetComponent<EnemyStatus>().Attacking == true) {
			//Sinaliza que o ataque já foi contabilizado.
			transform.root.GetComponent<EnemyStatus>().Attacking = false;

			other.transform.root.GetComponent<PlayerStatus> ().receberDano (Damage, other.transform.position, false, 0); 
			other.transform.root.GetComponent<PlayerStatus> ().mostrarStatus();

			Invoke ("possibilitarNovoAtaque", Tempo);
		}
	}

	void Start()
	{
		transform.root.GetComponent<EnemyStatus>().Attacking = true;
	}
}
