using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Transform myTransform;
    private Transform barTransform;
    private bool charging;
    private float force;
    private float increment;
    private bool reset;
    private const float ratio = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.gameObject.transform;
        barTransform = myTransform.GetChild(0);
        charging = false;
        reset = true;
        increment = -0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!charging)
        {
            //draw full bar
            if (reset) // this doesn't need to happen every frame
            {
                Vector3 newScale = barTransform.localScale;
                Vector3 newPosition = barTransform.localPosition;
                newScale.z = 1;
                newPosition.x = ratio;
                barTransform.localScale = newScale;
                barTransform.localPosition = newPosition;
                reset = false;
            }
        } 
        else 
        {
            Vector3 newScale = barTransform.localScale;
            Vector3 newPosition = barTransform.localPosition;
            newScale.z = force;
            newPosition.x = force*ratio;
            barTransform.localScale = newScale;
            barTransform.localPosition = newPosition;

            if (force <= 0.05f || force > 1f)
            {
                increment *= -1;
            }
            //TODO: maybe have a varying increment
            force += increment;
        }

        if (!charging)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myTransform.Rotate(0, -0.6f, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                myTransform.Rotate(0, 0.6f, 0);
            }
        }
    }

    public bool isCharging()
    {
        return charging;
    }

    public void charge()
    {
        charging = true;
        force = 1;
    }

    //returns force and stops charge
    public float getForce()
    {
        charging = false;
        reset = true;
        return force;
    }
}
