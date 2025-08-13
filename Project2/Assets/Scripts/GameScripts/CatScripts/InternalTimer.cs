using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InternalTimer : MonoBehaviour
    {
        //Variables. Although it looks like alot, this is just because the variables needed for other scripts are put here for convenience and easy access
        //Cat Movement Variables
        private CatMovement catMovement;
        public float xPosition = 1000f;
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

        //Lane Generator Variables
        public int laneTransition = 0;
        public int numberOfLanes = 0;
        public int numberOfLanesNeeded = 100;
        public bool endingLane = false;
        
        private CollisionDetectorScript collisionDetectorScript;
        public AudioSource backgroundMusic;

        // Start is called before the first frame update
        void Start()
        {
            // Find player cat
            GameObject playerObject = GameObject.Find("PlayerCat");
            if (playerObject != null)
            {
                catMovement = playerObject.GetComponent<CatMovement>();
                collisionDetectorScript = playerObject.GetComponent<CollisionDetectorScript>();
            }
            else
            {
                Debug.LogWarning("Player object not found!");
            }
            //Resets numberOfLanes to 0
            numberOfLanes = 0;
        }
        public void Awake()
        {
            //Starts timer
            StartStopwatch();
        }
       
        void Update()
        {
            //If Running
            if (isRunning)
            {
                //Move cat by giving it a new target location
                targetXvalue = new Vector3(xPosition, catMovement.yPosition, transform.position.z);
                //Timer Calculations
                elapsedTime += Time.deltaTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                //Energy Meter code
                if (energyMeter > 0f)
                {
                    energyMeter -= 2f * Time.deltaTime; // 2 units per second
                    energyMeter = Mathf.Max(energyMeter, 0f); // prevent going below 0
                }
                //Background Music
                if (backgroundMusic.isPlaying == false)
                {
                    backgroundMusic.Play();
                }
            }
            if (!isRunning)
            {
                backgroundMusic.Stop();
            }
            //Every 10 seconds, increase speed. (WaitASec value is so that it only increases one time, else it increases by the number of frames that pass)
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
            //Crouch Penalty
            if (catMovement.state == 3)
            {
                CrouchPenalty = 0.1f;
            }
            else
            {
                CrouchPenalty = 0f;
            }

            //Energy Meter Code penalties
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

            //Lane Generator to generate fish shop
            if (numberOfLanes == numberOfLanesNeeded)
            {
                endingLane = true;
            }

            //Final Speed calculation
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