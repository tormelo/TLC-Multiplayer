using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace Prototype.NetworkLobby
{
	public class LobbyChangeColor : MonoBehaviour {

		public LobbyManager lobbyManager;

		public Image ColorImage;
		public Image colorIcon;
		public Slider RedSlider, GreenSlider, BlueSlider;

		void Update () {
			if (Input.GetKeyDown(KeyCode.Return))
			{
				ChangeColor();
			}
		}

		public void OnEnable () {
			RedSlider.value = SaveSystem.player.r;
			GreenSlider.value = SaveSystem.player.g;
			BlueSlider.value = SaveSystem.player.b;
			onColorValueChanged (0);

			RedSlider.onValueChanged.RemoveAllListeners();
			RedSlider.onValueChanged.AddListener(onColorValueChanged);
			GreenSlider.onValueChanged.RemoveAllListeners();
			GreenSlider.onValueChanged.AddListener(onColorValueChanged);
			BlueSlider.onValueChanged.RemoveAllListeners();
			BlueSlider.onValueChanged.AddListener(onColorValueChanged);
		}

		public void ChangeColor()
		{
			this.gameObject.SetActive (false);
			SaveSystem.player.SetColor (ColorImage.color);
			SaveSystem.Save ();
			colorIcon.color = SaveSystem.player.GetColor ();
		}

		void onColorValueChanged(float value)
		{
			ColorImage.color = new Color (RedSlider.value, GreenSlider.value, BlueSlider.value);
		}
	}
}


