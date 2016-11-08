using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Animacoes : MonoBehaviour {

	public Text Contagem;

	public void c3()
	{
		Contagem.text = "3";
	}
	public void c2()
	{
		Contagem.text = "2";
	}
	public void c1()
	{
		Contagem.text = "1";
	}
	public void c0()
	{
		Contagem.text = "GO!";
	}
}
