using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//public enum StageNumber{
//	ONE = 0,
//	TWO = 1,
//	THREE = 2,
//	FOUR = 3,
//	FIVE = 4,
//	SIX = 5,
//	SEVEN = 6,
//	EIGHT = 7,
//	NINE = 8
//}

public class StageInfo : MonoBehaviour {


	public StageNumber Stage;
	public Text StageText;
	public GameObject[] Stars = new GameObject[4];
	public Text Score;

	void Start () {
		//Configuração do nome
		StageText.text = "STAGE " + ((int)Stage + 1).ToString();

		//Configuração estrelas
		for (int i = 0; i < 4; i++) 
		{
			if (SaveSystem.current.stars [(int)Stage] == i) 
			{
				Stars [i].SetActive (true);
			} 
			else 
			{
				Stars [i].SetActive (false);
			}
		}

		//Configuração do score
		Score.text = "SCORE " + SaveSystem.current.score[(int)Stage].ToString();
	}
}
