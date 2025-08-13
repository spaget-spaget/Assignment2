using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CollisionDetectorScript : MonoBehaviour
    {
        // Variables
        private BoxCollider2D MyRb { get; set; }
        private CatMovement catMovement;
        private InternalTimer internalTimer;
        public int points = 0;
        public bool invincibleState = false;
        public GameObject invisibleBarriers;
        private Animator animator;
        public AudioSource backgroundMusic;
        public AudioSource audioSource;
        public AudioClip[] audioClips;
        // Start is called before the first frame update
        void Awake()
        {
            // Finds the Global timer, animator and the invisible barriers
            GameObject invisibleBarriers = GameObject.Find("DeleteBarriers");
            animator = GetComponent<Animator>();
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
            if (animator == null)
            {
                Debug.LogWarning("Animator not found!");
            }
            MyRb = GetComponent<BoxCollider2D>();
            catMovement = GetComponent<CatMovement>();
            
            
        }
        void Update()
        {
            // Checks if the timer is still running.
            // If it is but the boolean defeated or won is true, unchecks the boxes to ensure that the cat is no longer defeated.
            // This is for the animation
            if ((internalTimer.isRunning == true) && ((animator.GetBool("Defeated") == true) || (animator.GetBool("Winning") == true)))
            {
                animator.SetBool("Defeated", false);
                animator.SetBool("Winning", false);
            }
            if (internalTimer.isRunning == false)
            {
                backgroundMusic.Stop();
            }
        }
        //Collision Detection
        public void OnTriggerStay2D(Collider2D other) 
        {
            // Checks if the timer is running
            if (internalTimer.isRunning == true)
            {
                // Checks if the cat has collided with a non instant kill object.
                if (((other.gameObject.CompareTag("Crouchables")) && (catMovement.state != 3)) && (invincibleState == false) ||
                ((other.gameObject.CompareTag("Jumpable")) && (catMovement.state != 2)) && (invincibleState == false))
                {
                    StartCoroutine(HitObstaclePenalty());
                    return;

                }
                //Cat food collision
                if (other.gameObject.CompareTag("CatFood"))
                {
                    internalTimer.energyMeter += 40; // adds energy
                    audioSource.PlayOneShot(audioClips[1]);
                    if (internalTimer.energyMeter > 100) // prevents energy meter from going over 100
                    { internalTimer.energyMeter = 100; }
                    ;
                    points += 100;
                    Destroy(other.gameObject);
                }

                //Instantkill Objects
                if ((other.gameObject.CompareTag("HumanOwner")) ||
                (other.gameObject.CompareTag("Blockades")))
                {
                    StartCoroutine(losingFunction());
                    return;
                }

                //Invisible Barriers (This is to reactivate the barriers once the tutorial is over)
                if (other.gameObject.CompareTag("InvisibleDeleteBarriers"))
                {
                    invisibleBarriers.SetActive(true);
                }
                //Win collider
                if (other.gameObject.CompareTag("EndingLane"))
                {
                    StartCoroutine(winningFunction());
                    return;
                }
            }
        }
        //Coroutines
        private IEnumerator HitObstaclePenalty()
        {
            animator.SetBool("WhenDamaged", true); // Change animation
            invincibleState = true; // Makes the cat invincible
            audioSource.PlayOneShot(audioClips[0]); 
            internalTimer.hitPenalty = 0.30f; // 30% hit penalty
            
            yield return new WaitForSeconds(1.5f); // delay for 1.5 second

            internalTimer.hitPenalty = 0f; // resets the hit penalty
            animator.SetBool("WhenDamaged", false);
            invincibleState = false;
        }

        //These 2 are the same, except they play different audio and open different scenes
        private IEnumerator losingFunction()
        {
            internalTimer.isRunning = false;
            animator.SetBool("Defeated", true);
            audioSource.PlayOneShot(audioClips[2]);
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("GameOver");
        }
        private IEnumerator winningFunction()
        {
            internalTimer.isRunning = false;
            animator.SetBool("Winning", true);
            audioSource.PlayOneShot(audioClips[3]);
            yield return new WaitForSeconds(4.8f);
            SceneManager.LoadScene("WinScreen");
        }
    }
}