using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Owner : MonoBehaviour
    {
        // Start is called before the first frame update

        private InternalTimer internalTimer; // Reference to the script
        void Start()
        {
            // Find the GameObject that has InternalTimer
            GameObject timerObject = GameObject.Find("GlobalTimer");
            if (timerObject != null)
            {
                internalTimer = timerObject.GetComponent<InternalTimer>();
            }
            else
            {
                Debug.LogWarning("Timer object not found!");
            }
        }


        // Update is called once per frame
        void Update()
        {
            //While Running
            if (internalTimer.isRunning == true)
            {
                //Moves the Owner
                transform.position = Vector3.MoveTowards(transform.position, internalTimer.targetXvalue, (internalTimer.currentSpeed * 0.92f) * Time.deltaTime);
                //Math is set so that the human is nearly the same speed as the cat while its crouching.
            }
        }
    }
}