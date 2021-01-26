using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class SpawnBears : MonoBehaviour
{
    //public List<GameObject> spawnPoints = new List<GameObject>();

    //void Start()
    //{
    //    spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    for (int i = 0; i < spawnPoints.Count; i++)
    //    {
    //        PhotonNetwork.Instantiate("Bear", new Vector2(spawnPoints[i].gameObject.transform.position.x,
    //            spawnPoints[i].gameObject.transform.position.y), Quaternion.identity);
    //        //PhotonNetwork.Instantiate("Bear", new Vector2(0,0), Quaternion.identity);
    //    }
    //}
}
