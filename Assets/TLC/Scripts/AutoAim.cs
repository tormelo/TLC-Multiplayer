using UnityEngine;
using System.Collections;

public class AutoAim : MonoBehaviour {

	private Vector3[] posInimigos = new Vector3[30];
	private int numInimigos;
	private int layerMask;
	public Vector3 alvo;

	IEnumerator OnTriggerStay(Collider other) 
	{
		//Se other for o colisor do inimigo o coloca na array
		if (other.gameObject.layer == 10 && other.transform.root.GetComponent<EnemyStatus>().Life > 0) 
		{
			RaycastHit hit;
			Vector3 rayDirection = other.transform.position - transform.position; //Subtração de vetores para encontrar a direção do raio
			if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity, layerMask))
			{
				Debug.DrawLine(transform.position, hit.point);
				Debug.Log (hit.transform.gameObject.layer);

				if (hit.transform.gameObject.layer == 10) 
				{
					numInimigos++;
					posInimigos [numInimigos-1] = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
				}
			}
		}

		yield return new WaitForFixedUpdate();
	}

	void escolherAlvo()
	{
		if (numInimigos > 0) 
		{
			int a = 0;
					
			for(int i = 0; i < numInimigos; i++)
			{
				if (i == 0) 
				{
					a = 0;
				} 
				else 
				{
					if (Vector3.Distance (transform.position, posInimigos [i]) < Vector3.Distance (transform.position, posInimigos [a])) 
					{
						a = i;
					}
				}
			}

			alvo = posInimigos [a];
		}
		else
		{
			alvo = new Vector3 (1000, 1000, 1000);
		}

		Debug.Log (alvo);
	}

	void Start () 
	{
		posInimigos = null;

		numInimigos = 0;

		layerMask = 1 << 8 | 1 << 10;

		alvo = new Vector3 (1000, 1000, 1000);
	}

	void FixedUpdate () 
	{
//		if (trigger.enabled) 
//		{
//			trigger.enabled = false;
//		} 
//		else 
//		{
//			trigger.enabled = true;
//		}

		escolherAlvo ();
		posInimigos = new Vector3[30];
		numInimigos = 0;
	}

//	void LateUpdate()
//	{
//		escolherAlvo ();
//		posInimigos = new Vector3[30];
//		numInimigos = 0;
//	}
}
