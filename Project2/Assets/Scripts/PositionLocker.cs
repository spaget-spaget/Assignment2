using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PositionLocker : MonoBehaviour
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

        void Update()
        {
            Vector3 targetXValue = Vector3.MoveTowards(transform.position,internalTimer.targetXvalue,internalTimer.movementSpeed * Time.deltaTime);

            
            transform.position = new Vector3(targetXValue.x, -0.05f, -10f);
        }
    }
}