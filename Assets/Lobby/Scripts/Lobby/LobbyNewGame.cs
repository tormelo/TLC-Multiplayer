using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
	public class LobbyNewGame : MonoBehaviour {

		public LobbyManager lobbyManager;
		
		public InputField matchNameInput;

		public Button ConfirmButton;

		void Update () {
			if (matchNameInput.text == "") {
				ConfirmButton.interactable = false;
			} else {
				ConfirmButton.interactable = true;
			}
		}

		public void OnEnable () {
//			EventSystem.current.SetSelectedGameObject (matchNameInput.gameObject);
//			matchNameInput.OnPointerClick(new PointerEventData(EventSystem.current));
			matchNameInput.ActivateInputField();
			matchNameInput.Select ();
			matchNameInput.text = "";
			matchNameInput.onEndEdit.RemoveAllListeners();
			matchNameInput.onEndEdit.AddListener(onEndEditName);
		}
			
		public void OnClickCreateMatchmakingGame()
		{
			lobbyManager.roomName = matchNameInput.text;
			this.gameObject.SetActive (false);
//			lobbyManager.StartMatchMaker();
			lobbyManager.matchMaker.CreateMatch(
				matchNameInput.text,
				(uint)lobbyManager.maxPlayers,
				true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);

			lobbyManager.DisplayIsConnecting();
		}
			
		void onEndEditName(string text)
		{
			if (Input.GetKeyDown(KeyCode.Return) && matchNameInput.text != "")
			{
				OnClickCreateMatchmakingGame();
			}
		}
	}
}
