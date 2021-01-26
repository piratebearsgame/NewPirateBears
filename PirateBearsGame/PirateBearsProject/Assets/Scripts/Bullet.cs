using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject playerObj;
    PlayerController player;
    private object photonView;

    public AudioClip impact;
    AudioSource audioSource;

    Rigidbody2D rigidB2D;

    void Start()
    {
        rigidB2D = GetComponent<Rigidbody2D>();

        rigidB2D.AddForce(transform.up * 200f, ForceMode2D.Force);

        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerController>();

        audioSource = GetComponent<AudioSource>();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Other")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);

        }
        //if (collision.gameObject.tag == "Player")
        //{
        //    player.TakeDamage(-10f);
        //    GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //    Destroy(effect, 5f);
        //    Destroy(gameObject);

        //}
    }

    [PunRPC]
    void BulletDestroy()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    void SpawnAnimColide()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }

    [PunRPC]
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Limitwall")
        {
            Destroy(this.gameObject);
        }

        if (col.CompareTag("Player") && col.GetComponent<PlayerController>() &&
            col.GetComponent<PhotonView>().IsMine)
        {
            col.GetComponent<PlayerController>().TakeDamage(-10f/*, GetComponent<PhotonView>().Owner*/);
            this.GetComponent<PhotonView>().RPC("SpawnAnimColide", RpcTarget.AllViaServer);
            this.GetComponent<PhotonView>().RPC("BulletDestroy", RpcTarget.AllViaServer);
            audioSource.PlayOneShot(impact, 0.7F);

        }

        if (col.CompareTag("Other"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            this.GetComponent<PhotonView>().RPC("BulletDestroy", RpcTarget.AllViaServer);
            print("Aqui");
        }
    }
}