using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    // Start is called before the first frame update
    int CurrentPosition = 2;
    private Vector3 targetPosition;
    public float moveSpeed = 10.0f;
    private string state = "Running";
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If we're not at the target yet, don't allow new input
        if (transform.position != targetPosition)
            return;

        if (CurrentPosition < 3)
        {
            if (Input.GetKey("w"))
            {
                MoveCatUp();
            }
        }
        if (CurrentPosition > 0)
        {
            if (Input.GetKey("s"))
            {
                MoveCatDown();
            }
        }
        if (Input.GetKey("space"))
        {
            if (state == "Running")
            {
                CatJump();
            }
        }
        if (Input.GetKey("a"))
        {
            if (state == "Running")
            {
                CatCrawl();
            }
        }
    }
    void MoveCatUp()
    {
        targetPosition += new Vector3(0, 3, 0);
        CurrentPosition += 1;
    }
    void MoveCatDown()
    {
        targetPosition += new Vector3(0, -3, 0);
        CurrentPosition -= 1;
    }
    async Task CatJump()
    {
        state = "Jumping";
        transform.localScale = new Vector3(12, 12, 0);
        await Task.Delay(1500);
        transform.localScale = new Vector3(8, 8, 0);
        state = "Running";
    }
    async Task CatCrawl()
    {
        state = "Crawling";
        transform.localScale = new Vector3(4, 4, 0);
        await Task.Delay(1500);
        transform.localScale = new Vector3(8, 8, 0);
        state = "Running";
    }
}
