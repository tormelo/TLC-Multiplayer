using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
	public class LobbyEditName : MonoBehaviour {

		public LobbyManager lobbyManager;

		public InputField nameInput;

		public Text name;

		public Button ConfirmButton;

		void Update () {
			if (nameInput.text == "") {
				ConfirmButton.interactable = false;
			} else {
				ConfirmButton.interactable = true;
			}
		}

		public void OnEnable () {
//			EventSystem.current.SetSelectedGameObject (nameInput.gameObject);
//			nameInput.OnPointerClick(new PointerEventData(EventSystem.current));
			nameInput.ActivateInputField();
			nameInput.Select ();
			nameInput.text = SaveSystem.player.name;
			nameInput.onEndEdit.RemoveAllListeners();
			nameInput.onEndEdit.AddListener(onEndEditName);
		}

		public void ChangeName()
		{
			this.gameObject.SetActive (false);
			SaveSystem.player.name = nameInput.text;
			SaveSystem.Save ();
			name.text = SaveSystem.player.name;
		}

		void onEndEditName(string text)
		{
			if (Input.GetKeyDown(KeyCode.Return) && nameInput.text != "")
			{
				ChangeName();
			}
		}
	}
}

