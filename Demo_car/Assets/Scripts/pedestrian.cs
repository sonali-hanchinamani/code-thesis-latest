using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 up= Vector3.zero,
    right= new Vector3(0,90,0),
    down =new Vector3(0,180,0),
    left = new Vector3(0,270,0),
    currentDirection= Vector3.zero;

    Vector3 nextPos, destination, direction;
    float speed= 5f;
    bool canMove=true;
    Animator anim;

    
    void Start()
    {
        currentDirection = up;
        destination = transform.position;
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
       if(canMove)
        Move();
      else
      {
        Move();
      }  
    }
    
    void FixedUpdate()
    {
    nextPos = Vector3.zero;
    }
    
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination,speed*Time.deltaTime);
      if(Input.GetKey(KeyCode.W))
        {
            nextPos = Vector3.forward;
            currentDirection=up;         
            canMove=true;
            anim.SetBool("isRunning",true);

        }      
         if(Vector3.Distance(destination,transform.position) <= 0.00001f)
        {
            transform.localEulerAngles = currentDirection;
            if(canMove)
            {
             destination = transform.position + nextPos;
             direction= nextPos;
             canMove=false;
                }
            }
         }
    }
   
