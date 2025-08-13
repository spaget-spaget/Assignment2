using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class ObjectDeleter : MonoBehaviour
    {
        // Start is called before the first frame update
        //Invisible barrier code. Made to just delete anything except for a few exceptions
        private BoxCollider2D MyRb { get; set; }
        void Awake()
        {
            MyRb = GetComponent<BoxCollider2D>();
        }
        // Update is called once per frame
        public void OnTriggerStay2D(Collider2D other)
        {
            if ((other.gameObject.tag != ("HumanOwner")) && (other.gameObject.tag != ("InvisibleDeleteBarriers")) && (other.gameObject.tag != ("ObjectDelete")))
            {
                Destroy(other.gameObject);
                return;
            }
        }
    }
}