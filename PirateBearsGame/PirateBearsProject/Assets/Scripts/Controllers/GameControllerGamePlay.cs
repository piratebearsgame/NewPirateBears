using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using Photon.Pun.UtilityScripts;

using System.Linq;

using UnityEngine.SceneManagement;

public class GameControllerGamePlay : MonoBehaviourPunCallbacks {

    
    public GameObject myPlayer;
    public Transform[] spawnPlayer;

    public GameObject canvasCountdow;

    public GameObject canvasGameOver;
    public GameObject canvasGameOverFinish;
    public GameObject canvasGameOverPlayScore;

    bool isGameOver = false;

    private void Start()
    {
        isGameOver = false;

        int i = Random.Range(0, spawnPlayer.Length);

        GameObject playerTemp = PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[i].position, spawnPlayer[i].rotation, 0) as GameObject;


        //Iniciando CountdownEndGame
        
                //if(playerTemp.GetComponent<PhotonView>().Owner.IsMasterClient)
                //{

                //    ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable {
                //        {CountdownEndGame.CountdownStartTime, (float) PhotonNetwork.Time }
                //    };

                //    PhotonNetwork.CurrentRoom.SetCustomProperties(props);
            
                //}

        CheckPlayers();

        if (canvasCountdow && !canvasCountdow.gameObject.activeInHierarchy)
        {
            canvasCountdow.gameObject.SetActive(true);
        }


    }

    void CheckPlayers() {

        if (PhotonNetwork.PlayerList.Length < 2)
        {
            //foreach (var item in PhotonNetwork.PlayerList) {

                //Debug.Log(item.NickName + " Vencedor!");
                GameOver();

            //}
        }

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " saiu da Partida!");

        if (isGameOver) {

            return;

        }

        CheckPlayers();
    }
    
    public void GameOver() {

        canvasGameOver.gameObject.SetActive(true);

        var dictionary = new Dictionary<string, int>();


        foreach (var item in PhotonNetwork.PlayerList)
        {
            /*
            GameObject playerScoreTemp = Instantiate(canvasGameOverPlayScore);


            playerScoreTemp.transform.SetParent(canvasGameOverFinish.transform);
            playerScoreTemp.transform.position = Vector3.zero;
            playerScoreTemp.GetComponent<PlayerScore>().SetDados(item.NickName, item.GetScore().ToString());
            */

            dictionary.Add(item.NickName, item.GetScore());

        }

        var items = from pair in dictionary
                    orderby pair.Value descending
                    select pair;


        foreach (var item in items)
        {
            GameObject playerScoreTemp = Instantiate(canvasGameOverPlayScore);


            playerScoreTemp.transform.SetParent(canvasGameOverFinish.transform);
            playerScoreTemp.transform.position = Vector3.zero;
           // playerScoreTemp.GetComponent<PlayerScore>().SetDados(item.Key, item.Value.ToString());
        }
        
        canvasCountdow.gameObject.SetActive(false);

        //Propriedades da Sala
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable {
                        {"isGameOver", true }
                    };

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        isGameOver = true;

    }



    //Botão Sair do Jogo

    public void BotaoDesconectar() {
        
        PhotonNetwork.LeaveRoom();
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Lobby");
    }


    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }
}
