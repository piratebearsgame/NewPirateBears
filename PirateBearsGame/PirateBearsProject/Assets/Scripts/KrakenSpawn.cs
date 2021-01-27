using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

using Photon.Pun.UtilityScripts;

using System.Linq;

using UnityEngine.SceneManagement;

public class KrakenSpawn : MonoBehaviour
{
    public GameObject Kraken;

    public Transform[] spawnsKraken;

    public bool canSpawnKrekanAppear = false;
    
    void Start()
    {
        int i = Random.Range(0, spawnsKraken.Length);


        GameObject krakenTemp = PhotonNetwork.Instantiate(Kraken.name, spawnsKraken[i].position, spawnsKraken[i].rotation, 0) as GameObject;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
