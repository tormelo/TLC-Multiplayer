using UnityEngine;
using System.Collections;

public class IcoBehavior : MonoBehaviour {

	public Animator animator; //Deve receber o animator do inimigo que representa
	private Transform PlayerPosition; //Posiçao do jogador a ser seguido
	[Range(1.0f, 5.0f)]
	public int Inteligence;
	//public int EnemyStrength;
	public float AttackDistance;


	private NavMeshAgent agent; //Usado para seguir o player
	private float distanciaPlayer; //Distancia em XZ entre o inimigo e o player
	private Vector3 playerX0Z; //Posiçao do player mas com a mesma altura do inimigo
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
		
	//Funçao que para o agent
	void parar(){
		transform.LookAt (playerX0Z);

		agent.Stop();
	}

	//Funçao que lida com a perseguiçao do player, resumindo o agent
	void perseguir(){
		agent.Resume();
	}

/// <summary>
/// Funçoes Escecializadas
/// </summary>

	//Comportamento
	void comportamento(){
		//Ciclo da vida do inimigo
		if(distanciaPlayer < AttackDistance)
		{
			parar();
		}
		else{
			perseguir();
		}
	}
				
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
	
		//Calcula a distancia do monstro ate o player
		if (ativado) {
			distanciaPlayer = Vector3.Distance (transform.position, playerX0Z);

			//Comportamento
			comportamento ();
		}
	}

	void LateUpdate(){
		//Faz o monstro sempre olhar na direçao do player
		if (GameObject.Find ("GameMaster").GetComponent<Master> ().estadoJogador == 1) {
			ativado = false;

			animator.SetBool ("ativo", false);

			if (alive) 
			{
				agent.Stop();
			}
		}

		if (alive) 
		{
			playerX0Z = new Vector3 (PlayerPosition.position.x, transform.position.y, PlayerPosition.position.z);
		}
	}
}
