using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public Vector3 lastPosition;
    public bool moving;
    public float holeTime;
    public float minHoleTime;
    private Rigidbody rbody;
    public float lowestSpeed;
    public int hits;
    public float slowTime;
    public float slowTimeLimit;
    public bool myTurn;
    public bool wasHitThisTurn;

    [SerializeField] private Aim aim;
    // [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private CameraControl cameraControl;
    private double x;
    private double z;
    private Vector3 direction = new Vector3(0,0,0);
    [SerializeField] private float force;

    public delegate void BallInHoleAction(int hits);
    public event BallInHoleAction BallInHole;

    public delegate void BallStoppedAction();
    public event BallStoppedAction BallStopped;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        rbody = GetComponent<Rigidbody>();
        moving = true;
        slowTimeLimit = 1.5f;
        minHoleTime = 1f;
        hits = 0;
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // ako je loptica ispod lowestSpeed u trajanju od slowTimeLimit,
        // zapamti njenu poslednju poziciju i postavi moving na false (omoguci udarac)
        if (rbody.velocity.magnitude < lowestSpeed)
        {
            CountSlowTime();
            if(slowTime > slowTimeLimit)
            {
                moving = false;
                lastPosition = rbody.position;
                if (wasHitThisTurn)
                {
                    myTurn = false;
                    Debug.Log("wasHitThisTurn");
                    BallStopped?.Invoke();
                    wasHitThisTurn = false;
                }
            }
        }
        else
        {
            moving = true;
            slowTime = 0;
        }

        onFrame();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Plane")
        {
            rbody.velocity = Vector3.zero;
            transform.position = lastPosition;
            moving = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Hole")
        {
            CountHoleTime();
        }
    }

    private void CountHoleTime()
    {
        holeTime += Time.deltaTime;
        if(holeTime >= minHoleTime)
        {
            Debug.Log("d bol iz in d hol after " + hits + " hits");
            // javi igracu da mu je loptica u rupi
            BallInHole?.Invoke(hits);
            // loptica je u rupi, igrac je zavrsio
            holeTime = 0;
            
        }
    }

    private void CountSlowTime()
    {
        if(slowTime <= slowTimeLimit)
        {
            slowTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Hole")
        {
            holeTime = 0;
        }
    }

    private void onFrame() {
        if (moving || !myTurn)
        {
            aim.gameObject.SetActive(false);
        }
        else
        {
            aim.gameObject.SetActive(true);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!aim.isCharging())
                {
                    aim.charge();
                    cameraControl.lockCamera();
                }
                else
                {
                    cameraControl.unlockCamera();
                    float angle = aim.gameObject.transform.eulerAngles.y;

                    double angleInRadians = Math.PI * angle / 180.0;

                    x = Math.Sin(angleInRadians);
                    z = Math.Cos(angleInRadians);

                    direction.x = (float)x;
                    direction.z = (float)z;
                    slowTime = 0;
                    
                    aim.gameObject.SetActive(false); //additionally making sure the bar doesn't show after hitting

                    rbody.AddForce(direction.normalized * force * aim.getForce(), ForceMode.Impulse);
                    hits++;
                    wasHitThisTurn = true;
                    
                }
            }
        }
    }
}
