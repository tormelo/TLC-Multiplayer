using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player {
	public string name;
	public float r, g, b;
	public int k, d, hiscore, rank;

	//Construtor padrão que não leva argumentos
	public Player()
	{
		name = "";
		r = 1;
		g = 1;
		b = 1;
		k = 0;
		d = 0;
		hiscore = 0;
		rank = 0;
	}

	public Color GetColor() {
		return new Color (r, g, b);
	}

	public void SetColor (Color color) {
		r = color.r;
		g = color.g;
		b = color.b;
	}
}

[System.Serializable]
public class Game {

	public int polys;
	public bool[] stages = new bool[8];
	public int[] score = new int[8];
	public int[] stars = new int[8];

	public float hiScore;
	public bool[] weaponsOwned = new bool[10];
	public int selectedWeapon;

	public bool[] perksOwned = new bool[3];
	public int selectedPerk;

	public float playerLife;

	//Configurações
	public bool miraAutomatica;
	public bool esconderLife;
	public bool sound;


	//Construtor padrão que não leva argumentos
	public Game()
	{
		polys = 0;

		//Configuração inicial das fases liberadas
		stages [0] = true;

		for (int i = 1; i < 8; i++) 
		{
			stages [i] = false;
		}

		for (int i = 0; i < 8; i++) 
		{
			score [i] = 0;
			stars [i] = 0;
		}

		hiScore = 0;


		//Configuração inicial das armas com weaponsOwned[0] sendo a unica liberada a principio
		weaponsOwned [0] = true;
		for (int i = 1; i < 10; i++) 
		{
			weaponsOwned [i] = false;
		}
		selectedWeapon = 0;

		for (int i = 0; i < 3; i++) 
		{
			perksOwned [i] = false;
		}
		selectedPerk = -1;

		playerLife = 10;

		//Configurações
		miraAutomatica = true;
		esconderLife = true;
		sound = true;
	}
}
