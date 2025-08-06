using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameStart : MonoBehaviour
    {

        public void StartGame()
        {
            Debug.Log("Button Clicked. Game Started!");
            SceneManager.LoadScene("Game");
        }
    }
}