using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TransitioningLanes : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject laneTransitionPrefab;
        public bool LaneMaking = true;
        private InternalTimer internalTimer;
        private int waitTime = 3;
        private int waitUntil = 0;
        public void Start()
        {
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
            if (laneTransitionPrefab == null)
            {
                laneTransitionPrefab = Resources.Load<GameObject>("prefab/Crossroads");
            }
        }

        // Update is called once per frame
        public void OnTriggerStay2D(Collider2D other)
        {
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
        }
    }
}