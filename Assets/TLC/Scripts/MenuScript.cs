using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Animator anim;
	public Text Polys;
	public AudioMixerGroup MixerPrincipal;
	public Text TextoAudio; 

	public void mudarAudio()
	{
		if (SaveSystem.current.sound) 
		{
			TextoAudio.text = "OFF";
			MixerPrincipal.audioMixer.SetFloat ("Volume", -80);
			SaveSystem.current.sound = false;
		}
		else
		{
			TextoAudio.text = "ON";
			MixerPrincipal.audioMixer.SetFloat ("Volume", 0);
			SaveSystem.current.sound = true;
		}

		SaveSystem.Save();
	}

	public void open(GameObject objeto)
	{
		objeto.SetActive (true);
	}

	public void close(GameObject objeto)
	{
		objeto.SetActive (false);
	}

	public void stages()
	{
		anim.SetTrigger ("Stages");
	}

	public void configurations()
	{
		anim.SetTrigger ("Configurations");
	}

	public void storeHot()
	{
		anim.SetTrigger ("Store_Hot");
	}

	public void storeWeapons()
	{
		anim.SetTrigger ("Store_Weapons");
	}

	public void storePUPS()
	{
		anim.SetTrigger ("Store_PUPS");
	}

	public void Jogar (string stage){
		//Application.LoadLevel (Application.loawadedLevel);
		SceneManager.LoadScene(stage);
	}

	void Start()
	{
		//SaveSystem.Erase ();
		if (SaveSystem.current.sound) 
		{
			TextoAudio.text = "ON";
			MixerPrincipal.audioMixer.SetFloat ("Volume", 0);
		}
		else
		{
			TextoAudio.text = "OFF";
			MixerPrincipal.audioMixer.SetFloat ("Volume", -80);
		}

		SaveSystem.Load ();
	}

	void Update()
	{
		if (Polys.text != SaveSystem.current.polys.ToString ()) 
		{
			Polys.text = SaveSystem.current.polys.ToString ();
		}
	}
}
