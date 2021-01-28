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
    public static KrakenSpawn instance;

    public GameObject Kraken;
    GameObject krakenTemp;

   

    public Transform[] spawnsKraken;

    public bool canSpawnKrekanAppear = false;

    public bool appeared = false;

    

    void Start()
    {
        //krakenInfos.SetActive(false);
        StartCoroutine(Appear());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if ((Input.GetKey(KeyCode.T)))            
        //{
        //    krakenNum++;
        //}
    }

    //public void KrakenAppear()
    //{
        
    //    int i = Random.Range(0, spawnsKraken.Length);
    //    GameObject myKraken = PhotonNetwork.Instantiate(Kraken.name, spawnsKraken[i].position, spawnsKraken[i].rotation, 0) as GameObject;
    //    //StartCoroutine(Desapear());
    //    //Destroy(myKraken.gameObject);


    //}

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(15);

        //int i = Random.Range(0, spawnsKraken.Length);
        


        krakenTemp = PhotonNetwork.Instantiate(Kraken.name,
        this.gameObject.GetComponent<GameControllerGamePlay>().spawnKraken[this.gameObject.GetComponent<GameControllerGamePlay>().j].position, 
        this.gameObject.GetComponent<GameControllerGamePlay>().spawnKraken[this.gameObject.GetComponent<GameControllerGamePlay>().j].rotation, 0)
        as GameObject;
        

        appeared = true;
    }    

    

    //[PunRPC]
    //void SpawnAnimColide()
    //{

    //}
}
