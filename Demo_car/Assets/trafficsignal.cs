using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carstimulator
{
    public class trafficsignal : MonoBehaviour
    {
        public GameObject greenlight;
        public GameObject redlight;
        public BoxCollider ob;
        private bool timerRunning = false;
        public s2_usercarcontroller playercar;

        public void Start()
        {
            playercar = FindObjectOfType<s2_usercarcontroller>();

            ob = GetComponent<BoxCollider>();
        }

        void OnTriggerStay(Collider collider)
        {
            if (collider.CompareTag("TrafficCar") && !timerRunning)
            {
                StartCoroutine(TimerCoroutine());

            }
        }

        private IEnumerator TimerCoroutine()
        {
            timerRunning = true;

            playercar.startagain = false;
            float elapsedTime = 0f;

            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            ob.enabled = false;
            playercar.currentSpeed = 0;
            OnTriggerExitLogic();
            timerRunning = false;
        }

        private void OnTriggerExitLogic()
        {
            redlight.SetActive(false);
            greenlight.SetActive(true);
            playercar.startagain = true;
        }
    }
}
