using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameStart : MonoBehaviour
    {
        public InternalTimer internalTimer;
        public void Start()
        {
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
        public void StartGame()
        {
           
            Debug.Log("Button Clicked. Game Started!");
            SceneManager.LoadScene("Game");
        }
        public void MainMenu()
        {
            Debug.Log("Button Clicked. Back to Main Menu!");
            SceneManager.LoadScene("Start Menu");
        }
        
    }
}