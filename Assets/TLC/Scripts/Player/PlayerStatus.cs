using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {

	private float Life;
	public GameObject Efeito;
	public GameObject Panel;
	public Image Lifebar;
	private AudioSource moeda;

	public AnimationCurve Cutoff;
	public AudioMixer mixer;

	public GameObject Model;
	public GameObject DeadBody;
	private bool invincible;

	public void somMoeda()
	{
		moeda.PlayOneShot (moeda.clip);
	}

	public void receberDano(int damage, Vector3 hitPos, bool makeInvulnerable, float invincibleTime)
	{
		if (!invincible && Life > 0)
		{
			Life -= damage;
			Instantiate (Efeito, hitPos, Quaternion.identity);

			if (SaveSystem.current.selectedPerk == 2) 
			{
				Lifebar.fillAmount = Life/(SaveSystem.current.playerLife*2);
			} 
			else
			{
				Lifebar.fillAmount = Life/SaveSystem.current.playerLife;
			}
		
			mixer.SetFloat ("Cutoff", Cutoff.Evaluate(10-Life)*22000f);
		}

		if (makeInvulnerable && !invincible) 
		{
			invincible = true;
			Invoke ("makeVunerable", invincibleTime);
			Debug.Log (invincibleTime);
		}
	}

	void makeVunerable()
	{
		Debug.Log ("HUE");
		invincible = false;
	}

	void Start(){
		moeda = GetComponent<AudioSource> ();

		if (!SaveSystem.current.esconderLife) 
		{
			Panel.SetActive (true);
		}

		if (SaveSystem.current.selectedPerk == 2) 
		{
			Life = SaveSystem.current.playerLife*2;
		} 
		else 
		{
			Life = SaveSystem.current.playerLife;
		}
			
		Lifebar.fillAmount = 1;
		mixer.SetFloat ("Cutoff", 22000f);
		invincible = false;
	}



	public void mostrarStatus(){

		if (SaveSystem.current.esconderLife && DeadBody.activeSelf == false) 
		{
			CancelInvoke ("apagarHealthBar");
			if (Life > 0) 
			{
				Invoke ("apagarHealthBar", 1);
			}
			else
			{
				Invoke ("apagarHealthBar", 0.2f);
			}
			Panel.SetActive (true);
		}
		//Debug.Log ("O jogador recebeu " + dano + " pontos de dano");
	}

	void apagarHealthBar(){
		Panel.SetActive (false);
	}

	void FixedUpdate(){
		if (Life <= 0) {
			GameObject.Find("GameMaster").GetComponent<Master>().estadoJogador = 1;
			Model.SetActive(false);
			DeadBody.SetActive(true);
			if (!SaveSystem.current.esconderLife) 
			{
				Invoke ("apagarHealthBar", 0.2f);
			}
		}

		if (!SaveSystem.current.esconderLife) 
		{
			if (!Panel.activeSelf) 
			{
				Panel.SetActive (true);
			}
		}
	}
}
