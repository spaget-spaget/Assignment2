using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InternalTimer : MonoBehaviour
    {
        public float xPosition = 0f;
        public float movementSpeed = 1f;
        public float currentSpeed = 1f;
        public float elapsedTime = 0f;
        public bool isRunning = false;
        private bool waitASec = false;
        private float waitTime = 0f;
        public float speedIncreaseRate = 0.1f;
        public float energyMeter = 100f;
        public float penaltyAmount = 1.00f;
        public Vector3 targetXvalue;
        public float CrouchPenalty = 0f;
        public float hitPenalty = 0f;
        public int laneTransition = 0;
        private CatMovement catMovement;
        private CollisionDetectorScript collisionDetectorScript;


        // Start is called before the first frame update
        void Start()
        {
            // Find the GameObject that has InternalTimer
            GameObject playerObject = GameObject.Find("PlayerCat"); // change to actual name
            if (playerObject != null)
            {
                catMovement = playerObject.GetComponent<CatMovement>();
                collisionDetectorScript = playerObject.GetComponent<CollisionDetectorScript>();
            }
            else
            {
                Debug.LogWarning("Player object not found!");
            }
            
        }
        public void Awake()
        {
            StartStopwatch();
        }
       
        void Update()
        {
            
            if (isRunning)
            {
                targetXvalue = new Vector3(xPosition, catMovement.yPosition, transform.position.z);
                elapsedTime += Time.deltaTime;

                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                if (energyMeter > 0f)
                {
                    energyMeter -= 2f * Time.deltaTime; // 2 units per second
                    energyMeter = Mathf.Max(energyMeter, 0f); // prevent going below 0
                }
            }

            if ((Mathf.FloorToInt(elapsedTime % 10) == 0) && (elapsedTime > 0) && (waitASec == false))
            {
                currentSpeed += speedIncreaseRate;
                waitASec = true;
                waitTime = elapsedTime + 1f;
            }
            if ((waitASec == true) && (elapsedTime >= waitTime))
            {
                waitASec = false;
            }

            if (this == null || gameObject == null)
            {
                StopStopwatch();
                return;
            }
            if (catMovement.state == 3)
            {
                CrouchPenalty = 0.1f;
            }
            else
            {
                CrouchPenalty = 0f;
            }

            //Energy Meter Code Begins Here
            if (energyMeter <= 80)
            {
                penaltyAmount = 0.95f;
            }
            if (energyMeter <= 50)
            {
                penaltyAmount = 0.90f;
            }
            if (energyMeter <= 30)
            {
                penaltyAmount = 0.85f;
            }
            if (energyMeter <= 10)
            {
                penaltyAmount = 0.80f;
            }
            else if (energyMeter >= 100)
            {
                penaltyAmount = 1.00f;
            }

            
            movementSpeed = currentSpeed * (penaltyAmount - CrouchPenalty - hitPenalty);


        }
        /*Clock Code Begins Here*/
        public void StopStopwatch()
        {
            isRunning = false;
        }

        public void StartStopwatch()
        {
            isRunning = true;
        }

        public void ResetStopwatch()
        {
            elapsedTime = 0f;
        }
    }
}