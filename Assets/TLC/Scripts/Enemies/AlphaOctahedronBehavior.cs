using UnityEngine;
using System.Collections;

public class AlphaOctahedronBehavior : MonoBehaviour {

	public Animator animator; //Deve receber o animator do inimigo que representa
	private Transform PlayerPosition; //Posiçao do jogador a ser seguido
	[Range(1.0f, 10.0f)]
	public int Inteligence;
	public float AttackDistance;


	private NavMeshAgent agent; //Usado para seguir o player
	private float distanciaPlayer; //Distancia em XZ entre o inimigo e o player
	private bool attack; //Usada para controlar as animaçoes
	private Vector3 playerX0Z; //Posiçao do player mas com a mesma altura do inimigo
	public bool ativado;
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

	void ativar()
	{
		ativado = true;
	}

	void escolherAtaque()
	{
		Debug.Log("OOO");
		int i = Random.Range(0, 100);

		if (i >= 0 && i < 60) 
		{
			animator.SetInteger ("numero", 0);
			agent.Resume ();

		} 
		else if (i >= 60 && i <= 100) 
		{
			animator.SetInteger ("numero", 1);
			agent.Stop ();
		} 
	}

	//Funçao que lida com o ataque do inimigo, fazendo com que ele pare o agent
	void atacar(){
		attack = true;

		//Seta o parametro attack do Animator para o valor da bool attack
		animator.SetBool ("attack", attack);

		if (!transform.root.GetComponent<EnemyStatus> ().Attacking) 
		{
			escolherAtaque ();
		}
	}

	//Funçao que lida com a perseguiçao do player, resumindo o agent
	void perseguir(){
		attack = false;

		//Seta o parametro attack do Animator para o valor da bool attack
		animator.SetBool ("attack", attack);

		if (!transform.root.GetComponent<EnemyStatus> ().Attacking) 
		{
			agent.Resume ();
		}
	}

/// <summary>
/// Funçoes Escecializadas
/// </summary>

	//Comportamento
	void comportamento(){
		//Ciclo da vida do inimigo
		if(distanciaPlayer < AttackDistance)
		{
			atacar();
		}
		else if(distanciaPlayer > AttackDistance && GetComponent<EnemyStatus>().Attacking == false){
			perseguir();
		}
	}
				
	void Start () {
		ativado = false;
		Invoke ("ativar", 2);
		alive = true;

		PlayerPosition = GameObject.Find ("Player").transform;

		//Referencia ao NavMeshAgent do Inimigo
		agent = GetComponent<NavMeshAgent>();
		agent.Stop ();

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


			attack = false;
			animator.SetBool ("attack", attack);

			if (alive) 
			{
				agent.Stop();
			}
		}

		if (alive) 
		{
			playerX0Z = new Vector3 (PlayerPosition.position.x, transform.position.y, PlayerPosition.position.z);
			//transform.LookAt (playerX0Z);
		}
	}
}
