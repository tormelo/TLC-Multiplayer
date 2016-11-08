using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

	public bool Boss;
	public int Life;
	public bool Attacking; //Flag para considerar dano
	public bool movel;
	public Collider MovCollider;
	public GameObject[] Efeitos;
	private bool invincible;

	public GameObject Polygon;

	public void receberDano(int damage, Vector3 hitPos, bool makeInvulnerable, float invincibleTime)
	{
		if(!invincible && Life > 0)
		{
			if (damage == 1) 
			{
				Life--;
				Instantiate (Efeitos [0], hitPos, Quaternion.identity);
			} 
			else if (damage > 1 && damage <= 3) 
			{
				Life -= damage;
				Instantiate (Efeitos [1], hitPos, Quaternion.identity);
			}
			else if (damage > 3) 
			{
				Life -= damage;
				Instantiate (Efeitos [2], hitPos, Quaternion.identity);
			}

			if (Boss) 
			{
				for (int i = 0; i < damage; i++) 
				{
					spawnarPolygon ();
				}
			}
		}

		if (makeInvulnerable && !invincible) 
		{
			invincible = true;
			Invoke ("makeVunerable", invincibleTime);
		}
	}

	void spawnarPolygon()
	{
		Instantiate (Polygon, new Vector3(transform.position.x + Random.Range(-3, 3), 2, transform.position.z + Random.Range(-3, 3)), transform.rotation);
	}

	void makeVunerable()
	{
		invincible = false;
	}

	void Start () {
		//Torna o ataque possivel
		Attacking = false;
		invincible = false;
	}

	void FixedUpdate () {
		if (Life <= 0) 
		{
			MovCollider.enabled = false;

			if (movel) 
			{
				GetComponent<NavMeshAgent> ().enabled = false;
			}

			gameObject.SendMessage ("morrer");
		}
	}
}
