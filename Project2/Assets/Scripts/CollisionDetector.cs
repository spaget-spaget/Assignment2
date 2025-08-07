using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CollisionDetectorScript : MonoBehaviour
    {
        private BoxCollider2D MyRb { get; set; }
        private CatMovement catMovement;
        private InternalTimer internalTimer;
        public int points = 0;
        public bool invincibleState = false;
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
            if (((other.gameObject.CompareTag("Crouchables")) && (catMovement.state != 3)) && (invincibleState == false) ||
            ((other.gameObject.CompareTag("Jumpable")) && (catMovement.state != 2)) && (invincibleState == false))
            {

                StartCoroutine(HitObstaclePenalty());
                return;
                
            }

            if (other.gameObject.CompareTag("CatFood"))
            {
                internalTimer.energyMeter += 40;
                if (internalTimer.energyMeter > 100) 
                        { internalTimer.energyMeter = 100; };
                points += 10;
                
                Destroy(other.gameObject);
            }
            if ((other.gameObject.CompareTag("HumanOwner")) ||
            (other.gameObject.CompareTag("Blockades")))
            {
                SceneManager.LoadScene("GameOver");
                return;
            }
        }
        private IEnumerator HitObstaclePenalty()
        {
            invincibleState = true;
            internalTimer.hitPenalty = 0.30f;

            yield return new WaitForSeconds(1.5f); // delay for 1 second

            internalTimer.hitPenalty = 0f;
            invincibleState = false;
        }
    }
}