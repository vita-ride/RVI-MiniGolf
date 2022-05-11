using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestFizika : MonoBehaviour
{
    [SerializeField] private Aim aim;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private CameraControl camera;
    private double x;
    private double z;
    private Vector3 direction = new Vector3(0,0,0);
    [SerializeField] private float force;
    public Ball ball;
    private bool isMoving;

    void Update()
    {
        if (rigidBody.velocity.magnitude > 0)
        {
            aim.gameObject.SetActive(false);
            isMoving = true;
            
        }
        else
        {
            aim.gameObject.SetActive(true);
            isMoving = false;
        }

        if (!isMoving)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!aim.isCharging())
                {
                    aim.charge();
                    camera.lockCamera();
                }
                else
                {
                    camera.unlockCamera();
                    float angle = aim.gameObject.transform.eulerAngles.y;

                    double angleInRadians = Math.PI * angle / 180.0;

                    x = Math.Sin(angleInRadians);
                    z = Math.Cos(angleInRadians);

                    direction.x = (float)x;
                    direction.z = (float)z;
                    rigidBody.AddForce(direction.normalized * force * aim.getForce(), ForceMode.Impulse);
                    aim.gameObject.SetActive(false); //additionally making sure the bar doesn't show after hitting
                }
            }
        }

    }
}
