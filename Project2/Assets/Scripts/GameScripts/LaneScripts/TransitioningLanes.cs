using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TransitioningLanes : MonoBehaviour
    {
        //Variables are declared
        public GameObject laneTransitionPrefab;
        public bool LaneMaking = true;
        private InternalTimer internalTimer;
        private int waitTime = 3;
        private int waitUntil = 0;
        public void Start()
        {
            GameObject timerObject = GameObject.Find("GlobalTimer"); // Find InternalTimer
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
            //Prefabs are loaded
            if (laneTransitionPrefab == null)
            {
                laneTransitionPrefab = Resources.Load<GameObject>("prefab/Crossroads");
            }
        }

        // Update is called once per frame
        public void OnTriggerStay2D(Collider2D other)
        {

            //Makes the crossroads lane
            if ((other.gameObject.CompareTag("Player")) && (LaneMaking == true))
            {
                {
                    waitUntil = (Mathf.FloorToInt(internalTimer.elapsedTime)) + waitTime;
                    LaneMaking = false;
                    
                    Vector3 spawnPos = new Vector3(this.transform.position.x + 28, -6.05f, 0);
                    Instantiate(laneTransitionPrefab, spawnPos, Quaternion.identity);
                    
                }
            }
            if ((LaneMaking == false) && (internalTimer.elapsedTime >= waitUntil))
            {
                LaneMaking = true;
            }
        }//Why OnTriggerStay2D rather than OnTriggerEnter2D? just in case the movements somehow dont get detected.
    }
}