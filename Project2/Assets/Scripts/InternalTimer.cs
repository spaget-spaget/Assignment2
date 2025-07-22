using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InternalTimer : MonoBehaviour
    {
        public float movementSpeed = 1f;
        private float elapsedTime = 0f;
        private bool isRunning = false;
        private bool waitASec = false;
        private float waitTime = 0f;
        public float speedIncreaseRate = 0.1f;
        public Vector3 targetXvalue;
        // Start is called before the first frame update
        void Start()
        {
            targetXvalue = new Vector3(100f, transform.position.y, transform.position.z);
            StartStopwatch();
        }

        // Update is called once per frame
        // Update is called once per frame
        void Update()
        {

            if (isRunning)
            {
                elapsedTime += Time.deltaTime;

                int minutes = Mathf.FloorToInt(elapsedTime / 60);
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
            }
            if ((Mathf.FloorToInt(elapsedTime % 10) == 0) && (elapsedTime > 0) && (waitASec == false))
            {
                movementSpeed += speedIncreaseRate;
                waitASec = true;
                waitTime = elapsedTime + 1f;
            }
            if ((waitASec == true) && (elapsedTime >= waitTime))
            {
                waitASec = false;
            }

            if (this == null || gameObject == null)
            {
                StopStopwatch();
                return;
            }

        }
        /*Clock Code Begins Here*/
        public void StopStopwatch()
        {
            isRunning = false;
        }

        public void StartStopwatch()
        {
            isRunning = true;
        }

        public void ResetStopwatch()
        {
            elapsedTime = 0f;
        }

    }
}