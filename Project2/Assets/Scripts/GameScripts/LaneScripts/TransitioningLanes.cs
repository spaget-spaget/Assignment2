using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitioningLanes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject laneTransitionPrefab;
    void Update()
    {
        if (laneTransitionPrefab == null)
        {
            laneTransitionPrefab = Resources.Load<GameObject>("prefab/LaneTransitioner");
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            {
                // Spawn the lane transition prefab directly above the player
                Vector3 spawnPos = new Vector3(0, 0, 0); // adjust height as needed
                Instantiate(laneTransitionPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
