using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CameraControl : MonoBehaviour
{
    private GameObject ball;
    private GameObject defaultCameraPosition;
    [SerializeField] private GameObject mapView;
    [SerializeField, Range(0.5f, 2)] private float cameraSpeed;

    private bool inMapView = false;
    private bool locked;

    private void Awake()
    {
        locked = false;
        cameraSpeed = 0.7f;
    }
    void Start()
    {
        SetCameraAtPlayer(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleMapView();
        }

        if (!inMapView)
        {
            Vector3 oldPos = transform.localPosition;

            transform.LookAt(ball.transform);

            if (transform.localPosition.y < 0)
            {
                transform.localPosition = Vector3.Scale(oldPos, new Vector3(1, 0, 1));
            }
            else
            {
                RotateCamera();
            }
        }
    }

    public void SetCameraAtPlayer(int id)
    {
        ball = GameObject.Find(MultiGameManager.GetInstance().players[id].name).transform.Find("Ball").Find("CameraFocus").gameObject;
        defaultCameraPosition = ball.transform.Find("DefaultCameraPosition").gameObject;

        transform.position = defaultCameraPosition.transform.position;
        transform.LookAt(ball.transform);
        transform.parent = ball.transform;
    }

    private void FocusOnBall()
    {
        transform.position = defaultCameraPosition.transform.position;
        transform.LookAt(ball.transform);
        transform.parent = ball.transform;    
    }
    private void RotateCamera()
    {
        if (!locked) {
            //Mouse left
            if (Input.GetAxis("Mouse X") < 0)
            {
                ball.transform.Rotate(new Vector3(0, -cameraSpeed, 0));
            }

            //Mouse right
            if (Input.GetAxis("Mouse X") > 0)
            {
                ball.transform.Rotate(new Vector3(0, cameraSpeed, 0));
            }

            //Mouse up
            if (Input.GetAxis("Mouse Y") > 0)
            {
                transform.RotateAround(ball.transform.position, ball.transform.right, cameraSpeed);
            }

            //Mouse down
            if (Input.GetAxis("Mouse Y") < 0)
            {
                transform.RotateAround(ball.transform.position, -ball.transform.right, cameraSpeed);
            }
        }

    }

    private void ToggleMapView()
    {
        if (!inMapView)
        {
            inMapView = true;
            transform.parent = null;
            transform.position = mapView.transform.position;
            transform.rotation = mapView.transform.rotation;
            Camera.main.orthographic = true;
        }

        else
        {
            inMapView = false;
            Camera.main.orthographic = false;
            transform.parent = ball.transform;
            FocusOnBall();
        }
    }

    public void lockCamera(){
        locked = true;
    }

    public void unlockCamera(){
        locked = false;
    }
}
