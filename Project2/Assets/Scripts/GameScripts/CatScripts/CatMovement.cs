using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CatMovement : MonoBehaviour
    {
        //Movement Variables
        private int CurrentPosition = 2; // Current row. There are 4 rows, starting at 0 and ending at 3 from bottom to top. cat starts at row 2, which is the first row above the middle.
        public int state = 1; // Running = 1, Jumping = 2, Crawling = 3. its better than using String
        private Vector3 targetYvalue; // ending y value
        private Vector3 runningScale = new Vector3(8, 8, -3); // this is the scale of the cat when it is running
        private Vector3 jumpingScale = new Vector3(12f, 12f, -3); // this is the scale of the cat when it is jumping
        private Vector3 crawlingScale = new Vector3(4f, 4f, -3); // this is the scale of the cat when it is crawling
        private bool inScaling = false; // this is a boolean that is used to check if the cat is currently being scaled to a new size
        private bool changingLanes = false; // this is a boolean that is used to check if the cat is currently changing lanes
        private float laneChangingSpeed = 0.2f; // Adjust this value to control the speed in which the cat changes lanes
        private float sizeChangingTime = 0.2f; // Adjust this value to control the speed in which the cat changes size
        public float yPosition = 0; // current Y position. Easier to manipulate
        public float bottomLanePosition = -11; // The Y position of the cat at the very bottom lane (lane 0)

        private InternalTimer internalTimer; // Reference to the timer script
        //Audio Variables
        public AudioSource audioSource; 
        public AudioClip jumpSound;
        void Start()
        {
            // Find the Global Timer Object that has the InternalTimer script
            GameObject timerObject = GameObject.Find("GlobalTimer"); 
            if (timerObject != null)
            {
                internalTimer = timerObject.GetComponent<InternalTimer>();
            }
            else
            {
                Debug.LogWarning("Timer object not found!");
            }
            // initial calculation of y value
            yPosition = (bottomLanePosition + (CurrentPosition * 3));
        }

        // Update is called once per frame
        void Update()
        {
            // checks if the timer is running
            if (internalTimer.isRunning == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, internalTimer.targetXvalue, internalTimer.movementSpeed * Time.deltaTime); // moves the cat to the target position
                if ((CurrentPosition < 3) && (changingLanes == false)) // checks if the cat is at the top row (which is row 3). if cat is in row 3, it wont be able to move up
                {
                    if (Input.GetKey("w")) // checks if the player has pressed the w key
                    {
                        callMoveUp(); // moves the cat up one row
                    }
                }
                if ((CurrentPosition > 0) && (changingLanes == false)) // checks if the cat is at the bottom row (which is row 0). if cat is in row 0, it wont be able to move down
                {

                    if (Input.GetKey("s")) // checks if the player has pressed the s key
                    {
                        callMoveDown(); // moves the cat down one row
                    }
                }


                if ((Input.GetKey("space")) && (inScaling == false)) // checks if the player has pressed the space key
                {

                    if (state == 1) // checks if the cat is in the running state, which is 1
                    {
                        callCatJump(); // calls the CatJump function
                        audioSource.PlayOneShot(jumpSound);
                    }
                }
                if ((Input.GetKey("e")) && (inScaling == false))// checks if the player has pressed the a key
                {


                    if (state == 1) // checks if the cat is in the running state, which is 1
                    {
                        callCatCrawl(); // calls the CatCrawl function
                    }
                    else if (state == 3) // checks if the cat is in the crawling state, which is 3
                    {
                        callStopCrawling(); // calls the StopCrawling function
                    }
                }

            }
        }


        private async void callMoveUp() // calls the CatJump function
        {
            await MoveUp();

        }
        private async void callMoveDown() // calls the CatJump function
        {
            await MoveDown();

        }
        private async void callCatJump() // calls the CatJump function
        {
            await CatJump();
        }
        private async void callCatCrawl() // calls the CatCrawl function
        {
            await CatCrawl();
        }
        private async void callStopCrawling() // calls the StopCrawling function
        {
            await StopCrawling();
        }


        // Up and Down movements
        async Task MoveUp()
        {
            changingLanes = true; // set changingLanes to true so that cat cannot change lanes
            // Math needed to change lanes
            CurrentPosition += 1;
            yPosition = (bottomLanePosition + (CurrentPosition * 3));
            targetYvalue = new Vector3(transform.position.x + (internalTimer.movementSpeed * laneChangingSpeed), yPosition, transform.position.z);

            await MoveTo(targetYvalue);
            
            changingLanes = false; // set changingLanes to false so that cat can change lanes again
        }
        async Task MoveDown()
        {
            changingLanes = true;
            CurrentPosition -= 1;
            yPosition = (bottomLanePosition + (CurrentPosition * 3));
            targetYvalue = new Vector3(transform.position.x + (internalTimer.movementSpeed * laneChangingSpeed), yPosition, transform.position.z);
            await MoveTo(targetYvalue);
            
            changingLanes = false; 

        }

        // crawl and jump movements
        async Task CatCrawl() //CatCrawl function
        {
            inScaling = true; // set inScaling to true so that cat cannot be scaled
            await ScaleTo(crawlingScale); // scales the cat to the crawling scale
            state = 3; // sets the state to 3, which is the crawling state
            await Task.Delay(400);
            inScaling = false; // set inScaling to false so that cat can be scaled again
        }
        async Task StopCrawling()
        {
            inScaling = true;
            await ScaleTo(runningScale);
            state = 1; 
            await Task.Delay(400);
            inScaling = false; 
        }

        async Task CatJump()
        {
            
            inScaling = true;
            state = 2;
            
            // Smoothly scale to jumpingScale
            await ScaleTo(jumpingScale);

            // Wait in the air
            await Task.Delay(700);

            // Smoothly scale back to runningScale
            await ScaleTo(runningScale);

            state = 1;
            inScaling = false; // set inScaling to false so that cat can be scaled again
        }

        // Scales the cat to the target scale
        async Task ScaleTo(Vector3 targetScale)
        {
            float elapsed = 0f; // keeps track of time
            Vector3 initialScale = transform.localScale; // stores the initial scale

            while (elapsed < sizeChangingTime) // while the elapsed time is less than the sizeChangingTime
            {
                if (this == null || gameObject == null) return; // if the cat is destroyed, stop scaling (Mostly here to prevent errors)
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / sizeChangingTime);
                elapsed += Time.deltaTime; // adds time
                await Task.Yield(); // Wait until next frame
            }

            transform.localScale = targetScale; // ensure final scale is exactly set

        }
        async Task MoveTo(Vector3 targetPosition)
        {
            float elapsed = 0f; // keeps track of time
            Vector3 initialPosition = transform.position; // stores the initial position

            while (elapsed < laneChangingSpeed) // while the elapsed time is less than the laneChangingTime
            {
                if (this == null || gameObject == null) return; // if the cat is destroyed, stop changing lanes (Mostly here to prevent errors)
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / laneChangingSpeed);
                elapsed += Time.deltaTime; // adds time
                await Task.Yield(); // Wait until next frame
            }

            transform.position = targetPosition; // ensure final scale is exactly set

        } // The short version is that this code gradually allows the cat to change size and move lanes rather than instantly being scaled and moved.
    }
}