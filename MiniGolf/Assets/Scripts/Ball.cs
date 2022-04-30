using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        rbody = GetComponent<Rigidbody>();
        moving = true;
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
            }
        }
        else
        {
            moving = true;
            slowTime = 0;
        }
        
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
            // loptica je u rupi, igrac je zavrsio
            holeTime = 0;
            transform.gameObject.SetActive(false);
        }
    }

    private void CountSlowTime()
    {
        slowTime += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Hole")
        {
            holeTime = 0;
        }
    }
}
