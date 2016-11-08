using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class DisparoJogador : MonoBehaviour {

	public Animator animator;
	public GameObject Projectile;
	public GameObject ProjectileSpawner;
	private AudioSource aSource;

	private Vector3 posSpawner;

	public void spawnarProjetil()
	{
		Instantiate (Projectile, ProjectileSpawner.transform.position, ProjectileSpawner.transform.rotation);
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
			animator.SetBool ("Firing", true);
		}

		if (disparando == 0)
		{
			animator.SetBool ("Firing", false);
		}
	}

	void disparoDesktop()
	{
		if (Input.GetMouseButton (0) && GameObject.Find("GameMaster").GetComponent<Master>().estadoJogador != 1) 
		{
			animator.SetBool ("Firing", true);
		}

		if (!Input.GetMouseButton (0))
		{
			animator.SetBool ("Firing", false);
		}
	}

	public void audioDisparo()
	{
		aSource.PlayOneShot (aSource.clip);
	}

	void Start()
	{
		aSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
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
