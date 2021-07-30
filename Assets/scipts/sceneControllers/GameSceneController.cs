using scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    
    public void LoadMenu() => sceneController.Get().LoadMenu();

    public void StopTime() => Time.timeScale = 0;

    public void ResumeTime() => Time.timeScale = 1;
}
