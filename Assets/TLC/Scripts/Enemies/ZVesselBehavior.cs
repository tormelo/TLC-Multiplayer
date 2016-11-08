using UnityEngine;
using System.Collections;

public class ZVesselBehavior : MonoBehaviour {

	public Animator animator; //Deve receber o animator do inimigo que representa
	private Transform PlayerPosition; //Posiçao do jogador a ser seguido
	public Transform VetorDirecao;
	public float AttackDistance;

	private float distanciaPlayer; //Distancia em XZ entre o inimigo e o player
	private Vector2 playerXZ; //Posiçao do player mas com a mesma altura do inimigo
	private Vector2 enemyXZ; //Posiçao do player mas com a mesma altura do inimigo
	private Vector2 vetorComp;
	private Vector2 directionPlayer;
	private float directionAngle;
	private bool ativado;
	private bool alive;
	private bool morreu;

	private int attackNum;
	private int porcentagem;

/// <summary>
///Funçoes Gerais
/// </summary>

	void atualizarAngulo()
	{
		playerXZ = new Vector2 (PlayerPosition.position.x, PlayerPosition.position.z);

		enemyXZ = new Vector2 (transform.position.x, transform.position.z);

		directionPlayer = playerXZ - enemyXZ;

		directionPlayer.Normalize ();

		directionAngle = Vector2.Angle(directionPlayer, vetorComp);

		Debug.Log (directionAngle);
	}

	void escolherAtaque()
	{
		if (directionAngle >= 70 && directionAngle <= 110) 
		{
			if (distanciaPlayer > AttackDistance) 
			{
				porcentagem = Random.Range (0, 100);

				if (porcentagem < 70) 
				{
					attackNum = 0;
				} 
				else 
				{
					attackNum = 1;
				}
			} 
			else 
			{
				attackNum = 4;
			}
		}

		if (directionAngle > 0 && directionAngle < 70) 
		{
			if (distanciaPlayer > AttackDistance) 
			{
				attackNum = 2;
			} 
			else 
			{
				attackNum = 1;
			}
		}

		if (directionAngle > 110 && directionAngle <= 180) 
		{
			if (distanciaPlayer > AttackDistance) 
			{
				attackNum = 3;
			} 
			else 
			{
				attackNum = 1;
			}
		}

		Debug.Log (distanciaPlayer);

		animator.SetInteger ("indice", attackNum);
	}

	//Funçao que destroi o objeto apos sua morte
	public void morrer(){
		//GameObject.Find ("GameMaster").GetComponent<Master> ().inimigosDerrotados++;

		if (!morreu) 
		{
			GameObject.Find ("GameMaster").GetComponent<Master> ().inimigosDerrotados++;
			morreu = true;
		}
	
		ativado = false;

		alive = false;

		animator.SetBool ("alive", alive);
		//Destroy (this.gameObject);
	} 

/// <summary>
/// Funçoes Escecializadas
/// </summary>
				
	void Start () {
		vetorComp = new Vector2(VetorDirecao.position.x, VetorDirecao.position.z) - new Vector2 (transform.position.x, transform.position.z);
		vetorComp.Normalize ();

		morreu = false;
		ativado = true;
		alive = true;

		PlayerPosition = GameObject.Find ("Player").transform;
	}

	void FixedUpdate(){
	
		//Calcula a distancia do monstro ate o player
		if (ativado) {
			distanciaPlayer = Vector2.Distance (enemyXZ, playerXZ);


			if (!transform.root.GetComponent<EnemyStatus> ().Attacking) 
			{
				escolherAtaque ();
			}
		}
	}

	void LateUpdate(){
		//Faz o monstro sempre olhar na direçao do player
		if (GameObject.Find ("GameMaster").GetComponent<Master> ().estadoJogador == 1) {
			ativado = false;

			attackNum = -1;
			animator.SetInteger ("indice", attackNum);
		}

		if (alive) 
		{
			atualizarAngulo ();
			//transform.LookAt (playerX0Z);
		}
	}
}
