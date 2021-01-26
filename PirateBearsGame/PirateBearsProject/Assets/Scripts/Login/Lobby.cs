using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Lobby : MonoBehaviour
{

    public GameObject painelLogin;
    public GameObject painellobby;

    public Text lobbyAguardar;
    public Text lobbyTimeStart;

    public InputField playerInputField;
    public string playerName;

    public Text playerStatusText;

    public GameObject lobbyBotaoCancelar;

    //public string lobbyTimeStartText = "Start Game in {0}...";

    private void Awake()
    {
        playerName = "Player" + Random.Range(1000, 10000);
        playerInputField.text = playerName;

        /*
         //PARA USAR O PHOTON SERVER HOSPEDADO NO PC, TEM QUE CRIAR O UserId ANTES. POREM, O PLAYER QUE MODIFICAR A PROPRIEDADE DA SALA, NÃO RECEBE O CALLBACK DESSA MUDANÇA, TERIA QUE VERIFICAR EM OUTRO LUGAR, COMO UPDATE
        string userId = playerName + Random.Range(1, 100000) + Random.Range(1, 100000) + Random.Range(1, 100000) + Random.Range(1, 100000) + Time.time.ToString();

        //Photon.Pun.PhotonNetwork.AuthValues = new Photon.Realtime.AuthenticationValues();
        //Photon.Pun.PhotonNetwork.AuthValues.UserId = userId;

        Photon.Pun.PhotonNetwork.AuthValues = new Photon.Realtime.AuthenticationValues(userId);
        */
    }

    void Start()
    {
        lobbyTimeStart.gameObject.SetActive(false);
        PainelLoginActive();

        playerStatusText.gameObject.SetActive(false);

        lobbyBotaoCancelar.gameObject.SetActive(true);

    }


    public void PainelLobbyActive()
    {
        painelLogin.gameObject.SetActive(false);
        painellobby.gameObject.SetActive(true);
        lobbyAguardar.text = "Procurando Oponente";

    }

    public void PainelLoginActive()
    {
        painelLogin.gameObject.SetActive(true);
        painellobby.gameObject.SetActive(false);
    }


}
