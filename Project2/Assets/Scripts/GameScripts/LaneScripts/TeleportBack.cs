using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TeleportBack : MonoBehaviour
    {
        public GameObject laneTransitionPrefab;
        private CatMovement playerCatScript;
        private float returnXValue = -28;
        private Vector3 lanePosition = new Vector3(-28, -6.05f, 0);
        private Vector3 OwnerPosition = new Vector3(0, 0, 0);
        private GameObject Owner;

        void Start()
        {
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
                Vector3 playerPos = other.gameObject.transform.position;
                float difference = (returnXValue - playerPos.x);
                playerPos.x = returnXValue;
                other.gameObject.transform.position = playerPos;
                Instantiate(laneTransitionPrefab, lanePosition, Quaternion.identity);

                Vector3 OwnerPosition = Owner.gameObject.transform.position;
                OwnerPosition.x += difference;
                Owner.gameObject.transform.position = OwnerPosition;
            }

        }
    }
}