using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    public float _playerSpeed;
    public float _playerRotSpeed;
    public float TimeToLive = 4f;
    float angle;

    public Text gameOverTxt;

    public int _bearCount = 0;

    public Transform barrelTip;
    public GameObject bullet;

    [Header("Health")]
    public float playerHealthMax = 100f;
    public float playerHealthCurrent;
    public Image playerHealthFill;

    Transform _playerTransform;
    Vector3 _playerPos;
    Vector3 _playerRot;

    public GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        _playerTransform = transform;
        _playerPos = _playerTransform.position;
        _playerRot = _playerTransform.rotation.eulerAngles;

        HealthManager(playerHealthMax);

        gameManager = GameObject.FindWithTag("GameManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D)))
        {
            _playerRot.z -= _playerRotSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A)))
        {
            _playerRot.z += _playerRotSpeed;
        }

        angle = _playerTransform.eulerAngles.magnitude * Mathf.Deg2Rad;

        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W)))
        {


            if (transform.position.x < -6.0f)
            {
                _playerPos.x = -6.0f;
            }
            else if (transform.position.x > 6.0f)
            {
                _playerPos.x = 6.0f;
            }
            else if (transform.position.y > 2f)
            {
                _playerPos.y = 2.0f;
            }
            else if (transform.position.y < -2f)
            {
                _playerPos.y = -2.0f;
            }
            else
            {                
                _playerPos.x += (Mathf.Cos(angle) * _playerSpeed) * Time.deltaTime;
                _playerPos.y += (Mathf.Sin(angle) * _playerSpeed) * Time.deltaTime;
            }


        }



        shooting();        

        //if (Input.GetKey(KeyCode.DownArrow) ||
        //   (Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.LeftShift)))
        //    _playerSpeed = 1;
        //else
        //    _playerSpeed = 3;

        _playerTransform.position = _playerPos;
        _playerTransform.rotation = Quaternion.Euler(_playerRot);

        //print("x: " + transform.position.x);
        //print("y: " + transform.position.y);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    //[PunRPC]
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.tag == "Bear")
        {                
                Destroy(col.gameObject);
        }
        //if (col.gameObject.tag == "Kraken")
        //{
        //    if (_bearCount >= 0)
        //    {
        //        _bearCount -= 1;
        //        _playerSpeed += 1f;
        //    }
        //}
    }

    

   

    [PunRPC]
    void shooting()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    photonView.RPC("FireBullet", RpcTarget.All);        
        //} 
        if (Input.GetMouseButtonDown(0))
        {
            PhotonNetwork.Instantiate(bullet.name, barrelTip.transform.position, barrelTip.transform.rotation, 0);
        }
    }

    [PunRPC]
    private void FireBullet()
    {
        GameObject fireBullet = Instantiate(bullet, barrelTip.position, barrelTip.rotation);
        fireBullet.GetComponent<Rigidbody2D>().velocity = barrelTip.up * 10f;
    }

    public void TakeDamage(float value/*, Player playerTemp*/)
    {       

        photonView.RPC("TakeDamageNetwork", RpcTarget.AllBuffered, value/*, playerTemp*/);

        //foreach (var item in PhotonNetwork.PlayerList)
        //{
        //    object playerScoreTempGet;
        //    item.CustomProperties.TryGetValue("Score", out playerScoreTempGet);

        //    Debug.Log("Player Name: " + item.NickName + " | Score: " + playerScoreTempGet.ToString());
        //}
    }

    [PunRPC]
    public void TakeDamageNetwork(float value/*, Player playerTemp*/)
    {
        HealthManager(value);

        //object playerScoreTempGet;
        //playerTemp.CustomProperties.TryGetValue("Score", out playerScoreTempGet);

        //int soma = (int)playerScoreTempGet;
        //soma += 10;

        //ExitGames.Client.Photon.Hashtable playerHashtableTemp = new ExitGames.Client.Photon.Hashtable();
        //playerHashtableTemp.Add("Score", soma);

        //playerTemp.SetCustomProperties(playerHashtableTemp, null, null);

        //if (playerHealthCurrent <= 0 && photonView.IsMine)
        //{
        //    photonView.RPC("IsGameOver", RpcTarget.MasterClient);
        //    gameManager.GetComponent<GameControllerGamePlay>().GameOver();
        //}
    }

    [PunRPC]
    void IsGameOver()
    {
        if (photonView.Owner.IsMasterClient)
        {
            Debug.Log("GameOver");
            //print("Player ID: " + GetComponent<Collider>().GetComponent<PhotonView>().Owner.ActorNumber + "player.name" + GetComponent<Collider>().GetComponent<PhotonView>().Owner.NickName);
            
        

        }
    }

    public void HealthManager(float value)
    {
        playerHealthCurrent += value;
        playerHealthFill.fillAmount = playerHealthCurrent / 100f;
    }
}
