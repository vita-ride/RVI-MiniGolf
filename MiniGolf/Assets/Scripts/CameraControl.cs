using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(GameObject))]
public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject defaultCameraPosition;
    [SerializeField] private GameObject mapView;
    [SerializeField, Range(10, 100)] private float cameraSpeed;

    private bool inMapView = false;

    private void Awake()
    {
        SetStartingPosition();
        cameraSpeed = 50;
    }
    void Start()
    {
        
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

    private void SetStartingPosition()
    {
        transform.position = defaultCameraPosition.transform.position;
        transform.LookAt(ball.transform);
        transform.parent = ball.transform;    
    }
    private void RotateCamera()
    {
        //Mouse left
        if (Input.GetAxis("Mouse X") < 0)
        {
            //transform.RotateAround(ball.transform.position, Vector3.up, 30 * Time.deltaTime);
            ball.transform.Rotate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
        }

        //Mouse right
        if (Input.GetAxis("Mouse X") > 0)
        {
            //transform.RotateAround(ball.transform.position, -Vector3.up, 30 * Time.deltaTime);
            ball.transform.Rotate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
        }

        //Mouse up
        if (Input.GetAxis("Mouse Y") > 0)
        {
            transform.RotateAround(ball.transform.position, ball.transform.right, cameraSpeed * Time.deltaTime);
        }

        //Mouse down
        if (Input.GetAxis("Mouse Y") < 0)
        {
            transform.RotateAround(ball.transform.position, -ball.transform.right, cameraSpeed * Time.deltaTime);
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
            SetStartingPosition();
        }
    }
}
