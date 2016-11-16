using UnityEngine;
using System.Collections; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveSystem {

	public static Game current = new Game();


	public static Player player = new Player();
	//public static bool saved = false;

	public static void Save()
	{
		//SaveSystem.save = Game.current; //Adiciona as informações atuais no save do jogo
		BinaryFormatter bf = new BinaryFormatter(); //Cria o BinaryFormatter que fará a serialização
		FileStream file = File.Create (Application.persistentDataPath + "/pData.tlc"); //Cria o arquivo no local especifico para a plataforma
		bf.Serialize(file, SaveSystem.player); //Guarda o save atual no arquivo criado acima
		file.Close(); //Fecha o arquivo criado
	}

	public static void Load()
	{
		//Verifica a existencia do save
		if(File.Exists(Application.persistentDataPath + "/pData.tlc")){
			BinaryFormatter bf = new BinaryFormatter(); //Cria o BinaryFormatter que fará a serialização
			FileStream file = File.Open (Application.persistentDataPath + "/pData.tlc", FileMode.Open); //Abre o arquivo que guarda o save
			SaveSystem.player = (Player)bf.Deserialize(file); //Deserializa o arquivo e o converte para o tipo Game, carregando o último save no current
			file.Close(); //Fecha o arquivo criado
		}
	}

	public static void Erase()
	{
		player = new Player ();
		Save ();
	}
}
