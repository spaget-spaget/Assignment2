using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class TextScript : MonoBehaviour
    {
        private CollisionDetectorScript collisonScript;
        private InternalTimer internalTimer;

        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI pointsText;

        private static TextScript instance;

        // This makes sure that only one Canvas exists at a time
        void Awake()
        {
            // Singleton to prevent duplicates
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            //constant updates to the UI
            if (internalTimer && timerText && energyText)
            {
                timerText.text = "Time: Minutes: " + Mathf.FloorToInt(internalTimer.elapsedTime / 60) + " Seconds: " + Mathf.FloorToInt(internalTimer.elapsedTime % 60); //Displays mintues and seconds
                energyText.text = "Energy: " + internalTimer.energyMeter; // Displays energy
            }

            if (collisonScript && pointsText)
                pointsText.text = "Points: " + collisonScript.points; // Displays points
        }

        //This ensures that it is called upon when the scene is loaded rather than (On Awake)
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Canvas sceneCanvas = FindObjectOfType<Canvas>(); // Find the Canvas in the scene
            if (sceneCanvas != null && timerText != null)
            {
                timerText.transform.SetParent(sceneCanvas.transform, false);
            }
            StartCoroutine(ReconnectReferences());
        }

        IEnumerator ReconnectReferences()
        {
            // Wait 1 frame to make sure objects have spawned
            yield return null;
            
            // Re-find UI
            timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
            energyText = GameObject.Find("EnergyText")?.GetComponent<TextMeshProUGUI>();
            pointsText = GameObject.Find("PointText")?.GetComponent<TextMeshProUGUI>();

            // Re-find player
            GameObject playerCatObject = GameObject.Find("PlayerCat");
            if (playerCatObject != null)
            {
                collisonScript = playerCatObject.GetComponent<CollisionDetectorScript>();
            }
            else
            {
                Debug.LogWarning("PlayerCat object not found in scene.");
            }

            // Re-find timer
            GameObject timerObject = GameObject.Find("GlobalTimer");
            if (timerObject != null)
                internalTimer = timerObject.GetComponent<InternalTimer>();
            else
                Debug.LogWarning("GlobalTimer object not found in scene.");
        }
    }
}

