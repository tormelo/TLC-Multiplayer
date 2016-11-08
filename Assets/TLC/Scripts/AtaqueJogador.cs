using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AtaqueJogador : MonoBehaviour {

	public Animator animator;
	private AudioSource audio;

	public void randomizarAtaque()
	{
		if (animator.GetInteger ("Attack") >= 2) 
		{
			int num = Random.Range (0, 2);
			animator.SetInteger ("Attack", num);
		} 
		else 
		{
			int num = Random.Range (2, 4);
			animator.SetInteger ("Attack", num);
		}
	}

	public void randomizarInicio()
	{
		int num = Random.Range (0, 4);
		animator.SetInteger ("Attack", num);
	}

	void ataqueMobile ()
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
			animator.SetBool ("Attacking", true);
		}

		if (disparando == 0)
		{
			animator.SetBool ("Attacking", false);
		}
	}

	void ataqueDesktop ()
	{
		if (Input.GetMouseButton (0) && GameObject.Find("GameMaster").GetComponent<Master>().estadoJogador != 1) 
		{
			animator.SetBool ("Attacking", true);
		}

		if (!Input.GetMouseButton (0))
		{
			animator.SetBool ("Attacking", false);
		}
	}

	public void tocarAudio()
	{
		//audio.pitch = Random.Range (0.7f, 0.75f);
		audio.PlayOneShot (audio.clip);
	}

	void Start()
	{
		audio = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		if (SystemInfo.deviceType == DeviceType.Handheld) 
		{
			ataqueMobile ();
		} 
		else 
		{
			ataqueDesktop ();
		}
	}
}
