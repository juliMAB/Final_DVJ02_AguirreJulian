using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scene;

public class MenuController : MonoBehaviour
{
    public void LoadGame() => sceneController.Get().LoadGame();

    public void ExitGame() => sceneController.Get().ExitGame();
}
