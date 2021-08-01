using scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public void LoadMenu() => sceneController.Get().LoadMenu();

    public void LoadHighscore() => sceneController.Get().LoadHigscoreBoard();
}
