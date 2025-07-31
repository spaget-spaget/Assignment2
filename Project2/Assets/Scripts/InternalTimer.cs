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
        private float elapsedTime = 0f;
        private bool isRunning = false;
        private bool waitASec = false;
        private float waitTime = 0f;
        public float speedIncreaseRate = 0.1f;
        public float energyMeter = 100f;
        public float penaltyAmount = 1.00f;
        public Vector3 targetXvalue;
        public float CrouchPenalty = 0f;
        private CatMovement catMovement;

        // Start is called before the first frame update
        void Start()
        {
            catMovement = GetComponent<CatMovement>();
            targetXvalue = new Vector3(xPosition, transform.position.y, transform.position.z);
            StartStopwatch();
        }

       
        void Update()
        {
            
            if (isRunning)
            {
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

            //Energy Meter Code Begins Here
            if (energyMeter <=80)
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

            if (catMovement.state == 3)
            {
                CrouchPenalty = 0.1f;
            }
            movementSpeed = currentSpeed * (penaltyAmount - CrouchPenalty);


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