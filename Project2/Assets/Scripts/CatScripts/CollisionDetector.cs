using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CollisionDetector : MonoBehaviour
    {
        private BoxCollider2D MyRb { get; set; }
        private CatMovement catMovement;
        private InternalTimer internalTimer;
        // Start is called before the first frame update
        void Awake()
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
            MyRb = GetComponent<BoxCollider2D>();
            catMovement = GetComponent<CatMovement>();
        }

        public void OnTriggerStay2D(Collider2D other) // checks if the cat has collided with a blockade
        {
            if ((other.gameObject.CompareTag("Blockades")) || 
            ((other.gameObject.CompareTag("Crouchables")) && (catMovement.state != 3)) || 
            ((other.gameObject.CompareTag("Jumpable")) && (catMovement.state != 2)))
            {
                SceneManager.LoadScene("GameOver");
                return;
            }

            if (other.gameObject.CompareTag("CatFood"))
            {
                internalTimer.energyMeter += 40;
                if (internalTimer.energyMeter > 100) 
                        { internalTimer.energyMeter = 100; };
                       
                
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("HumanOwner"))
            {
                    SceneManager.LoadScene("GameOver");
                    return;
            }
            
        }
    }
}