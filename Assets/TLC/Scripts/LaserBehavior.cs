using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {

	[Range(0,1)]
	public int Type;
    public int Damage;
	[Range(-1,1)]
	public int Sin;
	public float SinAmplitude;
	public float SinSpeed;
	public bool Piercing;
	public bool Hooking;
    public float Speed;

	private Rigidbody _rb;
	private float timeCount;

	void refletir()
	{
		Type = 0;
		_rb.velocity = -_rb.velocity;
		transform.Rotate (new Vector3 (0, 180, 0));
	}
		
	void autoDestruir(){
		//Funçao para destruir o laser
		Destroy(this.gameObject);
	}

	void timer(float time){
		//Chama a funçao de destruir apos 1 segundo
		Invoke("autoDestruir", time);
	}

	void OnTriggerEnter(Collider other) {
		if (Type == 0) 
		{
			if (other.gameObject.layer == 10) 
			{
				other.transform.root.GetComponent<EnemyStatus> ().receberDano (Damage, transform.position, false, 0);
				if (!Piercing) 
				{
					autoDestruir ();
				} 
			}
		}
		else if (Type == 1) 
		{
			if (other.gameObject.layer == 10 || other.gameObject.layer == 9) 
			{
				return;
			} 
			else if (other.gameObject.layer == 12) 
			{
				other.transform.root.GetComponent<PlayerStatus> ().receberDano (Damage, transform.position, false, 0);
				other.transform.root.GetComponent<PlayerStatus> ().mostrarStatus ();
				autoDestruir ();
			} 
			else if (other.tag == "Refletir") 
			{
				CancelInvoke ();
				timer (1);
				refletir ();
			} 
		}

		if (other.gameObject.layer == 8) 
		{
			if (!Hooking) 
			{
				autoDestruir ();
			} 
			else 
			{
				_rb.isKinematic = true;
			}
		}

		if (other.gameObject.tag == "Laser" && Piercing) 
		{
			autoDestruir ();
			Destroy (other.gameObject);
		}
	}
		
	void Start () {
		transform.parent = null;

		timeCount = 0;

		//Coloca o laser em movimento assim que ele for inicializado
		_rb = GetComponent<Rigidbody>();
		_rb.velocity = transform.forward * Speed;

		timer (1);
	}

	void FixedUpdate()
	{
		timeCount += Time.fixedDeltaTime;

		if (Sin == 1)
		{
			transform.Translate (Mathf.Sin(timeCount*SinSpeed)*SinAmplitude, 0, 0);
		}
		else if (Sin == -1) 
		{
			transform.Translate (-Mathf.Sin(timeCount*SinSpeed)*SinAmplitude, 0, 0);
		}
	}
}
