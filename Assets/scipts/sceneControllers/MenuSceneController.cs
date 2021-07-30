using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scene;

public class MenuSceneController : MonoBehaviour
{
    public void LoadGame() => sceneController.Get().LoadGame();

    public void ExitGame() => sceneController.Get().ExitGame();
}
