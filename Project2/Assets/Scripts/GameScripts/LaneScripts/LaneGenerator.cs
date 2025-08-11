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
        public GameObject laneTransitionPrefab;
        public GameObject CatFoodPrefab;
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
        public void Update()
        {
        if (LanesPrefab == null)
            {
                ObjectPrefab = Resources.Load<GameObject>("prefab/TrashCan");
                VehiclePrefab = Resources.Load<GameObject>("prefab/Car");
                LanesPrefab = Resources.Load<GameObject>("prefab/Lanes");
                CatFoodPrefab = Resources.Load<GameObject>("prefab/CatFood");
                laneTransitionPrefab = Resources.Load<GameObject>("prefab/StartingLane");
            }
            if (ObjectNumbers == false)
            {
                obstacleCounterLane1 = Random.Range(1, 4);
                obstacleCounterLane2 = Random.Range(1, 3);
                obstacleCounterLane3 = Random.Range(1, 3);
                obstacleCounterLane4 = Random.Range(1, 4);
                ObjectNumbers = true;
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LanePosition = new Vector3(this.transform.position.x + 28, this.transform.position.y, 0);
                if (internalTimer.laneTransition == 5)
                {    
                    Instantiate(laneTransitionPrefab, LanePosition, Quaternion.identity);
                    internalTimer.laneTransition = 0;
                }
                else
                {
                    
                    GameObject newLanes = Instantiate(LanesPrefab, LanePosition, Quaternion.identity);
                    for (int i = 0; i < obstacleCounterLane1; i++)
                    {
                        ObjectPosition = new Vector3(this.transform.position.x + 18 + (Random.Range(baseNumber, baseNumber * 1.4f)), this.transform.position.y + 4, -3);
                        MakeObstacles();
                        baseNumber += 4;
                    }
                    baseNumber = 0;
                    for (int i = 0; i < obstacleCounterLane2; i++)
                    {
                        ObjectPosition = new Vector3(this.transform.position.x + 20 + (Random.Range(baseNumber, baseNumber * 1.4f)), this.transform.position.y + 1, -3);
                        ObjectRotation = new Vector3(0, 0, 0);
                        MakeCar();
                        baseNumber += 4;
                    }
                    baseNumber = 0;
                    for (int i = 0; i < obstacleCounterLane3; i++)
                    {
                        ObjectPosition = new Vector3(this.transform.position.x - 20 - (baseNumber * 1.4f), this.transform.position.y - 2, -3);
                        ObjectRotation = new Vector3(0, 0, 180);
                        MakeCar();
                        baseNumber += 4;
                    }
                    baseNumber = 0;
                    for (int i = 0; i < obstacleCounterLane4; i++)
                    {
                        ObjectPosition = new Vector3(this.transform.position.x + 18 + (Random.Range(baseNumber, baseNumber * 1.4f)), this.transform.position.y - 5, -3);
                        MakeObstacles();
                        baseNumber += 4;
                    }
                    internalTimer.laneTransition += 1;
                    foodCreated = false;
                }
            }
        }

        public void MakeObstacles()
        {
            choice = Random.Range(1, 401);
            if ((choice <= 50) && (foodCreated == false))
            {
                GameObject newObstacle = Instantiate(CatFoodPrefab, ObjectPosition, Quaternion.identity);
                foodCreated = true;
            }
            else
            {
                GameObject newObstacle = Instantiate(ObjectPrefab, ObjectPosition, Quaternion.identity);
                
            }
            
        }
        public void MakeCar()
        {
            choice = Random.Range(1, 401);
            if ((choice <= 50) && (foodCreated == false))
            {
                GameObject newObstacle = Instantiate(CatFoodPrefab, ObjectPosition, Quaternion.identity);
                foodCreated = true;
            }
            else
            {
                GameObject newCar = Instantiate(VehiclePrefab, ObjectPosition, Quaternion.identity);
                newCar.transform.Rotate(ObjectRotation);
            }
            
        }
    }
}