using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ConstantMovement : MonoBehaviour
    {

        private InternalTimer movementSpeed;
        private InternalTimer targetXvalue; // ending x value
        // Start is called before the first frame update
        void Start()
        {

            targetXvalue = GetComponent<InternalTimer>();
            movementSpeed = GetComponent<InternalTimer>();
        }

        // Update is called once per frame
        void Update()
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetXvalue.targetXvalue, movementSpeed.movementSpeed * Time.deltaTime);
            
        }
    }
}