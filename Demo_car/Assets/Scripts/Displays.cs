using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displays : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("displays connected: " + Display.displays.Length);
       
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        if(Display.displays.Length > 1) {
            camera1.targetDisplay = 0;
            camera2.targetDisplay = 1;

            camera3.targetDisplay = 2; 
        }
        
    }
}

 
