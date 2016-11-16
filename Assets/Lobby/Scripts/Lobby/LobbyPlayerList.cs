using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
    //List of players in the lobby
    public class LobbyPlayerList : MonoBehaviour
    {
		public LobbyManager lobbyManager;
		public static LobbyPlayerList _instance = null;
        public RectTransform playerListContentTransform;
//        public Transform addButtonRow;
		public Text RoomName;
		public GameObject WaitingMessage;

        protected VerticalLayoutGroup _layout;
        protected List<LobbyPlayer> _players = new List<LobbyPlayer>();

        public void OnEnable()
        {
			RoomName.text = lobbyManager.roomName;
			_instance = this;
            _layout = playerListContentTransform.GetComponent<VerticalLayoutGroup>();
        }
			
        void Update()
        {
            //this dirty the layout to force it to recompute evryframe (a sync problem between client/server
            //sometime to child being assigned before layout was enabled/init, leading to broken layouting)

			lobbyManager.StartButton.gameObject.SetActive (lobbyManager.localState == "host");
			lobbyManager.StartButton.interactable = lobbyManager._playerNumber >= lobbyManager.minPlayers;
			WaitingMessage.SetActive (lobbyManager.localState == "client");

            if(_layout)
                _layout.childAlignment = Time.frameCount%2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;

//			Debug.Log (lobbyManager.localState);
        }

		//Adiciona um jogador a lista
        public void AddPlayer(LobbyPlayer player)
        {
            if (_players.Contains(player))
                return;

            _players.Add(player);

            player.transform.SetParent(playerListContentTransform, false);
//            addButtonRow.transform.SetAsLastSibling();

            PlayerListModified();

			player.initialConfig ();
        }

		//Remove um jogador da lista
        public void RemovePlayer(LobbyPlayer player)
        {
            _players.Remove(player);
            PlayerListModified();
        }

		//Verifica modificações na lista de jogadores e chama funcao para colorir de acordo
        public void PlayerListModified()
        {
            int i = 0;
            foreach (LobbyPlayer p in _players)
            {
                p.OnPlayerListChanged(i);
                ++i;
            }
        }
    }
}
