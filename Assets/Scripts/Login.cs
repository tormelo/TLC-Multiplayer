using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Prototype.NetworkLobby {
	public class Login : MonoBehaviour {

		public LobbyManager lobbyManager;
		public GameObject LoginPanel;
		public InputField NameInput;
		public Image ColorImage;
		public Slider RedSlider, GreenSlider, BlueSlider;
		public Button ConfirmButton;

		void Start () {
			EventSystem.current.SetSelectedGameObject (NameInput.gameObject);
		}

		void Update () {
			if (NameInput.text == "") {
				ConfirmButton.interactable = false;
			} else {
				ConfirmButton.interactable = true;
			}
		}

		public void updateColor () {
			ColorImage.color = new Color (RedSlider.value, GreenSlider.value, BlueSlider.value);
		}

		public void confirm () {
			SaveSystem.player.name = NameInput.text;
			SaveSystem.player.SetColor(ColorImage.color);
			SaveSystem.Save ();
			lobbyManager.ChangeTo (lobbyManager.gamesPanel);
		}
	}
}