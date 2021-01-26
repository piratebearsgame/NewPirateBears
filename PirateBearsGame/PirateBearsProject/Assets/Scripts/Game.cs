using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public float timer = 0.0f;
    public int seconds = 0;
    public int _timeLimit = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TimeFunc();
    }

    public void TimeFunc()
    {
        if (seconds != _timeLimit)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            print(seconds);
        }
        else
        {
            Application.Quit();
            //timer = 0;
            //seconds = 0;
        }
    }
}
