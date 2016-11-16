using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
    public class LobbyServerList : MonoBehaviour
    {
        public LobbyManager lobbyManager;

		public Text playerName, statistics;
		public Image playerColor;

        public RectTransform serverListRect;
        public GameObject serverEntryPrefab;
		public Text message;

        protected int currentPage = 0;
		protected bool isUpdating = false;

        static Color OddServerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        static Color EvenServerColor = new Color(.94f, .94f, .94f, 1.0f);

        void OnEnable()
        {
			lobbyManager.StartMatchMaker();

			playerName.text = SaveSystem.player.name;
			statistics.text = "K: " + SaveSystem.player.k + " / " +
			"D: " + SaveSystem.player.d + " / " +
			"HI-SCORE: " + SaveSystem.player.hiscore + " / " +
			"TRAINING SCORE: " + SaveSystem.player.rank;
			playerColor.color = SaveSystem.player.GetColor ();

			//Define a pagina atual como 0
			currentPage = 0;

			Debug.Log ("oi");

			//Destroi as salas dentro da lista de servidores
            foreach (Transform t in serverListRect)
                Destroy(t.gameObject);

			//Desativa a mensagem negativa
			message.text = "SEARCHING FOR GAMES...";

//			//Inicia a atualizacao da lista
//            RequestPage(0);
        }

		void Update () {
			if (!isUpdating) {
				RequestPage (0);
			}
		}

		public void UpdateServerList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
		{
			//Se não existem partidas termina o loop
			if (matches.Count == 0)
			{
                //Se a pagina atual é a primeira DELETA instancias antigas e ativa a mensagem negativa
				if (currentPage == 0)
                {
					foreach (Transform t in serverListRect)
						Destroy(t.gameObject);
					
					message.text = "NO GAMES FOUND";
                }

				//Reinicia a contagem de paginas
                currentPage = 0;
				isUpdating = false;

                return;
            }

			//Desativa a mensagem negativa
			message.text = "";

            //Se for a primeira pagina destroi todas salas da lista
			if(currentPage == 0) {
				foreach (Transform t in serverListRect)
					Destroy(t.gameObject);
			}
			//Cria as salas encontradas na lista
			for (int i = 0; i < matches.Count; ++i)
			{
                GameObject o = Instantiate(serverEntryPrefab) as GameObject;

				o.GetComponent<LobbyServerEntry>().Populate(matches[i], lobbyManager, (i % 2 == 0) ? OddServerColor : EvenServerColor);

				o.transform.SetParent(serverListRect, false);
            }
			//Pede informações da proxima pagina
			currentPage++;
			RequestPage (currentPage);
        }

//        public void ChangePage(int dir)
//        {
//            int newPage = Mathf.Max(0, currentPage + dir);
//
//            //if we have no server currently displayed, need we need to refresh page0 first instead of trying to fetch any other page
//            if (noServerFound.activeSelf)
//                newPage = 0;
//
//            RequestPage(newPage);
//        }

        public void RequestPage(int page)
        {
			isUpdating = true;
			lobbyManager.matchMaker.ListMatches(page, 10, "", true, 0, 0, UpdateServerList);
		}
    }
}