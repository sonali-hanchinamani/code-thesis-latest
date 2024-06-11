using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace carstimulator
{
    public class crossingcarstart : MonoBehaviour
    {
        public carcontroller ct;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("TrafficCar"))
            {
                ct.startcar = true;
                Debug.Log("collide");


            }
        }
    }

}
