using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carstimulator
{
    public class s6_carcontroller : MonoBehaviour
    {
        public float maxSpeed = 10;   //// CAR SPEED ///////
        private birdviewcamera run;
        public Transform[] visualWheels;  /// 4 Wheels /// 
        private float accelerationRate = 4f; // Acceleration rate
        private float decelerationRate = 1f; // deceleration Rate
        public float currentSpeed = 0f; // Current speed
        public bool stop;
        public Transform centerOfCircle; // Center point of the circular road
        public float radius =10f; // Radius of the circular road
        public float angularSpeed = 2.0f; // Angle around the center
        public bool startagain = true;
     //   private float targetAngle = 90f; // Target angle
        public bool rotating = false;
        private float angle;
        public Animator anim;
        void Start()
        {
           // anim = GetComponent<Animator>();
            if(centerOfCircle== null)
            {
                return;
            }
            angle = 100;
            run = FindObjectOfType<birdviewcamera>();
        }
     
    
        void Update()
        {
            if (run != null && run.startcars && startagain)
            {
                    Accelerate();
                    transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            wheelrotation();
            if (rotating)
            {
                circlemovement();
            }
        }
        public void circlemovement()
        {
            anim.SetBool("Walk", true);
            angle -= angularSpeed *Time.deltaTime;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            transform.position = new Vector3(x, 0, z) + centerOfCircle.position;
            Vector3 direction = new Vector3(-Mathf.Sin(angle), 0, Mathf.Cos(angle));
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(-direction);
            }
       
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("car"))
            {
                StartCoroutine(Decelerate());
                startagain = false;
            }
            if (other.CompareTag("turn"))
            {
                rotating = true;
                startagain = false;
            }
          

        }
 


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("car"))
            {
                startagain = true;
            }

        }

        private void Accelerate()
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += accelerationRate * Time.deltaTime;
            }

        }

        private IEnumerator Decelerate()
        {
            var targetSpeed = 0;
            while (currentSpeed > targetSpeed)
            {
                currentSpeed -= decelerationRate * Time.deltaTime;

                yield return null;
            }
        }
        ///// This Function : Car Wheel Rotations ////
        public void wheelrotation()
        {
            var rotationSpeed = 100f;
            foreach (Transform t in visualWheels)
            {
                t.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }
        }
    }
}


