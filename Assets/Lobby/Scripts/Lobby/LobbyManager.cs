using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;


namespace Prototype.NetworkLobby
{
    public class LobbyManager : NetworkLobbyManager 
    {
        static short MsgKicked = MsgType.Highest + 1;

        static public LobbyManager s_Singleton;



        [Header("Unity UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 5.0f;

        [Space]
        [Header("UI Reference")]
       
		public RectTransform loginPanel;
        public RectTransform gamesPanel;
		public RectTransform lobbyPanel;
		public Button StartButton;

        public LobbyInfoPanel infoPanel;
        public LobbyCountdownPanel countdownPanel;
//        public GameObject addPlayerButton;

        protected RectTransform currentPanel;

		//Current local state
		[HideInInspector]
		public string localState = "";
		[HideInInspector]
		public string roomName = "";

        //Client numPlayers from NetworkManager is always 0, so we count (throught connect/destroy in LobbyPlayer) the number
        //of players, so that even client know how many player there is.
        [HideInInspector]
        public int _playerNumber = 0;
        
        protected ulong _currentMatchID;

        protected LobbyHook _lobbyHooks;

        void Start()
        {
//			StartMatchMaker ();
			SaveSystem.Load ();
			//Reinicializa o matchmaker
			//			StopMatchMaker();
			StartMatchMaker();

			s_Singleton = this;
            _lobbyHooks = GetComponent<Prototype.NetworkLobby.LobbyHook>();

			if (SaveSystem.player.name == "") {
				currentPanel = loginPanel;
			} else {
				currentPanel = gamesPanel;
			}

            GetComponent<Canvas>().enabled = true;

           	DontDestroyOnLoad(gameObject);

			ChangeTo (currentPanel);
        }

//		public override void StopMatchMaker() {
//			base.StopMatchMaker ();
//
//			StartMatchMaker ();
//		}
		public override void OnLobbyClientSceneChanged(NetworkConnection conn)
		{
			if (SceneManager.GetSceneAt(0).name == lobbyScene)
			{
				ChangeTo (gamesPanel);
			}
			else
			{
				ChangeTo(null);
			}
		}
  
        public void ChangeTo(RectTransform newPanel)
        {
            if (currentPanel != null)
            {
                currentPanel.gameObject.SetActive(false);
            }

            if (newPanel != null)
            {
                newPanel.gameObject.SetActive(true);
            }

            currentPanel = newPanel;
        }

        public void DisplayIsConnecting()
        {
            var _this = this;
			infoPanel.Display("CONNECTING...", "Cancel", () => { this.cancelClbk(); });
			//infoPanel.Display("Connecting...", "Cancel", () => { _this.backDelegate(); });

        }

		public void cancelClbk() {
			if (localState == "host") {
				if (currentPanel == gamesPanel) {
					StopHost ();
					ChangeTo (gamesPanel);
				} else {
					StopHostClbk ();
				}
			}

			if (localState == "client") {
				StopClientClbk ();
			}
		}

        public void RemovePlayer(LobbyPlayer player)
        {
            player.RemovePlayer();
        }
                 
        public void StopHostClbk()
        {
			matchMaker.DestroyMatch((NetworkID)_currentMatchID, 0, OnDestroyMatch);
        }

        public void StopClientClbk()
        {
            StopClient();
			ChangeTo(gamesPanel);
        }

        class KickMsg : MessageBase { }
        public void KickPlayer(NetworkConnection conn)
        {
            conn.Send(MsgKicked, new KickMsg());
        }




        public void KickedMessageHandler(NetworkMessage netMsg)
        {
            infoPanel.Display("Kicked by Server", "Close", null);
            netMsg.conn.Disconnect();
        }

        //===================

        public override void OnStartHost()
        {
            base.OnStartHost();

			ChangeTo(lobbyPanel);
        }

		public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
		{
			base.OnMatchCreate(success, extendedInfo, matchInfo);
            _currentMatchID = (System.UInt64)matchInfo.networkId;
			localState = "host";
			Debug.Log ("sucesso");
		}

		public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
		{
			base.OnMatchJoined(success, extendedInfo, matchInfo);
			localState = "client";
		}
		public override void OnDestroyMatch(bool success, string extendedInfo)
		{
			base.OnDestroyMatch(success, extendedInfo);
			if (success)
            {
				StopMatchMaker();
				StopHost();
				ChangeTo(gamesPanel);
//				StartMatchMaker ();
            }
        }
			
        public void OnPlayersNumberModified(int count)
        {
            _playerNumber += count;
        }

        // ----------------- Server callbacks ------------------

        public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
        {
            //This hook allows you to apply state data from the lobby-player to the game-player
            //just subclass "LobbyHook" and add it to the lobby object.

            if (_lobbyHooks)
				_lobbyHooks.OnLobbyServerSceneLoadedForPlayer(this, lobbyPlayer, gamePlayer);

            return true;
        }

        // --- Countdown management

        public override void OnLobbyServerPlayersReady()
        {
			bool allready = true;
			for(int i = 0; i < lobbySlots.Length; ++i)
			{
				if(lobbySlots[i] != null)
					allready &= lobbySlots[i].readyToBegin;
			}

			if(allready)
				StartCoroutine(ServerCountdownCoroutine());
        }

        public IEnumerator ServerCountdownCoroutine()
        {
            float remainingTime = prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;
                int newFloorTime = Mathf.FloorToInt(remainingTime);

                if (newFloorTime != floorTime)
                {//to avoid flooding the network of message, we only send a notice to client when the number of plain seconds change.
                    floorTime = newFloorTime;

                    for (int i = 0; i < lobbySlots.Length; ++i)
                    {
                        if (lobbySlots[i] != null)
                        {//there is maxPlayer slots, so some could be == null, need to test it before accessing!
                            (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(floorTime);
                        }
                    }
                }
            }

            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                if (lobbySlots[i] != null)
                {
                    (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(0);
                }
            }

            ServerChangeScene(playScene);
        }

        // ----------------- Client callbacks ------------------

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            infoPanel.gameObject.SetActive(false);

            conn.RegisterHandler(MsgKicked, KickedMessageHandler);

			if (localState == "client")
            {//only to do on pure client (not self hosting client)
				Debug.Log("Client connected");
				ChangeTo(lobbyPanel);
            }
        }

		public override void OnClientDisconnect(NetworkConnection conn)
		{
			base.OnClientDisconnect(conn);
			ChangeTo(gamesPanel);
		}

		public override void OnClientError(NetworkConnection conn, int errorCode)
		{
			ChangeTo(gamesPanel);
			infoPanel.Display("Cient error : " + (errorCode == 6 ? "timeout" : errorCode.ToString()), "Close", null);
		}
    }
}
