using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<GameObject> spawnPoints = new List<GameObject>();

    public InputField playerNameInput;
    public InputField roomName;
    string playerNameTemp;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        playerNameTemp = "Player" + Random.Range(1000, 10000);
        playerNameInput.text = playerNameTemp;

        roomName.text = "Room" + Random.Range(1000, 10000);

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();

        
    }

    public void Login()
    {
        if (playerNameInput.text != "")        
            PhotonNetwork.NickName = playerNameInput.text;        
        else        
            PhotonNetwork.NickName = playerNameTemp;

        PhotonNetwork.ConnectUsingSettings();

       
    }

    public void BotaoBuscarPartidaRapida()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        //print("OnConected");
    }

    public void BotaoCriarSala()
    {
        string roomNameTemp = roomName.text;
        PhotonNetwork.JoinOrCreateRoom(roomNameTemp, new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        print("OnConected");
        //PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 2}, TypedLobby.Default);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomTemp = "Room" + Random.Range(1000, 10000);
        PhotonNetwork.CreateRoom(roomTemp);
    }

    public override void OnJoinedRoom()
    {
        //print("Joined");        

        //spawn kraken
        //PhotonNetwork.Instantiate("Kraken", new Vector2(0.0f, 0.0f), Quaternion.identity);

        //spawn player
        PhotonNetwork.Instantiate("Player", new Vector2(4.0f, 0.0f), Quaternion.identity);

        //PhotonNetwork.Instantiate("Text", new Vector2(0.0f, 0.0f), Quaternion.identity);

        //// spawn ilhas
        //for (int i = 0; i < 4; i++)
        //{
        //    PhotonNetwork.Instantiate("Bear", new Vector2(
        //    Random.Range(-8f, 11f), transform.position.y + i), Quaternion.identity);
        //}

        //for (int i = 0; i < spawnPoints.Count; i++)
        //{
        //    //PhotonNetwork.Instantiate("Bear", new Vector2(spawnPoints[i].gameObject.transform.position.x,
        //    //    spawnPoints[i].gameObject.transform.position.y), Quaternion.identity);
        //    PhotonNetwork.Instantiate("Bear", new Vector2(0,0), Quaternion.identity);
        //}

        foreach (var item in PhotonNetwork.PlayerList)
        {
            Hashtable playerCustom = new Hashtable();
            //playerCustom.Add("lives", 0);
            playerCustom.Add("Score", 0);

            item.SetCustomProperties(playerCustom, null, null);

            //item.SetScore(0);
        }
    }

}