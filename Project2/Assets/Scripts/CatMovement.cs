using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class CatMovement : MonoBehaviour
{
   
    int CurrentPosition = 2; // Current row. There are 4 rows, starting at 0 and ending at 3 from bottom to top. cat starts at row 2, which is the first row above the middle.
    private Vector3 targetPosition; // declares targetPosition variable
    public float laneChangingSpeed = 10.0f; // Adjust this value to control the speed in which the cat changes lanes
    public float sizeChangingTime = 0.2f; // Adjust this value to control the speed in which the cat changes size
    private int state = 1; // Running = 1, Jumping = 2, Crawling = 3. its better than using String
    private Vector3 runningScale = new Vector3(8, 8, 0); // this is the scale of the cat when it is running
    private Vector3 jumpingScale = new Vector3(12, 12, 0); // this is the scale of the cat when it is jumping
    private Vector3 crawlingScale = new Vector3(4, 4, 0); // this is the scale of the cat when it is crawling
    private bool inScaling = false; // this is a boolean that is used to check if the cat is currently being scaled to a new size
    public float movementSpeed = 0.1f;


    void Start()
    {
        targetPosition.x = transform.position.x;
        targetPosition.z = transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangingSpeed * Time.deltaTime); // Checks if it needs to move the cat up or down lanes
        // If the cat is not at the target yet, don't allow new input
        if (transform.position != targetPosition)
            return;
        
        if (CurrentPosition < 3) // checks if the cat is at the top row (which is row 3). if cat is in row 3, it wont be able to move up
        {
            if (Input.GetKey("w")) // checks if the player has pressed the w key
            {
                MoveCatUp(); // moves the cat up one row
            }
        }
        if (CurrentPosition > 0) // checks if the cat is at the bottom row (which is row 0). if cat is in row 0, it wont be able to move down
        {
            if (Input.GetKey("s")) // checks if the player has pressed the s key
            {
                MoveCatDown(); // moves the cat down one row
            }
        }
        if (Input.GetKey("space")) // checks if the player has pressed the space key
        {
            if (inScaling == false)
            {
                inScaling = true;
                if (state == 1) // checks if the cat is in the running state, which is 1
                {
                    callCatJump(); // calls the CatJump function
                }
            }
        }
        if (Input.GetKey("a")) // checks if the player has pressed the a key
        {
            if (inScaling == false)
            {
                inScaling = true;
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

    void MoveCatUp() // moves the cat up one row. the space between rows is about 3 units in y value
    {
        targetPosition += new Vector3(0, 3, 0); // adds 3 units to the y value
        CurrentPosition += 1; // adds 1 to the CurrentPosition, which is essentially the row that the cat is on
    }
    void MoveCatDown() // moves the cat down one row by subtracting 3 units from the y value
    {
        targetPosition += new Vector3(0, -3, 0); // subtracts 3 units from the y value
        CurrentPosition -= 1; // subtracts 1 from the CurrentPosition
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
 
    async Task CatCrawl() //CatCrawl function
    {
        await ScaleTo(crawlingScale); // scales the cat to the crawling scale
        state = 3; // sets the state to 3, which is the crawling state
    }
    async Task StopCrawling()
    {
        
        await ScaleTo(runningScale); // scales the cat to the running scale
        state = 1; // sets the state to 1, which is the running state
    }

    async Task CatJump()
    {
        state = 2;

        // Smoothly scale to jumpingScale
        await ScaleTo(jumpingScale); // Adjust duration as needed

        // Wait in the air
        await Task.Delay(1500);

        // Smoothly scale back to runningScale
        await ScaleTo(runningScale);

        state = 1;
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
        inScaling = false; // set inScaling to false so that cat can be scaled again
    }
}
