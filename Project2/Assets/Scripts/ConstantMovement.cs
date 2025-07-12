using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class ConstantMovement : MonoBehaviour
    {
        private Vector3 targetXvalue; // ending x value
        private float movementSpeed = 1f;
        private Vector3 targetPosition = new Vector3(100, 0, 0);
        // Start is called before the first frame update
        void Start()
        {
            targetXvalue = new Vector3(100f, transform.position.y, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {

            transform.position = Vector3.MoveTowards(transform.position, targetXvalue, movementSpeed * Time.deltaTime);

        }
    }
}