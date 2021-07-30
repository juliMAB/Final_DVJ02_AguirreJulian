using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviourSingletonScript;
using UnityEngine.SceneManagement;

namespace scene
{
    public class sceneController : MonoBehaviourSingleton<sceneController>
    {
        public void LoadGame()
        {
            SceneManager.LoadScene("GAME");
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene("MENU");
        }
        public void LoadEnd()
        {
            SceneManager.LoadScene("END");
        }
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}


