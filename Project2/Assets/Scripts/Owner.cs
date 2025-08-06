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
            GameObject timerObject = GameObject.Find("GlobalTimer"); // change to actual name
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
            transform.position = Vector3.MoveTowards(transform.position, internalTimer.targetXvalue, (internalTimer.currentSpeed * 0.90f) * Time.deltaTime);
        }
    }
}