using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFramerate : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 144;
    private void Awake()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
