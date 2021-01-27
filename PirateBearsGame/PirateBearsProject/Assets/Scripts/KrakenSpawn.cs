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
    GameObject krakenTemp;

    public Transform[] spawnsKraken;

    public bool canSpawnKrekanAppear = false;

    public bool appeared = false;


    void Start()
    {

        StartCoroutine(Appear());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (appeared)
        {
            StartCoroutine(HoldTime());
            int j = Random.Range(0, spawnsKraken.Length);
            krakenTemp.gameObject.transform.position = new Vector2(spawnsKraken[j].position.x, spawnsKraken[j].position.y);
            appeared = false;
        }
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

        //appeared = true;
    }

    IEnumerator HoldTime()
    {
        yield return new WaitForSeconds(10);
    }

        IEnumerator Desapear()
    {
        ////Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        //int j = Random.Range(0, spawnsKraken.Length);
        
        //Kraken.gameObject.transform.position = spawnsKraken[j].position;
        //Kraken.gameObject.transform.rotation = spawnsKraken[j].rotation;
        //canSpwanAgain = true;
    }
}
