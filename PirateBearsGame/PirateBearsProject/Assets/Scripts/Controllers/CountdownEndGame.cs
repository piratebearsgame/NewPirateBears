using UnityEngine;
using UnityEngine.UI;

using ExitGames.Client.Photon;
using Photon.Pun;


    
    public class CountdownEndGame : MonoBehaviourPunCallbacks
    {
        public const string CountdownStartTime = "StartTimeEnd";

        /// <summary>
        /// OnCountdownTimerHasExpired delegate.
        /// </summary>
        //public delegate void CountdownTimerHasExpired();

        /// <summary>
        /// Called when the timer has expired.
        /// </summary>
        //public static event CountdownTimerHasExpired OnCountdownTimerHasExpired;

        private bool isTimerRunning;

        private float startTime;

        [Header("Reference to a Text component for visualizing the countdown")]
        public Text Text;

        [Header("Countdown time in seconds")]
        public float Countdown = 90.0f;

        public void Start()
        {
            if (Text == null)
            {
                Debug.LogError("Reference to 'Text' is not set. Please set a valid reference.", this);
                return;
            }
        }

        public void Update()
        {
            if (!isTimerRunning)
            {
                return;
            }

            float timer = (float)PhotonNetwork.Time - startTime;
            float countdown = Countdown - timer;

            string minutes = ((int)countdown / 60).ToString();
            string seconds = (countdown % 60).ToString("00");

            if (seconds.Equals("60"))
            {
                return;
            }

            string timeInText = minutes + ":" + seconds;

            Text.text = timeInText;

            if (timeInText.Equals("0:00"))
            {
                Debug.Log("Fim de Jogo!");

                isTimerRunning = false;

            this.GetComponent<GameControllerGamePlay>().GameOver();

            }

            /*
            Text.text = string.Format("Game starts in {0} seconds", countdown.ToString("n2"));

            if (countdown > 0.0f)
            {
                return;
            }

            isTimerRunning = false;

            Text.text = string.Empty;

            if (OnCountdownTimerHasExpired != null)
            {
                OnCountdownTimerHasExpired();
            }
            */

        }//Update

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            object startTimeFromProps;

            if (propertiesThatChanged.TryGetValue(CountdownStartTime, out startTimeFromProps))
            {
                isTimerRunning = true;
                startTime = (float)startTimeFromProps;
            }

        object isGameOver;

        if (propertiesThatChanged.TryGetValue("isGameOver", out isGameOver))
        {
            if ((bool)isGameOver)
            {
                isTimerRunning = false;
            }
        }
    }
    }



