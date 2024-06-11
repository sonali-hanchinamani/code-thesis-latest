using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carstimulator{
public class carcontroller : MonoBehaviour
{
    public float speed = 10;   //// CAR SPEED ///////
    public bool startcar = true; 
    private birdviewcamera run; 
    public Transform[] visualWheels;  /// 4 Wheels /// 
    public float rotationSpeed = 100f;   ///// Rotation Speed //////

    void Start(){
          run = FindObjectOfType<birdviewcamera>();     
    }
    
    void Update()
    {
    if (run != null && run.startcars && startcar){
    transform.Translate(Vector3.forward * speed * Time.deltaTime);    
      }
      wheelrotation();
    }
    //// WHEN TRIGGER WITH BOX COLLIDER IT THE SPEED WILL GOES DOWN ////////
    void OnTriggerEnter(){
        speeddown();
    }
    
    public void speeddown(){
      for(int i = 0 ; i < 8 ; i++){
        speed=(int)speed;
        this.speed -= i * Time.deltaTime;}
    }

    ///// This Function : Car Wheel Rotations ////
    public void wheelrotation(){

        foreach(Transform t in visualWheels){
            t.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
    }
  }  
   


    

  