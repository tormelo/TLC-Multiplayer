using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum StageNumber{
	ONE = 0,
	TWO = 1,
	THREE = 2,
	FOUR = 3,
	FIVE = 4,
	SIX = 5,
	SEVEN = 6,
	EIGHT = 7,
}

public enum StageType{
	DEFAULT,
	BOSS
}

public class Master : MonoBehaviour {

	public StageNumber Stage;
	public StageType Type;
	public float BestScore;
	public Animator Canvas;
	public GameObject[] Inimigos;
	public GameObject[] Spawners;
	public Text Polygon_Text;
	public int estadoJogador;
	public int inimigosDerrotados;
	private int numInimigos;
	private bool inicializado;

	public int Polygons;
	public Text plusPolys;
	public Text Score_Text;
	public GameObject[] Stars = new GameObject[3];

	private Transform player;

	//Wave config
	private int waveNumber;
	private bool waveInitialized;

	//Stage end
	private int stageState; // -1 = inicial, 0 = perdeu, 1 = venceu
	private bool endInitialized;

	//Pause
	public Text TipoMira;
	public Text ComportamentoHealth;
	public GameObject Health;
 	
	//Waves
	public Text WaveLeft;
	public Text WaveRight;

	public IEnumerator wave1()
	{
		if ((int)Stage == 0) 
		{
			numInimigos = 8;
		}
		if ((int)Stage == 1) 
		{
			numInimigos = 8;
		}
		if ((int)Stage == 3) 
		{
			numInimigos = 12;
		}
		if ((int)Stage == 4) 
		{
			numInimigos = 10;
		}
		if ((int)Stage == 6) 
		{
			numInimigos = 10;
		}
		waveInitialized = true;

		Canvas.SetTrigger("contagem");
		yield return new WaitForSeconds (4.5F);

		inicializado = true;

		yield return new WaitForSeconds (2);

		WaveLeft.fontSize = 240;
		WaveRight.fontSize = 240;
		WaveLeft.text = "WAVE 1";
		WaveRight.text = "WAVE 1";
		Canvas.SetTrigger ("vinheta");

		yield return new WaitForSeconds (3);

		if ((int)Stage == 0) 
		{
			spawnarInimigo (Inimigos [0], 8);
		}
		if ((int)Stage == 1) 
		{
			spawnarInimigo (Inimigos [0], 3);
			spawnarInimigo (Inimigos [1], 3);
			spawnarInimigo (Inimigos[2], 2);
		}
		if ((int)Stage == 3) 
		{
			spawnarInimigo (Inimigos [3], 8);
			spawnarInimigo (Inimigos[11], 4);
		}
		if ((int)Stage == 4) 
		{
			spawnarInimigo (Inimigos [3], 4);
			spawnarInimigo (Inimigos[9], 6);
		}
		if ((int)Stage == 6) 
		{
			Instantiate (Inimigos [10], Spawners [0].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [1].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [2].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [3].transform.position, Quaternion.identity);
			spawnarInimigo (Inimigos[11], 6);
		}
	}

	public IEnumerator wave2()
	{
		if ((int)Stage == 0) 
		{
			numInimigos = 8;
		}
		if ((int)Stage == 1) 
		{
			numInimigos = 10;
		}
		if ((int)Stage == 3) 
		{
			numInimigos = 10;
		}
		if ((int)Stage == 4) 
		{
			numInimigos = 12;
		}
		if ((int)Stage == 6) 
		{
			numInimigos = 10;
		}
		waveInitialized = true;

		yield return new WaitForSeconds (2);

		WaveLeft.fontSize = 240;
		WaveRight.fontSize = 240;
		WaveLeft.text = "WAVE 2";
		WaveRight.text = "WAVE 2";
		Canvas.SetTrigger ("vinheta");

		yield return new WaitForSeconds (3);

		if ((int)Stage == 0) 
		{
			spawnarInimigo (Inimigos[1], 8);
		}
		if ((int)Stage == 1) 
		{
			spawnarInimigo (Inimigos [0], 2);
			spawnarInimigo (Inimigos [1], 2);
			spawnarInimigo (Inimigos[2], 4);
			spawnarInimigo (Inimigos[7], 2);
		}
		if ((int)Stage == 3) 
		{
			spawnarInimigo (Inimigos [3], 6);
			spawnarInimigo (Inimigos[8], 2);
			spawnarInimigo (Inimigos[11], 2);
		}
		if ((int)Stage == 4) 
		{
			spawnarInimigo (Inimigos [3], 4);
			spawnarInimigo (Inimigos[5], 4);
			spawnarInimigo (Inimigos[9], 4);
		}
		if ((int)Stage == 6) 
		{
			Instantiate (Inimigos [10], Spawners [0].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [1].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [2].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [3].transform.position, Quaternion.identity);
			spawnarInimigo (Inimigos [9], 6);
		}
	}

	public IEnumerator wave3()
	{
		if ((int)Stage == 0) 
		{
			numInimigos = 10;
		}
		if ((int)Stage == 1) 
		{
			numInimigos = 8;
		}
		if ((int)Stage == 3) 
		{
			numInimigos = 12;
		}
		if ((int)Stage == 4) 
		{
			numInimigos = 14;
		}
		if ((int)Stage == 6) 
		{
			numInimigos = 8;
		}
		waveInitialized = true;

		yield return new WaitForSeconds (2);

		WaveLeft.fontSize = 240;
		WaveRight.fontSize = 240;
		WaveLeft.text = "WAVE 3";
		WaveRight.text = "WAVE 3";
		Canvas.SetTrigger ("vinheta");

		yield return new WaitForSeconds (3);

		if ((int)Stage == 0) 
		{
			spawnarInimigo (Inimigos [0], 5);
			spawnarInimigo (Inimigos[1], 5);
		}
		if ((int)Stage == 1) 
		{
			spawnarInimigo (Inimigos[2], 6);
			spawnarInimigo (Inimigos[7], 1);
			spawnarInimigo (Inimigos[8], 1);
		}
		if ((int)Stage == 3) 
		{
			spawnarInimigo (Inimigos [3], 6);
			spawnarInimigo (Inimigos[4], 2);
			spawnarInimigo (Inimigos[11], 4);
		}
		if ((int)Stage == 4) 
		{
			spawnarInimigo (Inimigos [3], 4);
			spawnarInimigo (Inimigos[5], 3);
			spawnarInimigo (Inimigos[6], 1);
			spawnarInimigo (Inimigos[9], 6);
		}
		if ((int)Stage == 6) 
		{
			Instantiate (Inimigos [10], Spawners [0].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [1].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [2].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [3].transform.position, Quaternion.identity);
			spawnarInimigo (Inimigos [8], 1);
			spawnarInimigo (Inimigos [7], 1);
			spawnarInimigo (Inimigos [11], 2);
		}
	}

	public IEnumerator wave4()
	{
		if ((int)Stage == 0) 
		{
			numInimigos = 14;
		}
		if ((int)Stage == 1) 
		{
			numInimigos = 10;
		}
		if ((int)Stage == 3) 
		{
			numInimigos = 12;
		}
		if ((int)Stage == 4) 
		{
			numInimigos = 14;
		}
		if ((int)Stage == 6) 
		{
			numInimigos = 10;
		}
		waveInitialized = true;

		yield return new WaitForSeconds (2);

		WaveLeft.fontSize = 240;
		WaveRight.fontSize = 240;
		WaveLeft.text = "WAVE 4";
		WaveRight.text = "WAVE 4";
		Canvas.SetTrigger ("vinheta");

		yield return new WaitForSeconds (3);

		if ((int)Stage == 0) 
		{
			spawnarInimigo (Inimigos [0], 7);
			spawnarInimigo (Inimigos[1], 7);
		}
		if ((int)Stage == 1) 
		{
			spawnarInimigo (Inimigos[2], 8);
			spawnarInimigo (Inimigos[8], 2);
		}
		if ((int)Stage == 3) 
		{
			spawnarInimigo (Inimigos [3], 4);
			spawnarInimigo (Inimigos[4], 2);
			spawnarInimigo (Inimigos[8], 2);
			spawnarInimigo (Inimigos[11], 4);
		}
		if ((int)Stage == 4) 
		{
			spawnarInimigo (Inimigos [3], 2);
			spawnarInimigo (Inimigos[5], 6);
			spawnarInimigo (Inimigos[6], 2);
			spawnarInimigo (Inimigos[9], 4);
		}
		if ((int)Stage == 6) 
		{
			Instantiate (Inimigos [10], Spawners [0].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [1].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [2].transform.position, Quaternion.identity);
			Instantiate (Inimigos [10], Spawners [3].transform.position, Quaternion.identity);
			spawnarInimigo (Inimigos [8], 1);
			spawnarInimigo (Inimigos [7], 1);
			spawnarInimigo (Inimigos [9], 4);
		}
	}

	public IEnumerator wave5()
	{
		if ((int)Stage == 0) 
		{
			numInimigos = 15;
		}
		if ((int)Stage == 1) 
		{
			numInimigos = 10;
		}
		if ((int)Stage == 3) 
		{
			numInimigos = 14;
		}
		if ((int)Stage == 4) 
		{
			numInimigos = 14;
		}
		if ((int)Stage == 6) 
		{
			numInimigos = 10;
		}
		waveInitialized = true;

		yield return new WaitForSeconds (2);

		WaveLeft.fontSize = 150;
		WaveRight.fontSize = 150;
		WaveLeft.text = "FINAL WAVE";
		WaveRight.text = "FINAL WAVE";
		Canvas.SetTrigger ("vinheta");
	
		yield return new WaitForSeconds (3);

		if ((int)Stage == 0) 
		{
			spawnarInimigo (Inimigos [0], 8);
			spawnarInimigo (Inimigos[1], 5);
			spawnarInimigo (Inimigos[7], 2);
		}
		if ((int)Stage == 1) 
		{
			spawnarInimigo (Inimigos[2], 6);
			spawnarInimigo (Inimigos[7], 1);
			spawnarInimigo (Inimigos[8], 3);
		}
		if ((int)Stage == 3) 
		{
			spawnarInimigo (Inimigos [3], 4);
			spawnarInimigo (Inimigos[4], 4);
			spawnarInimigo (Inimigos[11], 6);
		}
		if ((int)Stage == 4) 
		{
			spawnarInimigo (Inimigos[5], 2);
			spawnarInimigo (Inimigos[6], 4);
			spawnarInimigo (Inimigos[9], 8);
		}
		if ((int)Stage == 6) 
		{
			spawnarInimigo (Inimigos [8], 1);
			spawnarInimigo (Inimigos [7], 1);
			spawnarInimigo (Inimigos [9], 6);
			spawnarInimigo (Inimigos [11], 2);
		}
	}

	public void Pausar()
	{
		if (inicializado && stageState == -1) 
		{
			Time.timeScale = 0;
			Canvas.SetTrigger ("pausar");
		}

		Debug.Log("Ué");
	}

	public void Resumir()
	{
		if (inicializado) 
		{
			Time.timeScale = 1;
			Canvas.SetTrigger ("resumir");
		}
	}

	public void Reiniciar (string stage){
		//Application.LoadLevel (Application.loawadedLevel);
		SceneManager.LoadScene(stage);
	}

	void calcularResultado()
	{
		SaveSystem.current.polys += Polygons;

		plusPolys.text = Polygons.ToString ();

		int score = Polygons * 1000;

		if (score > SaveSystem.current.score [(int)Stage]) {
			SaveSystem.current.score [(int)Stage] = score;
			Score_Text.text = "NEW HI-SCORE " + score.ToString ();
		} 
		else 
		{
			Score_Text.text = "SCORE " + score.ToString ();
		}

		float porcentagem = score/BestScore;
		int estrelas;

		if (porcentagem < 0.5f) 
		{
			estrelas = 1;
		} 
		else if (porcentagem >= 0.5f && porcentagem < 1) 
		{
			estrelas = 2;
		}
		else 
		{
			estrelas = 3;
		}

		if (estrelas > SaveSystem.current.stars [(int)Stage]) 
		{
			SaveSystem.current.stars [(int)Stage] = estrelas;
		}

		//Configuração estrelas
		for (int i = 0; i < 3; i++) 
		{
			if (estrelas-1 == i) 
			{
				Stars [i].SetActive (true);
			} 
			else 
			{
				Stars [i].SetActive (false);
			}
		}
	}

	void unlockNextStage()
	{
		if (!SaveSystem.current.stages [(int)Stage + 1]) 
		{
			SaveSystem.current.stages [(int)Stage + 1] = true;
		}
	}

	void falhou()
	{
		Canvas.SetTrigger ("failed");
	}

	void venceu()
	{
		calcularResultado ();

		Canvas.SetTrigger ("won");

		SaveSystem.Save ();
	}

	public void mudarMira()
	{
		if (SaveSystem.current.miraAutomatica) 
		{
			SaveSystem.current.miraAutomatica = false;
			TipoMira.text = "MANUAL";
		}
		else
		{
			SaveSystem.current.miraAutomatica = true;
			TipoMira.text = "AUTO";
		}
	}

	public void mudarHealthSystem()
	{
		if (SaveSystem.current.esconderLife) 
		{
			SaveSystem.current.esconderLife = false;
			Health.SetActive (true);
			ComportamentoHealth.text = "ALWAYS ON";

		} 
		else 
		{
			SaveSystem.current.esconderLife = true;
			Health.SetActive (false);
			ComportamentoHealth.text = "AUTO";
		}
	}


	void spawnarInimigo(GameObject inimigo, int quantidade)
	{
		for (int i = 0; i < quantidade; i++) 
		{
			int n = 0;
			float distancia;
			Vector2 a = new Vector2 (player.position.x, player.position.z);
			Vector2 b;

			//Evita que os inimigos spawnem muito proximos do jogador
			do {
				n = Random.Range (0, Spawners.Length);
				b = new Vector2 (Spawners [n].transform.position.x, Spawners [n].transform.position.z);
				distancia = Vector2.Distance (a, b);

			} while(distancia < 20);

			Instantiate (inimigo, Spawners [n].transform.position, Quaternion.identity);
		}
	}

	void Awake()
	{
		//Carreaga o save do jogo
		//SaveSystem.Load ();
	}

	void Start () {
		if (SaveSystem.current.miraAutomatica) 
		{
			TipoMira.text = "AUTO";
		}
		else 
		{
			TipoMira.text = "MANUAL";
		}

		if (SaveSystem.current.esconderLife) 
		{
			ComportamentoHealth.text = "AUTO";		
		}
		else 
		{
			ComportamentoHealth.text = "ALWAYS ON";		
		}
			
		Time.timeScale = 1;

		Polygons = 0;
		player = GameObject.Find ("Player").transform;

		estadoJogador = 0;
		inimigosDerrotados = 0;
		inicializado = false;

		waveNumber = 0;
		waveInitialized = false;

		stageState = -1;
		endInitialized = false;

		//SaveSystem.saved = false;
	}

	void Update () {
		if (Polygon_Text.text != Polygons.ToString()) 
		{
			Polygon_Text.text = Polygons.ToString ();
		}
			
		if (estadoJogador == 1 && !endInitialized) 
		{
			stageState = 0;
			endInitialized = true;

			Invoke ("falhou", 1.5f);
		}

		if (Type == StageType.DEFAULT) 
		{
			if (waveNumber == 0 && !waveInitialized) 
			{
				StartCoroutine ("wave1");
			}
			if (waveNumber == 1 && !waveInitialized) 
			{
				StartCoroutine ("wave2");
			}
			if (waveNumber == 2 && !waveInitialized) 
			{
				StartCoroutine ("wave3");
			}
			if (waveNumber == 3 && !waveInitialized) 
			{
				StartCoroutine ("wave4");
			}
			if (waveNumber == 4 && !waveInitialized) 
			{
				StartCoroutine ("wave5");
			}

			if (waveNumber < 4 && waveInitialized && inimigosDerrotados == numInimigos) 
			{
				inimigosDerrotados = 0;
				waveNumber++;
				waveInitialized = false;
			}

			if (waveNumber == 4 && waveInitialized && inimigosDerrotados == numInimigos && !endInitialized) 
			{
				stageState = 1;
				endInitialized = true;

				Invoke ("venceu", 5f);

				if ((int)Stage < 7) 
				{
					unlockNextStage ();
				}
			}
		}

		if (Type == StageType.BOSS) 
		{
			if (!inicializado)
			{
				numInimigos = 1;
				inicializado = true;
			}

			if (inimigosDerrotados == numInimigos) 
			{
				stageState = 1;
			}

			if (!endInitialized) 
			{
				if (stageState == 1) 
				{
					endInitialized = true;

					Invoke ("venceu", 5f);

					if ((int)Stage < 7) 
					{
						unlockNextStage ();
					}
				} 
			}

		}
	}
}
