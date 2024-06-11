using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carstimulator
{
    public class s3_carcontroller : MonoBehaviour
    {
        public float maxSpeed = 10;   //// CAR SPEED ///////
        private birdviewcamera run;
        public Transform[] visualWheels;  /// 4 Wheels /// 
        private float accelerationRate = 4f; // Acceleration rate
        private float decelerationRate = 1f; // deceleration Rate
        public float currentSpeed = 0f; // Current speed
     //   public Transform pedestrian;
        private Animator anim;
        public bool stop;
        public bool startagain = true;
        void Start()
        {
            run = FindObjectOfType<birdviewcamera>();
           // anim = pedestrian.GetComponent<Animator>();
        }
        void Update()
        {
            if (run != null && run.startcars && startagain)
            {
                Accelerate();
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            wheelrotation();

            // var distance_x = Mathf.Abs(pedestrian.position.x - transform.position.x);
            // var distance_z = Mathf.Abs(pedestrian.position.z - transform.position.z);

            // if(distance_x < 15){
            //     anim.SetBool("Walk", true);
            //     StartCoroutine(Decelerate());
            //   }
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
                StartCoroutine(turningleft());
            }
            if (other.CompareTag("leftturn"))
            {
                StartCoroutine(firstturnleft());
            }

        }
        private IEnumerator turningleft()
        {
            var turnangle = 270; // Turning 270 degrees left
            Quaternion initialRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, turnangle, 0));

            Debug.Log("Starting rotation coroutine");
            Debug.Log("Initial rotation: " + initialRotation.eulerAngles);
            Debug.Log("Target rotation: " + targetRotation.eulerAngles);

            // Stop the car for 20 seconds
            startagain = false;
            Debug.Log("Stopping car for 20 seconds");
            yield return new WaitForSeconds(3f);
            startagain = true;

            // Start the turn
            Debug.Log("Starting the turn");
            float time = 0f;
            float halfDuration = 1.2f;
            float fullDuration = 3.5f;

            // First part of the turn
            while (time < halfDuration)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, time / fullDuration);
                time += Time.deltaTime;
                yield return null;
            }

            startagain = false;
            yield return new WaitForSeconds(2f); // Short wait to start moving again
            startagain = true;

            // Complete the turn
            while (time < fullDuration)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, time / fullDuration);
                time += Time.deltaTime;
                yield return null;
            }

            // Ensure we end up exactly at the target rotation
            transform.rotation = targetRotation;

            Debug.Log("Completed rotation");
        }

        private IEnumerator firstturnleft()
        {
            var turnangle = -90; 
            Quaternion initialRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, turnangle, 0));
            float fullDuration = 3.5f;
            float time = 0f;
            while (time < fullDuration)
            {
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, time / fullDuration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;
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


