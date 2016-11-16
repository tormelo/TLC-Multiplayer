using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.NetworkLobby
{
    //Player entry in the lobby. Handle selecting color/setting name & getting ready for the game
    //Any LobbyHook can then grab it and pass those value to the game player prefab (see the Pong Example in the Samples Scenes)
    public class LobbyPlayer : NetworkLobbyPlayer
    {
		private LobbyManager lobbyManager;
		public Image colorImage;
		public Text nameText;
        public Button removePlayerButton;

		public GameObject localHost, localClient, remoteHost, remoteClient;

//		public GameObject localIcone;
//		public GameObject remoteIcone;

        //OnMyName function will be invoked on clients when server change the value of playerName
        [SyncVar]
		public string playerName;
        [SyncVar]
		public Color playerColor;
		[SyncVar]
		public string playerStatus;

		static Color OddRowColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		static Color EvenRowColor = new Color(.94f, .94f, .94f, 1.0f);
	
		void Start()
		{
			base.OnClientEnterLobby();

			if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(1);

			LobbyPlayerList._instance.AddPlayer(this);
			//            LobbyPlayerList._instance.DisplayDirectServerWarning(isServer && LobbyManager.s_Singleton.matchMaker == null);

//			Debug.Log (lobbyManager + "hhihi");

			//setup the player data on UI. The value are SyncVar so the player
			//will be created with the right value currently on server
			//            OnMyName(playerName);
			//            OnMyColor(playerColor);
		}

		public void initialConfig () {
			lobbyManager = transform.root.GetComponent<LobbyManager>();

			if (lobbyManager.localState == "host") {
				lobbyManager.StartButton.onClick.RemoveAllListeners ();
				lobbyManager.StartButton.onClick.AddListener (OnReadyClicked);
			} else {
				lobbyManager.StartButton.onClick.RemoveAllListeners ();
			}
				
			if (isLocalPlayer)
			{
				SetupLocalPlayer();
			}
			else
			{
				SetupOtherPlayer();
			}
		}

		//        public override void OnClientEnterLobby()
//        {
//            base.OnClientEnterLobby();
//
//            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(1);
//
//            LobbyPlayerList._instance.AddPlayer(this);
////            LobbyPlayerList._instance.DisplayDirectServerWarning(isServer && LobbyManager.s_Singleton.matchMaker == null);
//
//            if (isLocalPlayer)
//            {
//                SetupLocalPlayer();
//            }
//            else
//            {
//                SetupOtherPlayer();
//            }
//
//			Debug.Log ("hihi");
//
//            //setup the player data on UI. The value are SyncVar so the player
//            //will be created with the right value currently on server
////            OnMyName(playerName);
////            OnMyColor(playerColor);
//        }

//        public override void OnStartAuthority()
//        {
//            base.OnStartAuthority();
//
//            //if we return from a game, color of text can still be the one for "Ready"
////            readyButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//
//           SetupLocalPlayer();
//        }

        void SetupOtherPlayer()
        {
			remoteHost.gameObject.SetActive(playerStatus == "host");
			remoteClient.gameObject.SetActive(playerStatus == "client");

			OnMyName (playerName);
			OnMyColor (playerColor);

			removePlayerButton.interactable = NetworkServer.active;

            OnClientReady(false);
        }

        void SetupLocalPlayer()
        {
			playerName = SaveSystem.player.name;
			playerColor = SaveSystem.player.GetColor();
			playerStatus = lobbyManager.localState;

			OnMyName (playerName);
			OnMyColor (playerColor);

			localHost.gameObject.SetActive(playerStatus == "host");
			localClient.gameObject.SetActive(playerStatus == "client");

			if(playerStatus == "client")
				SendReadyToBeginMessage();

//			remoteIcone.gameObject.SetActive(false);
//            localIcone.gameObject.SetActive(true);

            CheckRemoveButton();
		
//			CmdColorChange(SaveSystem.player.GetColor ());
//		
//            //have to use child count of player prefab already setup as "this.slot" is not set yet
//            CmdNameChanged("Player" + (LobbyPlayerList._instance.playerListContentTransform.childCount-1));
//
            //when OnClientEnterLobby is called, the loval PlayerController is not yet created, so we need to redo that here to disable
            //the add button if we reach maxLocalPlayer. We pass 0, as it was already counted on OnClientEnterLobby
            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(0);
        }

        //This enable/disable the remove button depending on if that is the only local player or not
        public void CheckRemoveButton()
        {
            if (!isLocalPlayer)
                return;

            int localPlayerCount = 0;
            foreach (PlayerController p in ClientScene.localPlayers)
                localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;

            removePlayerButton.interactable = localPlayerCount > 1;
        }

        public void OnPlayerListChanged(int idx)
        { 
            GetComponent<Image>().color = (idx % 2 == 0) ? EvenRowColor : OddRowColor;
        }

		public void OnReadyClicked()
		{
			SendReadyToBeginMessage();
		}

        ///===== callback from sync var

        public void OnMyName(string newName)
        {
            playerName = newName;
            nameText.text = playerName;
        }

        public void OnMyColor(Color newColor)
        {
            playerColor = newColor;
            colorImage.color = newColor;
        }

        //===== UI Handler

        //Note that those handler use Command function, as we need to change the value on the server not locally
        //so that all client get the new value throught syncvar

        public void OnRemovePlayerClick()
        {
            if (isLocalPlayer)
            {
                RemovePlayer();
            }
            else if (isServer)
                LobbyManager.s_Singleton.KickPlayer(connectionToClient);
                
        }

        [ClientRpc]
        public void RpcUpdateCountdown(int countdown)
        {
			LobbyManager.s_Singleton.countdownPanel.UIText.text = "MATCH STARTING IN " + countdown;
            LobbyManager.s_Singleton.countdownPanel.gameObject.SetActive(countdown != 0);
        }

        [ClientRpc]
        public void RpcUpdateRemoveButton()
        {
            CheckRemoveButton();
        }
			
        //Cleanup thing when get destroy (which happen when client kick or disconnect)
        public void OnDestroy()
        {
            LobbyPlayerList._instance.RemovePlayer(this);
            if (LobbyManager.s_Singleton != null) LobbyManager.s_Singleton.OnPlayersNumberModified(-1);
        }
    }
}
