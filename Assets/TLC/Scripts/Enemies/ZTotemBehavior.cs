using UnityEngine;
using System.Collections;

public class ZTotemBehavior : MonoBehaviour {

	public Animator animator; //Deve receber o animator do inimigo que representa
	public Transform Body;
	private Transform PlayerPosition; //Posiçao do jogador a ser seguido
	public float DistanciaDisparo;

	private float distanciaPlayer; //Distancia em XZ entre o inimigo e o player
	private bool attack; //Usada para controlar as animaçoes
	private Vector3 playerX0Z; //Posiçao do player mas com a mesma altura do inimigo
	private bool ativado;
	private bool alive;

/// <summary>
///Funçoes Gerais
/// </summary>

	//Funçao que destroi o objeto apos sua morte
	public void morrer(){
		CancelInvoke("atualizarDestino");

		//GameObject.Find ("GameMaster").GetComponent<Master> ().inimigosDerrotados++;

		ativado = false;

		alive = false;

		animator.SetBool ("alive", alive);
		//Destroy (this.gameObject);
	} 
		
	//Funçao que lida com o ataque do inimigo, fazendo com que ele pare o agent
	void atacar(){
		attack = true;
	}

	//Funçao que lida com a perseguiçao do player, resumindo o agent
	void parar(){
		attack = false;
	}

/// <summary>
/// Funçoes Escecializadas
/// </summary>

	//Comportamento
	void comportamentoSniper(){
		//Ciclo da vida do inimigo
		if(distanciaPlayer < DistanciaDisparo)
		{
			atacar();
		}
		else
		{
			parar();
		}
	}

	void Start () {
		ativado = true;
		alive = true;

		PlayerPosition = GameObject.Find ("Player").transform;
	}

	void FixedUpdate(){
	
		//Calcula a distancia do monstro ate o player
		if (ativado) {
			distanciaPlayer = Vector3.Distance (transform.position, playerX0Z);

			comportamentoSniper ();

			//Seta o parametro attack do Animator para o valor da bool attack
			animator.SetBool ("attack", attack);
		}
	}

	void LateUpdate(){
		//Faz o monstro sempre olhar na direçao do player
		if (GameObject.Find ("GameMaster").GetComponent<Master> ().estadoJogador == 1) {
			ativado = false;
			attack = false;
			animator.SetBool ("attack", attack);
		}
		if(alive){
			playerX0Z = new Vector3 (PlayerPosition.position.x, Body.position.y, PlayerPosition.position.z);
			Body.LookAt (playerX0Z);
		}
	}
}
