using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNextLevel : MonoBehaviour
{
    public void StartNextLevel()
    {
        MultiGameManager.GetInstance().initNextLevel();
    }
}
