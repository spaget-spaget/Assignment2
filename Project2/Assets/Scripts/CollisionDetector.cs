using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CollisionDetector : MonoBehaviour
    {
        private BoxCollider2D MyRb { get; set; }
        private CatMovement catMovement;
        // Start is called before the first frame update
        void Awake()
        {
            MyRb = GetComponent<BoxCollider2D>();
            catMovement = GetComponent<CatMovement>();
        }

        public void OnTriggerStay2D(Collider2D other) // checks if the cat has collided with a blockade
        {
            if ((other.gameObject.CompareTag("Blockades")) && (catMovement.state != 2))
            {
                Destroy(gameObject);
                return;
            }

            else if ((other.gameObject.CompareTag("Crouchables")) && (catMovement.state != 3))
            {
                Destroy(gameObject);
                return;
            }
            
        }
    }
}