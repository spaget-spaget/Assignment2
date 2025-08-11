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
        }

        void Update()
        {
            if (playerCatScript != null)
            {
                Vector3 targetPosition = playerCatScript.transform.position;
                transform.position = new Vector3(targetPosition.x, -0.53f, -10f);
            }
        }
    }
}