using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingGame
{
    public class MainMenu : MonoBehaviour
    {
        public void StartNewGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
