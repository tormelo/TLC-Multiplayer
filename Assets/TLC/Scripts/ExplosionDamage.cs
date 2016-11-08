using UnityEngine;
using System.Collections;

public class ExplosionDamage : MonoBehaviour {

	private int Damage;
	public Transform ExplosionCenter;
	private int layerMask;

	void Start()
	{
		layerMask = 1 << 8 | 1 << 10 | 1 << 12;

		Damage = transform.root.GetComponent<ExplosiveBehavior> ().Damage;
	}

	void OnTriggerEnter(Collider other) {
		if (transform.root.GetComponent<ExplosiveBehavior> ().Type == 0) 
		{
			//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
			if (other.gameObject.layer == 10) 
			{
				RaycastHit hit;
				Vector3 rayDirection = other.transform.position - ExplosionCenter.position; //Subtração de vetores para encontrar a direção do raio
				if (Physics.Raycast(ExplosionCenter.position, rayDirection, out hit, Mathf.Infinity, layerMask))
				{
					Debug.DrawLine(ExplosionCenter.position, hit.point);
					Debug.Log (hit.collider.gameObject.layer);

					if (hit.collider.gameObject.layer == 10) 
					{
						other.transform.root.GetComponent<EnemyStatus> ().receberDano (Damage, hit.point, false, 0);
					}
				}
			}
		} 
		else if (transform.root.GetComponent<ExplosiveBehavior> ().Type == 1) 
		{
			//Se other for o colisor do player e um ataque foi inicializado contabiliza o dano
			if (other.gameObject.layer == 12) 
			{
				RaycastHit hit;
				Vector3 rayDirection = other.transform.position - ExplosionCenter.position; //Subtração de vetores para encontrar a direção do raio
				if (Physics.Raycast(ExplosionCenter.position, rayDirection, out hit, Mathf.Infinity, layerMask))
				{
					Debug.DrawLine(ExplosionCenter.position, hit.point);

					Debug.Log (hit.collider.gameObject.layer);

					if (hit.collider.gameObject.layer == 12) 
					{
						other.transform.root.GetComponent<PlayerStatus> ().receberDano (Damage, hit.point, false, 0); 
						other.transform.root.GetComponent<PlayerStatus> ().mostrarStatus();
					}
				}
			}
		}

	}
}
