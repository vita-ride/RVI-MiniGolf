using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour
{
    private GameObject canvas;
    private CPC_CameraPath cameraPath;

    private void Start()
    {
        canvas = transform.GetChild(0).gameObject;
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        cameraPath = GameObject.Find("CameraPath").GetComponent<CPC_CameraPath>();
        Debug.Log("START SKIP");
    }
    public void Skip()
    {
        Debug.Log("CLICKED SKIP");
        if (cameraPath.IsPlaying())
        {
            Debug.Log("SKIP");
            cameraPath.StopPath();
            canvas.gameObject.SetActive(false);
        }
    }
}
