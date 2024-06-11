using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carstimulator{
public class usercarcontroller : MonoBehaviour
{
    public float maxSpeed = 10;   //// CAR SPEED ///////
    private birdviewcamera run; 
    public Transform[] visualWheels;  /// 4 Wheels /// 
    private float accelerationRate = 4f; // Acceleration rate
    private float decelerationRate = 2f; // deceleration Rate
    private float currentSpeed = 0f; // Current speed
    public Transform trafficcar;


    void Start(){
          run = FindObjectOfType<birdviewcamera>();     
    }
    
    void Update()
    {
    if (run != null && run.startcars){
            Accelerate();
    transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);    
      }
      wheelrotation();
       
    var distance = Mathf.Abs(trafficcar.position.z - transform.position.z);
    if(distance < 10f){
        Decelerate();
        maxSpeed = 5f;
      }
    }

      //// WHEN TRIGGER WITH BOX COLLIDER IT THE SPEED WILL GOES DOWN ////////
    // void OnTriggerEnter(Collider other)
    // {  
    //     Decelerate();
    // }

        // Accelerate the car
    private void Accelerate()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += accelerationRate * Time.deltaTime;
        }
    }
     
    private void Decelerate()
    {
        while (currentSpeed > 3)
        {
            currentSpeed -= decelerationRate * Time.deltaTime;
        }
    }

    ///// This Function : Car Wheel Rotations ////
    public void wheelrotation()
    {
        var rotationSpeed = 100f;
        foreach(Transform t in visualWheels){
            t.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }
        }
    }
}  