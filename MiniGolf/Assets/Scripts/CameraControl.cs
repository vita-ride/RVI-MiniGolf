using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CameraControl : MonoBehaviour
{
    private GameObject ball;
    private GameObject defaultCameraPosition;
    [SerializeField] private GameObject mapView;
    private float cameraSpeed;
    private CPC_CameraPath cameraPath;

    public bool inMapView = false;
    private bool locked;

    private void Awake()
    {
        locked = false;
        cameraSpeed = 100f;
    }
    void Start()
    {
        cameraPath = GameObject.Find("CameraPath").GetComponent<CPC_CameraPath>();
    }

    void Update()
    {
        if (!cameraPath.IsPlaying() && ball != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ToggleMapView();
            }

            if (!inMapView)
            {
                transform.LookAt(ball.transform);
                RotateCamera();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && cameraSpeed > 60)
        {
            cameraSpeed -= 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && cameraSpeed < 160)
        {
            cameraSpeed += 10;
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
                ball.transform.Rotate(new Vector3(0, -cameraSpeed*Time.deltaTime, 0));
            }

            //Mouse right
            if (Input.GetAxis("Mouse X") > 0)
            {
                ball.transform.Rotate(new Vector3(0, cameraSpeed*Time.deltaTime, 0));
            }

            //Mouse up
            if (Input.GetAxis("Mouse Y") > 0)
            {
                if (transform.localPosition.y > 0 || transform.localPosition.z < 0)
                {
                    transform.RotateAround(ball.transform.position, ball.transform.right, cameraSpeed*Time.deltaTime);
                }
            }

            //Mouse down
            if (Input.GetAxis("Mouse Y") < 0)
            {
                if (transform.localPosition.y > 0 || transform.localPosition.z > 0)
                {
                    transform.RotateAround(ball.transform.position, -ball.transform.right, cameraSpeed*Time.deltaTime);
                }
            }
        }

    }

    public void ToggleMapView()
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
