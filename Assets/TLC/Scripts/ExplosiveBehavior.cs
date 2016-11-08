using UnityEngine;
using System.Collections;

public class ExplosiveBehavior : MonoBehaviour {

	public GameObject Explosion;
	[Range(0,1)]
	public int Type;
	public bool Bomb;
    public int Damage;
	public bool ExplodeOnContact;
	public float ActivationTime;
	public float ExplosionDuration;
	public float HitDuration;
    public float Speed;

	private Rigidbody _rb;

	void stopHitCollider()
	{
		Explosion.transform.FindChild ("Collider").gameObject.SetActive (false);
	}

	void autoDestruir(){
		//Funçao para destruir o laser
		Destroy(this.gameObject);
	}

	void explosion(){
		Explosion.SetActive (true);

		if (HitDuration < ExplosionDuration) 
		{
			Invoke ("stopHitCollider", HitDuration);
		}
		Invoke ("autoDestruir", ExplosionDuration);

		if (Bomb) 
		{
			GameObject bomb = transform.FindChild ("Bomb").gameObject;
			Collider col = GetComponent<Collider> ();
			col.enabled = false;
			bomb.SetActive (false);
			_rb.isKinematic = true;
		}
	}

	void timer(){
		//Chama a funçao de destruir
		Invoke("explosion", ActivationTime);
	}

	void OnTriggerEnter(Collider other) {
		if (Type == 0) 
		{
			if (other.gameObject.layer == 10) 
			{
				if (ExplodeOnContact) 
				{
					CancelInvoke ();
					other.transform.root.GetComponent<EnemyStatus> ().receberDano (Damage,  other.transform.position, true, 0);
					explosion ();
				} 
			}
		}
		else if (Type == 1) 
		{
			if (ExplodeOnContact) 
			{
				if (other.gameObject.layer == 12) 
				{
					CancelInvoke ();
					other.transform.root.GetComponent<PlayerStatus> ().receberDano (Damage, other.transform.position, false, 0);
					other.transform.root.GetComponent<PlayerStatus> ().mostrarStatus ();
					explosion ();
					Debug.Log ("Player");
				} 
				else if (other.gameObject.layer == 8 || other.gameObject.layer == 11) 
				{
					CancelInvoke ();
					explosion ();

					Debug.Log ("Env");
				}
				else if (other.gameObject.layer == 10 || other.gameObject.layer == 9) 
				{
					return;
				} 
			} 
		}
	}
		
	void Start () {
		//Coloca o laser em movimento assim que ele for inicializado
		_rb = GetComponent<Rigidbody>();
		_rb.velocity = transform.forward * Speed;

		timer ();
	}
}
