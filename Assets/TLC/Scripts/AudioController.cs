using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

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
	}
}
