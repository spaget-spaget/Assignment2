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
        private Vector3 LanePosition { get; set; }
        private Vector3 ObjectPosition { get; set; }
        private Vector3 ObjectRotation { get; set; }
        private bool LaneMade = false;
        private bool ObjectNumbers = false;
        private int obstacleCounterLane1 = 0;
        private int obstacleCounterLane2 = 0;
        private int obstacleCounterLane3 = 0;
        private int obstacleCounterLane4 = 0;
        private int baseNumber = 0;

        public void Start()
        {
            
        }
        public void Update(){

        if (LanesPrefab == null)
            {
                ObjectPrefab = Resources.Load<GameObject>("prefab/TrashCan");
                VehiclePrefab = Resources.Load<GameObject>("prefab/Car");
                LanesPrefab = Resources.Load<GameObject>("prefab/Lanes");
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
        public void OnTriggerStay2D(Collider2D other)
        {
            if ((LaneMade == false) && (other.gameObject.CompareTag("Player")))
            {
                LanePosition = new Vector3(this.transform.position.x + 28, 1, 0);
                GameObject newLanes = Instantiate(LanesPrefab, LanePosition, Quaternion.identity);
                LaneMade = true;
                for (int i = 0; i < obstacleCounterLane1; i++)
                {
                    ObjectPosition = new Vector3(this.transform.position.x + 14 + (Random.Range(baseNumber, 15)), 5, -1);
                    MakeObstacles();
                    baseNumber += 4;
                }
                baseNumber = 0;
                for (int i = 0; i < obstacleCounterLane2; i++)
                {
                    ObjectPosition = new Vector3(this.transform.position.x + 14 + (Random.Range(baseNumber, 15)), 2, -1);
                    ObjectRotation = new Vector3(0, 0, 0);
                    MakeCar();
                    baseNumber += 4;
                }
                baseNumber = 0;
                for (int i = 0; i < obstacleCounterLane3; i++)
                {
                    ObjectPosition = new Vector3(this.transform.position.x - 16 - baseNumber, -1, -1);
                    ObjectRotation = new Vector3(0, 0, 180);
                    MakeCar();
                    baseNumber += 4;
                }
                baseNumber = 0;
                for (int i = 0; i < obstacleCounterLane4; i++)
                {
                    ObjectPosition = new Vector3(this.transform.position.x + 14 + (Random.Range(baseNumber, 15)), -4, -1);
                    MakeObstacles();
                    baseNumber += 4;
                }
            }
            if ((LaneMade == true) && (other.gameObject.CompareTag("ObjectDelete")))
            {   
                Destroy(gameObject);
                LaneMade = false;
            }
        }

        public void MakeObstacles()
        {
            GameObject newObstacle = Instantiate(ObjectPrefab, ObjectPosition, Quaternion.identity);
        }
        public void MakeCar()
        {
            GameObject newCar = Instantiate(VehiclePrefab, ObjectPosition, Quaternion.identity);
            newCar.transform.Rotate(ObjectRotation);
        }
    }
}