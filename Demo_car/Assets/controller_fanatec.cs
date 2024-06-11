using UnityEngine;
namespace carstimulator
{
    public class controller_fanatec : MonoBehaviour
    {
        private birdviewcamera run;

        void Start()
        {
            run = FindObjectOfType<birdviewcamera>();
        }

        public void Update()
        {
            // Legacy Input Manager handling
            if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Space))
            {

                run.startcars = false;

                // Print a message to the console
                Debug.Log("Space bar pressed!");
            }
        }
    }
}

