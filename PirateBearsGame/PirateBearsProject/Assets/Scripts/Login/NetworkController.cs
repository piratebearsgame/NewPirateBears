using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;


public class NetworkController : MonoBehaviourPunCallbacks
{


    public byte playerRoomMax = 2;

    public Lobby _lobbyScript;

    public int team;


    public override void OnEnable()
    {
        base.OnEnable();

        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimeIsExpired;
    }


    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }


    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimeIsExpired;
    }

    void OnCountdownTimeIsExpired()
    {

        //Chamar a função a ser executada
        StartGame();
    }


    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        _lobbyScript.PainelLobbyActive();

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");

        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

        string roomName = "Room" + Random.Range(1000, 10000);

        RoomOptions roomOptions = new RoomOptions()
        {

            IsOpen = true,
            IsVisible = true,
            MaxPlayers = playerRoomMax

        };

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

       // PhotonNetwork.LocalPlayer.SetScore(0);//Zerando o Score

        //------------
        /*
        if (PhotonNetwork.CurrentRoom.PlayerCount == playerRoomMax)
        {

            foreach (var item in PhotonNetwork.PlayerList)
            {
                if (item.IsMasterClient)
                {
                    //StartGame();

                    Hashtable props = new Hashtable {
                        {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time }
                    };

                    PhotonNetwork.CurrentRoom.SetCustomProperties(props);

                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    PhotonNetwork.CurrentRoom.IsVisible = false;

                    return;
                }
            }

        }
        */

    }

    ////NOVO PARA TESTE
    //private void OnGUI()
    //{
    //    if (PhotonNetwork.InRoom)
    //    {
    //        GUI.Label(new Rect(0, 0, 500, 200), "NameRoom: " + PhotonNetwork.CurrentRoom.Name + " NumPlayersInRoom: " + PhotonNetwork.CurrentRoom.PlayerCount);
    //    }
    //}


    //TEM QUE UTILIZAR UM RPC NO LUGAR DE PROPRIEDADES DA SALA, SE FOR USAR SERVER NO PC
  
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        
        //SetTexts();
        

        if (PhotonNetwork.CurrentRoom.PlayerCount == playerRoomMax)
        {
            
            foreach (var item in PhotonNetwork.PlayerList)
            {
                
                if (item.IsMasterClient)
                {
                    //StartGame();

                    StartCoroutine(StartTimer());


                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    PhotonNetwork.CurrentRoom.IsVisible = false;

                    return;
                }
                         
            }

        }

    }//OnPlayerEnteredRoom

    
    void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected: " + cause.ToString());
        _lobbyScript.PainelLoginActive();
    }


    public void BotaoCancelar()
    {

        PhotonNetwork.Disconnect(); // print: DisconnectByClientLogic
        _lobbyScript.playerStatusText.gameObject.SetActive(false);
    }

    public void BotaoLogin()
    {

        PhotonNetwork.NickName = _lobbyScript.playerInputField.text;

        _lobbyScript.playerStatusText.gameObject.SetActive(true);

        PhotonNetwork.ConnectUsingSettings();
    }

    IEnumerator StartTimer()
    {
   
        yield return new WaitForSeconds(5f);
        
        StartGame();
    }

    public void JoinTeam(int team)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsValue("Team"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }
        else
        {
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
            {
                { "Team", team}
            };

            PhotonNetwork.SetPlayerCustomProperties(playerProps);
        }
        print("Team: " + PhotonNetwork.LocalPlayer.CustomProperties["Team"]);
    }
    //PhotonNetwork.jo
    }