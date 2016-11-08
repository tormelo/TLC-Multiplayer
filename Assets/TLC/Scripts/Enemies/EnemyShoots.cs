using UnityEngine;
using System.Collections;

public class EnemyShoots : MonoBehaviour {
	public Transform[] Mira;
	public GameObject Projetil;

	public void Disparar(int mirar)
	{
		for (int i = 0; i < Mira.Length; i++) 
		{
			if (mirar == 1) 
			{
				Mira[i].LookAt (GameObject.Find ("Player").transform.position);
			}

			Instantiate (Projetil, Mira[i].position, Mira[i].rotation);
		}
	}
}
