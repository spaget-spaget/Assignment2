using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LaneGenerator : MonoBehaviour
    {
        private BoxCollider2D MyRb { get; set; }
        public GameObject LanesPrefab;
        private Vector3 MyPosition { get; set; }
        private bool LaneMade = false;

        
        
        public void Update(){



        if (LanesPrefab == null)
            {
                LanesPrefab = Resources.Load<GameObject>("prefab/Lanes");
            }
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            if ((LaneMade == false) && (other.gameObject.CompareTag("Player")))
            {
                MyPosition = new Vector3(this.transform.position.x + 28, 1, 0);
                GameObject newLanes = Instantiate(LanesPrefab, MyPosition, Quaternion.identity);
                LaneMade = true;
            }
            if ((LaneMade == true) && (other.gameObject.CompareTag("LaneDelete")))
            {   
                Destroy(gameObject);
                LaneMade = false;
            }
        }
    }
}