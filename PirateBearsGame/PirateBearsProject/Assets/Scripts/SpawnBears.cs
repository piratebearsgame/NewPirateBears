using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class SpawnBears : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<GameObject> spawnedBears = new List<GameObject>();

    public bool canSpawnBears = false;
    public bool canSpwanAgain = false;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints").ToList();
        //spawnedBears = GameObject.FindGameObjectsWithTag("Bear").ToList();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            PhotonNetwork.Instantiate("Bear", new Vector2(spawnPoints[i].gameObject.transform.position.x,
                spawnPoints[i].gameObject.transform.position.y), Quaternion.identity);
            //PhotonNetwork.Instantiate("Bear", new Vector2(0,0), Quaternion.identity);
        }

        spawnedBears = GameObject.FindGameObjectsWithTag("Bear").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        print("bears: " + spawnedBears.Count);

        if (spawnedBears.Count <= 0)
        {
            StartCoroutine(ExampleCoroutine2());
            if (canSpwanAgain == true)
            {
                ReFind();
            }
        }        




    }

    //public void SpawnNewBears()
    //{
    //    //if (spawnedBears.Count <= 0 && spawnedBears.Count <= spawnPoints.Count)
    //    //{
    //        for (int i = 0; i < spawnPoints.Count; i++)
    //        {
    //            PhotonNetwork.Instantiate("Bear", new Vector2(spawnPoints[i].gameObject.transform.position.x,
    //                spawnPoints[i].gameObject.transform.position.y), Quaternion.identity);
    //            //PhotonNetwork.Instantiate("Bear", new Vector2(0,0), Quaternion.identity);
    //        }
    //    //}
    //}

    public void ReFind()
    {
        

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            PhotonNetwork.Instantiate("Bear", new Vector2(spawnPoints[i].gameObject.transform.position.x,
                spawnPoints[i].gameObject.transform.position.y), Quaternion.identity);
            //PhotonNetwork.Instantiate("Bear", new Vector2(0,0), Quaternion.identity);
        }
        spawnedBears = GameObject.FindGameObjectsWithTag("Bear").ToList();
        canSpwanAgain = false;
        //}
    }

    IEnumerator ExampleCoroutine2()
    {
        ////Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        canSpwanAgain = true;
    }

    //public void 
}
