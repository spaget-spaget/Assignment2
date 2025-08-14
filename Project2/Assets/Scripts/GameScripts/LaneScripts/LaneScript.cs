using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LaneScript : MonoBehaviour
    {
        private LaneGenerator laneGenerator;
        private InternalTimer internalTimer;

        private int baseNumber = 0;
        private int choice = 0;
        private bool foodCreated = false;
        // Start is called before the first frame update
        void Awake()
        {
            // Find the Global Timer Object that has the InternalTimer script
            GameObject timerObject = GameObject.Find("GlobalTimer");
            if (timerObject != null)
            {
                laneGenerator = timerObject.GetComponent<LaneGenerator>();
                internalTimer = timerObject.GetComponent<InternalTimer>();
            }
            else
            {
                Debug.LogWarning("Timer object not found!");
            }
            // initial calculation of y value
        }

        // Update is called once per frame
        private void OnTriggerEnter2D(Collider2D other)
        {
            laneGenerator.lanePosition = new Vector3(transform.position.x + 28, transform.position.y, 0);

            if (other.gameObject.CompareTag("Player"))
            {
                // Looping lane
                if (internalTimer.laneTransition == 8 && !internalTimer.endingLane)
                {
                    laneGenerator.lanePosition = new Vector3(transform.position.x + 26, transform.position.y, 0);
                    Instantiate(laneGenerator.laneTransitionPrefab, laneGenerator.lanePosition, Quaternion.identity);
                    internalTimer.laneTransition = 0;
                }
                // Ending lane
                else if (internalTimer.endingLane)
                {
                    laneGenerator.lanePosition = new Vector3(transform.position.x + 28, transform.position.y - 0.51f, 0);
                    Instantiate(laneGenerator.winningShopPrefab, laneGenerator.lanePosition, Quaternion.identity);
                }
                // Standard lane
                else if (internalTimer.laneTransition != 8 && !internalTimer.endingLane)
                {
                    Instantiate(laneGenerator.lanesPrefab, laneGenerator.lanePosition, Quaternion.identity);

                    // Lane 1 obstacles
                    for (int i = 0; i < laneGenerator.obstacleCounterLane1; i++)
                    {
                        laneGenerator.objectPosition = new Vector3(transform.position.x + 18 + Random.Range(baseNumber, baseNumber * 1.4f), transform.position.y + 4, -3);
                        MakeObstacles();
                        baseNumber += 4;
                    }
                    baseNumber = 0;

                    // Lane 2 cars
                    for (int i = 0; i < laneGenerator.obstacleCounterLane2; i++)
                    {
                        laneGenerator.objectPosition = new Vector3(transform.position.x + 20 + Random.Range(baseNumber, baseNumber * 1.4f), transform.position.y + 1, -3);
                        laneGenerator.objectRotation = Vector3.zero;
                        MakeCar();
                        baseNumber += 4;
                    }
                    baseNumber = 0;

                    // Lane 3 cars
                    for (int i = 0; i < laneGenerator.obstacleCounterLane3; i++)
                    {
                        laneGenerator.objectPosition = new Vector3(transform.position.x - 20 - (baseNumber * 1.4f), transform.position.y - 2, -3);
                        laneGenerator.objectRotation = new Vector3(0, 0, 180);
                        MakeCar();
                        baseNumber += 4;
                    }
                    baseNumber = 0;

                    // Lane 4 obstacles
                    for (int i = 0; i < laneGenerator.obstacleCounterLane4; i++)
                    {
                        laneGenerator.objectPosition = new Vector3(transform.position.x + 18 + Random.Range(baseNumber, baseNumber * 1.4f), transform.position.y - 5, -3);
                        MakeObstacles();
                        baseNumber += 4;
                    }

                    
                }

                internalTimer.numberOfLanes += 1;
                laneGenerator.objectNumbersGenerated = false;
                internalTimer.laneTransition += 1;
                laneGenerator.foodCreated = false;
            }
        }

        private void MakeObstacles()
        {
            choice = Random.Range(1, 401);

            if (choice <= 50 && !foodCreated)
            {
                Instantiate(laneGenerator.catFoodPrefab, laneGenerator.objectPosition, Quaternion.identity);
                laneGenerator.foodCreated = true;
            }
            else
            {
                choice = Random.Range(1, 401);
                if (choice <= 100)
                {
                    Instantiate(laneGenerator.blockablePrefab, laneGenerator.objectPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(laneGenerator.objectPrefab, laneGenerator.objectPosition, Quaternion.identity);
                }
            }
        }

        private void MakeCar()
        {
            choice = Random.Range(1, 401);

            if (choice <= 50 && !foodCreated)
            {
                Instantiate(laneGenerator.catFoodPrefab, laneGenerator.objectPosition, Quaternion.identity);
                foodCreated = true;
            }
            else
            {
                GameObject newCar = Instantiate(laneGenerator.vehiclePrefab, laneGenerator.objectPosition, Quaternion.identity);
                newCar.transform.Rotate(laneGenerator.objectRotation);
            }
        }
    }
}