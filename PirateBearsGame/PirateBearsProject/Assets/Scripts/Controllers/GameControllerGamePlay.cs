using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

using Photon.Pun.UtilityScripts;

using System.Linq;

using UnityEngine.SceneManagement;

public class GameControllerGamePlay : MonoBehaviourPunCallbacks
{

    public Text pointsTxt;

    public int j;

    public int pontosVencedor = 0;

    public int team;

    public GameObject myPlayer;
    public GameObject myKraken;

    public Transform[] spawnPlayer;
    public Transform[] spawnKraken;

    public GameObject canvasCountdow;

    public GameObject canvasGameOver;
    public GameObject canvasGameOverFinish;
    public GameObject canvasGameOverPlayScore;

    public GameObject krakenInfos;

    public static GameControllerGamePlay instance;
    public static int blueScore = 0;
    public static int redScore = 0;

    bool isGameOver = false;

    public Text blueScoreText;
    public Text redScoreText;

    public static int teamAtual = 0;

    public static int krakenNum = 0;
    public int krakenMax = 10;
    public Text krakenText;

    public Text messageText;

    public static int checkTeam = 0;


    public static readonly byte RestartGameEventCode;

    private void Awake()
    {
        SetScoreText();
        SetKrakenText();
        instance = this;
    }

    


    private void Start()
    {
        isGameOver = false;

        StartCoroutine(DisplayMessage("Fight!!"));

        int i = Random.Range(0, spawnPlayer.Length);

        this.GetComponent<SpawnBears>().enabled = false;
        this.GetComponent<KrakenSpawn>().enabled = false;
        krakenInfos.SetActive(true);

        GameObject playerTemp = PhotonNetwork.Instantiate(myPlayer.name, spawnPlayer[i].position, spawnPlayer[i].rotation) as GameObject;

        

        SpawnItensMaster();


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

    public void Update()
    {
        //pointsTxt.text = myPlayer.GetComponent<PlayerController>()._bearCount.ToString();
        print("teamAtual: " + teamAtual);

        //checkPontos();
        SetScoreText();
        SetKrakenText();

        

        if (krakenNum >= 2)
        {
            GameOverKraken();
            

        }

                //if (redScore >= 3)
        //{
        //    StartCoroutine(DisplayMessage("Red Team wins"));

        //}
        //if (blueScore >= 3)
        //{
        //    StartCoroutine(DisplayMessage("Blue Team wins"));

        //}

        //foreach (var item in PhotonNetwork.PlayerList)
        //{
        //    
        //}
    }
        

    

    public void SetKrakenText()
    {
        krakenText.text = krakenNum.ToString();
    }


    void CheckPlayers()
    {

        if (PhotonNetwork.PlayerList.Length < 2)
        {
            //foreach (var item in PhotonNetwork.PlayerList) {

                //Debug.Log(item.NickName + " Vencedor!");
                GameOverKraken();

            //}
        }

    }

    void SpawnItensMaster()
    {
        foreach (var item in PhotonNetwork.PlayerList)
        {
            if (PhotonNetwork.IsMasterClient)
            {                
                this.GetComponent<SpawnBears>().enabled = true;
                this.GetComponent<KrakenSpawn>().enabled = true;

                j = Random.Range(0, spawnKraken.Length);


                //GameObject krakenTemp = PhotonNetwork.Instantiate(myKraken.name, 
                //    spawnKraken[j].position, spawnKraken[j].rotation, 0) as GameObject;

                
            }            
            //else
            //{
            //    this.GetComponent<SpawnBears>().enabled = false;
            //}
        }
    }

    public void SetScoreText()
    {
        redScoreText.text = redScore.ToString();
        blueScoreText.text = blueScore.ToString();
    }

    public void SetWinner(int myTeamAtual)
    {
        teamAtual = myTeamAtual;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " saiu da Partida!");

        if (isGameOver) {

            return;

        }

        CheckPlayers();
    }

    //public void checkPontos()
    //{
    //    if (redScore >= pontosVencedor)
    //    {
    //        GameOver();
    //    }
    //    if (blueScore >= pontosVencedor)
    //    {
    //        GameOver();
    //    }
    //}

    public void checkLife()
    {
        //foreach (var item in PhotonNetwork.PlayerList)
        //{
        //    item.
        //}
    }

    IEnumerator DisplayMessage(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(2);
        messageText.text = "";
    }

    public void GameOverKraken()
    {
       

        canvasGameOver.gameObject.SetActive(true);        

        //Propriedades da Sala
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
                        {"isGameOver", true }
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        if (teamAtual == 0)
        {
            messageText.text = "Blue Team Wins";
        }
        if (teamAtual == 1)
        {
            messageText.text = "Red Team Wins";
        }

        isGameOver = true;

    }

    public void GameOverLife()
    {


        canvasGameOver.gameObject.SetActive(true);

        //Propriedades da Sala
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
                        {"isGameOver", true }
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        messageText.text = "Em uma luta entre ursinhos piratas não existem vencedores... Sempre há um peixe maior... Ou, bem, um molusco!";

        isGameOver = true;

    }


    public override void OnJoinedRoom()
    {

    }

        //Botão Sair do Jogo

        public void BotaoDesconectar()
    {
        
        PhotonNetwork.LeaveRoom();
        
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Main");
    }


    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }
}
