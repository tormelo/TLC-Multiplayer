using UnityEngine;
using System.Collections;

public class GhostBehavior : MonoBehaviour {

	public Animator animator; //Deve receber o animator do inimigo que representa
	private Transform PlayerPosition; //Posiçao do jogador a ser seguido
	[Range(1.0f, 10.0f)]
	public int Inteligence;

	private NavMeshAgent agent; //Usado para seguir o player
	private bool ativado;
	private bool alive;

/// <summary>
///Funçoes Gerais
/// </summary>

	//Funçao que atualiza o destino do agent
	void atualizarDestino() {
		//Seta o destino do NavMeshAgent para a posiçao do jogador
		agent.destination = PlayerPosition.position;
	}

	//Funçao que destroi o objeto apos sua morte
	public void morrer(){
		CancelInvoke("atualizarDestino");

		//GameObject.Find ("GameMaster").GetComponent<Master> ().inimigosDerrotados++;

		ativado = false;

		alive = false;

		animator.SetBool ("alive", alive);
		//Destroy (this.gameObject);
	} 

	//Funçao que lida com a perseguiçao do player, resumindo o agent
	void perseguir(){
		agent.Resume();
	}

/// <summary>
/// Funçoes Escecializadas
/// </summary>
				
	void Start () {
		ativado = true;
		alive = true;

		animator.SetBool ("ativo", true);

		PlayerPosition = GameObject.Find ("Player").transform;

		//Referencia ao NavMeshAgent do Inimigo
		agent = GetComponent<NavMeshAgent>();

		//Chama o metodo para atualizar o destino na inicializaçao e depois a cada 1/Inteligencia segundos
		InvokeRepeating("atualizarDestino", 0, 1.0F/Inteligence);
	}

	void FixedUpdate(){
		if (ativado) {
			perseguir ();
		}
	}

	void LateUpdate(){
		if (GameObject.Find ("GameMaster").GetComponent<Master> ().estadoJogador == 1) {
//			ativado = false;
//
//			animator.SetBool ("ativo", false);
//
//			if (alive) 
//			{
//				agent.Stop();
//			}

			GetComponent<EnemyStatus> ().Life = 0;
		}
	}
}
