using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PositionLocker : MonoBehaviour
    {
        private CatMovement playerCatScript; // reference to CatMovement script

        void Start()
        {
            GameObject playerCatObject = GameObject.Find("PlayerCat");
            if (playerCatObject != null)
            {
                playerCatScript = playerCatObject.GetComponent<CatMovement>();
            }
            else
            {
                Debug.LogWarning("PlayerCat object not found!");
            }
            //Sets the initial position of the camera
            transform.position = new Vector3(playerCatObject.transform.position.x, playerCatObject.transform.position.y -1.5f, transform.position.z);
        }

        void Update()
        {
            if (playerCatScript != null)
            {
                //Updates the position of the camera
                Vector3 targetPosition = playerCatScript.transform.position;
                transform.position = new Vector3(targetPosition.x, transform.position.y , -10f);
            }
        }
    }
}