using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LaneGenerator : MonoBehaviour
    {
        private BoxCollider2D MyRb { get; set; }

        public GameObject LanesPrefab;
        public GameObject VehiclePrefab;
        public GameObject ObjectPrefab;
        public GameObject LaneTransitionPrefab;
        public GameObject CatFoodPrefab;
        public GameObject BlockablePrefab;
        public GameObject WinningShopPrefab;

        private Vector3 LanePosition { get; set; }
        private Vector3 ObjectPosition { get; set; }
        private Vector3 ObjectRotation { get; set; }

        private bool ObjectNumbers = false;
        private int obstacleCounterLane1 = 0;
        private int obstacleCounterLane2 = 0;
        private int obstacleCounterLane3 = 0;
        private int obstacleCounterLane4 = 0;
        private int baseNumber = 0;
        private int choice = 0;
        private bool foodCreated = false;

        private InternalTimer internalTimer;

        public void Start()
        {
            // Find and assign timer
            GameObject timerObject = GameObject.Find("GlobalTimer");
            if (timerObject != null)
            {
                internalTimer = timerObject.GetComponent<InternalTimer>();
            }
            else
            {
                Debug.LogWarning("Timer object not found!");
            }

            // Load all prefabs at start so they’re never null
            
        }

        public void Update()
        {
            if (!ObjectNumbers)
            {
                obstacleCounterLane1 = Random.Range(1, 4);
                obstacleCounterLane2 = Random.Range(1, 3);
                obstacleCounterLane3 = Random.Range(1, 3);
                obstacleCounterLane4 = Random.Range(1, 4);
                ObjectNumbers = true;
            }
            if ((ObjectPrefab == null || VehiclePrefab == null || LanesPrefab == null || CatFoodPrefab == null || LaneTransitionPrefab == null || BlockablePrefab == null || WinningShopPrefab == null))
            {
                ObjectPrefab = Resources.Load<GameObject>("prefab/TrashCan");
                VehiclePrefab = Resources.Load<GameObject>("prefab/Car");
                LanesPrefab = Resources.Load<GameObject>("prefab/Lanes");
                CatFoodPrefab = Resources.Load<GameObject>("prefab/CatFood");
                LaneTransitionPrefab = Resources.Load<GameObject>("prefab/StartingLane");
                BlockablePrefab = Resources.Load<GameObject>("prefab/Blockable");
                WinningShopPrefab = Resources.Load<GameObject>("prefab/EndingLane");
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            LanePosition = new Vector3(transform.position.x + 28, transform.position.y, 0);

            if (other.gameObject.CompareTag("Player"))
            {
                if (internalTimer.laneTransition == 8 && !internalTimer.endingLane)
                {
                    LanePosition = new Vector3(transform.position.x + 26, transform.position.y, 0); 
                    GameObject newLane = Instantiate(LaneTransitionPrefab, LanePosition, Quaternion.identity);
                    internalTimer.laneTransition = 0;
                }
                else if (internalTimer.endingLane)
                {
                    LanePosition = new Vector3(transform.position.x + 28, transform.position.y - 0.51f, 0);
                    GameObject newLane = Instantiate(WinningShopPrefab, LanePosition, Quaternion.identity);
                }
                else
                {
                    GameObject newLane = Instantiate(LanesPrefab, LanePosition, Quaternion.identity);

                    // Lane 1 obstacles
                    for (int i = 0; i < obstacleCounterLane1; i++)
                    {
                        ObjectPosition = new Vector3(transform.position.x + 18 + Random.Range(baseNumber, baseNumber * 1.4f), transform.position.y + 4, -3);
                        MakeObstacles();
                        baseNumber += 4;
                    }
                    baseNumber = 0;

                    // Lane 2 cars
                    for (int i = 0; i < obstacleCounterLane2; i++)
                    {
                        ObjectPosition = new Vector3(transform.position.x + 20 + Random.Range(baseNumber, baseNumber * 1.4f), transform.position.y + 1, -3);
                        ObjectRotation = Vector3.zero;
                        MakeCar();
                        baseNumber += 4;
                    }
                    baseNumber = 0;

                    // Lane 3 cars
                    for (int i = 0; i < obstacleCounterLane3; i++)
                    {
                        ObjectPosition = new Vector3(transform.position.x - 20 - (baseNumber * 1.4f), transform.position.y - 2, -3);
                        ObjectRotation = new Vector3(0, 0, 180);
                        MakeCar();
                        baseNumber += 4;
                    }
                    baseNumber = 0;

                    // Lane 4 obstacles
                    for (int i = 0; i < obstacleCounterLane4; i++)
                    {
                        ObjectPosition = new Vector3(transform.position.x + 18 + Random.Range(baseNumber, baseNumber * 1.4f), transform.position.y - 5, -3);
                        MakeObstacles();
                        baseNumber += 4;
                    }

                    internalTimer.laneTransition += 1;
                    foodCreated = false;
                }

                internalTimer.numberOfLanes += 1;
            }
        }

        public void MakeObstacles()
        {
            choice = Random.Range(1, 401);

            if (choice <= 50 && !foodCreated)
            {
                GameObject newFood = Instantiate(CatFoodPrefab, ObjectPosition, Quaternion.identity);
                foodCreated = true;
            }
            else
            {
                choice = Random.Range(1, 401);
                if (choice <= 100)
                {
                    GameObject newObject = Instantiate(BlockablePrefab, ObjectPosition, Quaternion.identity);
                }
                else
                {
                    GameObject newObject = Instantiate(ObjectPrefab, ObjectPosition, Quaternion.identity);
                }
            }
        }

        public void MakeCar()
        {
            choice = Random.Range(1, 401);

            if (choice <= 50 && !foodCreated)
            {
                GameObject newFood = Instantiate(CatFoodPrefab, ObjectPosition, Quaternion.identity);
                foodCreated = true;
            }
            else
            {
                GameObject newObject = Instantiate(VehiclePrefab, ObjectPosition, Quaternion.identity);
                newObject.transform.Rotate(ObjectRotation); // Rotate the car itself
            }
        }
    }
}
