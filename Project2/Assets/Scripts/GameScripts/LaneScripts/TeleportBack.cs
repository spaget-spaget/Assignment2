using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TeleportBack : MonoBehaviour
    {
        //Variables
        public GameObject laneTransitionPrefab;
        private CatMovement playerCatScript;
        private float returnXValue = -28;
        private Vector3 lanePosition = new Vector3(-28, -6.05f, 0);
        private Vector3 OwnerPosition = new Vector3(0, 0, 0);
        private GameObject Owner;

        void Start()
        {
            //Find human owner (The one who acts as a kill wall)
            Owner = GameObject.Find("HumanOwner");
            GameObject playerCatObject = GameObject.Find("PlayerCat");
            if (playerCatObject != null)
            {
                playerCatScript = playerCatObject.GetComponent<CatMovement>();
            }
            else
            {
                Debug.LogWarning("PlayerCat object not found!");
            }
            //Find lane position
            lanePosition = new Vector3(returnXValue, (playerCatScript.bottomLanePosition + 4.95f), 0f);

        }
        void Update()
        {
            if (laneTransitionPrefab == null)
            {
                laneTransitionPrefab = Resources.Load<GameObject>("prefab/StarterLane");
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
                Vector3 playerPos = other.gameObject.transform.position; // Find Player current pos
                float difference = (returnXValue - playerPos.x); // Difference between New Pos and Old Pos
                playerPos.x = returnXValue; // Set New Pos
                other.gameObject.transform.position = playerPos; //Teleports
                Instantiate(laneTransitionPrefab, lanePosition, Quaternion.identity); // Creates a lane on the Player's new position

                //Does the same for the human owner
                Vector3 OwnerPosition = Owner.gameObject.transform.position;
                OwnerPosition.x += difference;
                Owner.gameObject.transform.position = OwnerPosition;
            }

        }
    }
}