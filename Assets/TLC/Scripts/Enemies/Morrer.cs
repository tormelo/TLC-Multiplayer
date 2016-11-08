using UnityEngine;
using System.Collections;

public class Morrer : MonoBehaviour {

	public GameObject Polygon;
	public float VelocidadeDeDescida;
	public float Profundidade;
	public float Contagem;

	private GameObject g;
	public bool isDead;

	public void inicializarDescida()
	{
		Invoke ("confirmarMorte", Contagem);

		GameObject.Find ("GameMaster").GetComponent<Master> ().inimigosDerrotados++;
	}

	public void spawnarPolygon()
	{
		Instantiate (Polygon, new Vector3(transform.position.x, 2, transform.position.z), transform.root.transform.rotation);
	}

	void confirmarMorte()
	{
		isDead = true;
	}

	void Start () {
		isDead = false;
		g = transform.root.gameObject;
	}

	void Update () {
		if (isDead) 
		{
			g.transform.Translate (0, -VelocidadeDeDescida/100, 0);
		}

		if (g.transform.position.y < -Profundidade) 
		{
			Destroy (g);
		}
	}
}
