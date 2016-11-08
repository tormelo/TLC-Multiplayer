using UnityEngine;
using System.Collections;

public class WeaponSelect : MonoBehaviour {

	public GameObject[] Weapons;

	void selectWeapon()
	{
		//Desativa todas as armas
		for (int i = 0; i < Weapons.Length; i++) 
		{
			Weapons [i].SetActive (false);
		}

		//Ativa a arma selecionada
		Weapons[SaveSystem.current.selectedWeapon].SetActive(true);
	}

	public void changeWeapon()
	{
		if (SaveSystem.current.selectedWeapon < 9) 
		{
			SaveSystem.current.selectedWeapon++;
		} 
		else if (SaveSystem.current.selectedWeapon == 9) 
		{
			SaveSystem.current.selectedWeapon = 0;
		}

		selectWeapon ();
	}

	void Start () 
	{
		selectWeapon ();
	}


	#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			changeWeapon ();
		}
	}
	#endif
}
