using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class TextScript : MonoBehaviour
    {
        // Start is called before the first frame update
        private CatMovement playerCatScript;
        private CollisionDetectorScript collisonScript;
        private InternalTimer internalTimer;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI energyText;
        public TextMeshProUGUI pointsText;
        void Start()
        {
            timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
            energyText = GameObject.Find("EnergyText").GetComponent<TextMeshProUGUI>();
            pointsText = GameObject.Find("PointText").GetComponent<TextMeshProUGUI>();
            timerText.text = "Time: 0";
            energyText.text = "Energy: 0";
            pointsText.text = "Points: 0";
            {
                GameObject playerCatObject = GameObject.Find("PlayerCat");
                if (playerCatObject != null)
                {
                    playerCatScript = playerCatObject.GetComponent<CatMovement>();
                    collisonScript = playerCatObject.GetComponent<CollisionDetectorScript>();
                }
                else
                {
                    Debug.LogWarning("PlayerCat object not found!");
                }
                // Find the GameObject that has InternalTimer
                GameObject timerObject = GameObject.Find("GlobalTimer"); // change to actual name
                if (timerObject != null)
                {
                    internalTimer = timerObject.GetComponent<InternalTimer>();
                }
                else
                {
                    Debug.LogWarning("Timer object not found!");
                }

            }
        }

        // Update is called once per frame
        void Update()
        {
            timerText.text = "Time: " + Mathf.FloorToInt(internalTimer.elapsedTime);
            energyText.text = "Energy: " + internalTimer.energyMeter;
            pointsText.text = "Points: " + collisonScript.points;
        }
    }
}