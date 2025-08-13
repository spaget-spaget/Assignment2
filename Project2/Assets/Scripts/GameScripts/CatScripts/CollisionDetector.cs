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
        public GameObject invisibleBarriers;
        private Animator animator;
        // Start is called before the first frame update
        void Awake()
        {
            GameObject invisibleBarriers = GameObject.Find("DeleteBarriers");
            if (invisibleBarriers != null)
            {
                invisibleBarriers.SetActive(false);
            }
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
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator not found!");
            }
        }
        void Update()
        {
            if ((internalTimer.isRunning == true) && (animator.GetBool("Defeated") == true))
            {
                animator.SetBool("Defeated", false);
                animator.SetBool("Winning", true);
            }
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
                points += 100;
                
                Destroy(other.gameObject);
            }
            if ((other.gameObject.CompareTag("HumanOwner")) ||
            (other.gameObject.CompareTag("Blockades")))
            {
                
                StartCoroutine(losingFunction());
                return;
            }
            if (other.gameObject.CompareTag("InvisibleDeleteBarriers"))
            {
                invisibleBarriers.SetActive(true);
            }
            if (other.gameObject.CompareTag("EndingLane"))
            {
                StartCoroutine(winningFunction());
                return;
            }
        }
        private IEnumerator HitObstaclePenalty()
        {
            animator.SetBool("WhenDamaged", true);
            invincibleState = true;
            
            internalTimer.hitPenalty = 0.30f;
            
            yield return new WaitForSeconds(1.5f); // delay for 1 second

            internalTimer.hitPenalty = 0f;
            animator.SetBool("WhenDamaged", false);
            invincibleState = false;
        }
        private IEnumerator losingFunction()
        {
            internalTimer.isRunning = false;
            animator.SetBool("Defeated", true);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameOver");
        }
        private IEnumerator winningFunction()
        {
            internalTimer.isRunning = false;
            animator.SetBool("Winning", true);
            yield return new WaitForSeconds(1.6f);
            SceneManager.LoadScene("WinScreen");
        }
    }
}