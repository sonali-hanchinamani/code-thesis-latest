using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carstimulator{
public class s2_usercarcontroller : MonoBehaviour
{
    public float maxSpeed = 10;   //// CAR SPEED ///////
    private birdviewcamera run; 
    public Transform[] visualWheels;  /// 4 Wheels /// 
    private float accelerationRate = 4f; // Acceleration rate
    private float decelerationRate = 1f; // deceleration Rate
    public float currentSpeed = 0f; // Current speed
    public Transform pedestrian;
    public AudioSource audioSource;
    private Animator anim;
    public bool stop;
    public bool stopsound = true;
    private bool insideTrigger = false;
    public bool startagain = true;
    void Start(){
          run = FindObjectOfType<birdviewcamera>();   
          anim = pedestrian.GetComponent<Animator>();
          audioSource.Stop();

        }
   private void Update()
    {
            if (run != null && run.startcars && startagain)
            {
                Accelerate();

                if (!audioSource.isPlaying && insideTrigger == false)
                {
                    audioSource.Play();
                }
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

    private void OnTriggerStay(Collider other){
       
        if (other.CompareTag("car"))
        {
        anim.SetBool("Walk", true);
        StartCoroutine(Decelerate());
         startagain= false; 
        insideTrigger = true;
        audioSource.Stop();
            }
}
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("car"))
            {
                startagain = true;
                insideTrigger = false;
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
        foreach(Transform t in visualWheels){
            t.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            }
        }
    }
}  



//     public float maxSpeed = 10;   //// CAR SPEED ///////
//     private birdviewcamera run; 
//     public Transform[] visualWheels;  /// 4 Wheels /// 
//     private float accelerationRate = 4f; // Acceleration rate
//     private float decelerationRate = 1f; // deceleration Rate
//     public float currentSpeed = 0f; // Current speed
//     public Transform pedestrian;
//     private Animator anim;
//     public bool stop;
//     void Start(){
//           run = FindObjectOfType<birdviewcamera>();   
//           anim = pedestrian.GetComponent<Animator>();  
//     }
//     void Update()
//     {
//     if (run != null && run.startcars){
//             Accelerate();
//     transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);    
//       }
//       wheelrotation();
       
//     var distance_x = Mathf.Abs(pedestrian.position.x - transform.position.x);
//     var distance_z = Mathf.Abs(pedestrian.position.z - transform.position.z);

//     if(distance_x < 15){
//         anim.SetBool("Walk", true);
//         StartCoroutine(Decelerate());
//       }
//     }


    
//     private void Accelerate()
//     {
//         if (currentSpeed < maxSpeed)
//         {
//             currentSpeed += accelerationRate * Time.deltaTime;
//         }
//     }
     
//     private IEnumerator Decelerate()
//     {
//        var targetSpeed = 0;
//        while (currentSpeed > targetSpeed)
//     {
//         currentSpeed -= decelerationRate * Time.deltaTime;
//         yield return null;
//     }
// }
//     ///// This Function : Car Wheel Rotations ////
//     public void wheelrotation()
//     {
//         var rotationSpeed = 100f;
//         foreach(Transform t in visualWheels){
//             t.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
//             }
//         }
//     }