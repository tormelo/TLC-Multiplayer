using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PromoConfig : MonoBehaviour {
	public string Name;
	public int Number;
	public int Price;
	public Text Header;
	public GameObject Preco;
	public GameObject BotaoComprar;
	public GameObject PopUpError;

	public void comprar ()
	{
		if (SaveSystem.current.polys >= Price) 
		{
			SaveSystem.current.polys -= Price;

			if (Number == 0) 
			{
				for (int i = 1; i < 10; i++) 
				{
					SaveSystem.current.weaponsOwned [i] = true;
				}
			}

			if (Number == 1) 
			{
				SaveSystem.current.weaponsOwned [8] = true;
				SaveSystem.current.weaponsOwned [9] = true;
			}

			if (Number == 2) 
			{
				for (int i = 0; i < 3; i++) 
				{
					SaveSystem.current.perksOwned [i] = true;
				}
			}

			SaveSystem.Save ();
		} 
		else
		{
			PopUpError.SetActive (true);
		}
	}

	bool verificar0()
	{
		int n = 0;

		for (int i = 0; i < 10; i++) 
		{
			if (SaveSystem.current.weaponsOwned [i] == false) 
			{
				n++;
			}
		}

		if (n > 0) 
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool verificar1()
	{
		if (SaveSystem.current.weaponsOwned [8] && SaveSystem.current.weaponsOwned [9]) 
		{
			return false;
		} 
		else 
		{
			return true;
		}
	}

	bool verificar2()
	{
		int n = 0;

		for (int i = 0; i < 3; i++) 
		{
			if (SaveSystem.current.perksOwned [i] == false) 
			{
				n++;
			}
		}

		if (n > 0) 
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void Start()
	{
		Header.text = Name;
		Preco.GetComponentInChildren<Text> ().text = Price.ToString();
	}
		
	void Update () 
	{
		if (Number == 0) 
		{
			if (verificar0()) 
			{
				Preco.SetActive (true);
				BotaoComprar.GetComponent<Button> ().interactable = true;
				BotaoComprar.GetComponentInChildren<Text> ().text = "BUY";
			} 
			else 
			{
				Preco.SetActive (false);
				BotaoComprar.GetComponent<Button> ().interactable = false;
				BotaoComprar.GetComponentInChildren<Text> ().text = "ACQUIRED";
			}
		}

		if (Number == 1)
		{
			if (verificar1()) 
			{
				Preco.SetActive (true);
				BotaoComprar.GetComponent<Button> ().interactable = true;
				BotaoComprar.GetComponentInChildren<Text> ().text = "BUY";
			} 
			else 
			{
				Preco.SetActive (false);
				BotaoComprar.GetComponent<Button> ().interactable = false;
				BotaoComprar.GetComponentInChildren<Text> ().text = "ACQUIRED";
			}
		}

		if (Number == 2)
		{
			if (verificar2()) 
			{
				Preco.SetActive (true);
				BotaoComprar.GetComponent<Button> ().interactable = true;
				BotaoComprar.GetComponentInChildren<Text> ().text = "BUY";
			} 
			else 
			{
				Preco.SetActive (false);
				BotaoComprar.GetComponent<Button> ().interactable = false;
				BotaoComprar.GetComponentInChildren<Text> ().text = "ACQUIRED";
			}
		}
	}
}
