using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private int numOfBalls;
    private float speed;
    private Transform myTransform;
    private float startingY;
    private float maxY;
    // Start is called before the first frame update
    void Start()
    {
        numOfBalls = 0;
        speed = 0;
        myTransform = this.gameObject.transform;
        startingY = myTransform.localPosition.y;
        maxY = startingY + 1f;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = myTransform.localPosition;
        newPosition.y = newPosition.y + speed;
        myTransform.localPosition = newPosition;

        if (myTransform.localPosition.y <= startingY || myTransform.localPosition.y >= maxY){
            speed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball script = other.gameObject.GetComponent<Ball>();
        if (script != null)
        {
            Debug.Log("Ball entered");
            if (numOfBalls == 0) {
                raise();
            }
            numOfBalls++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ball script = other.gameObject.GetComponent<Ball>();
        if (script != null)
        {
            Debug.Log("Ball left");
            numOfBalls--;
            if (numOfBalls == 0){
                lower();
            }
        }
    }

    private void raise() {
        speed = 0.01f;
    }

    private void lower() {
        speed = -0.01f;
    }
}
