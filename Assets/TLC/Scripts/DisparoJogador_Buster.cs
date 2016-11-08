using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class DisparoJogador_Buster : MonoBehaviour {

	public Animator animator;
	public GameObject[] Projectiles = new GameObject[3];
	private AudioSource[] audios = new AudioSource[3];
	public GameObject ProjectileSpawner;

	public float tempoMedio;
	public float tempoForte;

	private Vector3 posSpawner;
	private float chargeTimer;
	private bool charging;

	public void spawnarProjetil()
	{
		if (chargeTimer < tempoMedio) 
		{
			Instantiate (Projectiles[0], ProjectileSpawner.transform.position, ProjectileSpawner.transform.rotation);
			audios [0].PlayOneShot (audios [0].clip);
		}
		else if (tempoMedio <= chargeTimer && chargeTimer < tempoForte) 
		{
			Instantiate (Projectiles[1], ProjectileSpawner.transform.position, ProjectileSpawner.transform.rotation);
			audios [0].PlayOneShot (audios [1].clip);
		}
		else if (chargeTimer > tempoForte) 
		{
			Instantiate (Projectiles[2], ProjectileSpawner.transform.position, ProjectileSpawner.transform.rotation);
			audios [0].PlayOneShot (audios [2].clip);
		}


		chargeTimer = 0;
		animator.SetFloat ("charge", chargeTimer);
	}

	void disparoMobile ()
	{
		float disparando;

		if (SaveSystem.current.miraAutomatica) 
		{
			disparando = GameObject.Find ("Right Trigger").GetComponent<TriggerScript> ().pos;
		} 
		else 
		{
			disparando = GameObject.Find ("Right Joystick").GetComponent<JoystickScript> ().pos.magnitude;
		}

		if (disparando > 0 && GameObject.Find("GameMaster").GetComponent<Master>().estadoJogador != 1) 
		{
			if (!charging) 
			{
				charging = true;

				animator.SetBool ("Charging", true);
			}

			chargeTimer += Time.fixedDeltaTime;

			animator.SetFloat ("charge", chargeTimer);
		}

		if (disparando == 0)
		{
			if (charging) 
			{
				charging = false;

				animator.SetBool ("Charging", false);

				animator.SetTrigger ("Firing");
			}
		}
	}

	void disparoDesktop()
	{
		if (Input.GetMouseButton (0) && GameObject.Find("GameMaster").GetComponent<Master>().estadoJogador != 1) 
		{
			if (!charging) 
			{
				charging = true;

				animator.SetBool ("Charging", true);
			}

			chargeTimer += Time.fixedDeltaTime;

			animator.SetFloat ("charge", chargeTimer);
		}

		if (!Input.GetMouseButton (0))
		{
			if (charging) 
			{
				charging = false;

				animator.SetBool ("Charging", false);

				animator.SetTrigger ("Firing");
			}
		}
	}

	void Start()
	{
		charging = false;

		chargeTimer = 0;

		audios = GetComponents<AudioSource> ();
	}

	void FixedUpdate () 
	{
		if (SystemInfo.deviceType == DeviceType.Handheld) 
		{
			disparoMobile ();
		} 
		else 
		{
			disparoDesktop ();
		}
	}
}
