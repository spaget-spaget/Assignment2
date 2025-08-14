using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LaneGenerator : MonoBehaviour
    {
        private BoxCollider2D myRb;

        // Prefabs (assign in Inspector or leave empty to auto-load from Resources)
        [SerializeField] public GameObject lanesPrefab;
        [SerializeField] public GameObject vehiclePrefab;
        [SerializeField] public GameObject objectPrefab;
        [SerializeField] public GameObject laneTransitionPrefab;
        [SerializeField] public GameObject catFoodPrefab;
        [SerializeField] public GameObject blockablePrefab;
        [SerializeField] public GameObject winningShopPrefab;

        // Positions
        public Vector3 lanePosition;
        public Vector3 objectPosition;
        public Vector3 objectRotation;

        // Variables
        public bool objectNumbersGenerated = false;
        public int obstacleCounterLane1 = 0;
        public int obstacleCounterLane2 = 0;
        public int obstacleCounterLane3 = 0;
        public int obstacleCounterLane4 = 0;
        public int baseNumber = 0;
        public int choice = 0;
        public bool foodCreated = false;

        private InternalTimer internalTimer;

        private void Start()
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

            // Load prefabs from Resources only if not assigned in Inspector
            if (objectPrefab == null) objectPrefab = Resources.Load<GameObject>("prefab/TrashCan");
            if (vehiclePrefab == null) vehiclePrefab = Resources.Load<GameObject>("prefab/Car");
            if (lanesPrefab == null) lanesPrefab = Resources.Load<GameObject>("prefab/Lanes");
            if (catFoodPrefab == null) catFoodPrefab = Resources.Load<GameObject>("prefab/CatFood");
            if (laneTransitionPrefab == null) laneTransitionPrefab = Resources.Load<GameObject>("prefab/StartingLane");
            if (blockablePrefab == null) blockablePrefab = Resources.Load<GameObject>("prefab/Blockable");
            if (winningShopPrefab == null) winningShopPrefab = Resources.Load<GameObject>("prefab/EndingLane");
        }

        private void Update()
        {
            // Generate random numbers for each lane if not already generated
            if (!objectNumbersGenerated)
            {
                obstacleCounterLane1 = Random.Range(1, 4);
                obstacleCounterLane2 = Random.Range(1, 3);
                obstacleCounterLane3 = Random.Range(1, 3);
                obstacleCounterLane4 = Random.Range(1, 4);
                objectNumbersGenerated = true;
            }
        }
    }
}
