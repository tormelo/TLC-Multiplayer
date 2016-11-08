using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
	public Transform Mira;
	public GameObject Projetil;

	public void Disparar(int mirar)
	{
		if (mirar == 1) 
		{
			Mira.LookAt (GameObject.Find ("Player").transform.position);
		}
		Instantiate (Projetil, Mira.position, Mira.rotation);
	}
}
