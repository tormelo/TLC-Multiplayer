using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {

	public GameObject[] Stages = new GameObject[8];

	void Start () {
		//Mostra somente os estagios liberados
		for (int i = 0; i < 8; i++) 
		{
			if (SaveSystem.current.stages [i]) 
			{
				Stages [i].SetActive (true);
			}
		}
	}
}
